﻿using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Connections;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using UserManagement;
using Utils;

namespace Chat
{
    public class ServerChat : Chat
    {
        /// <inheritdoc/>
        public override event EventHandler<LogEventArgs> OnLogEntry;
        /// <inheritdoc/>
        public override void RaiseLogEntry(LogEventArgs e) => OnLogEntry?.Invoke(this, e);
        
        /// <summary>
        /// <c>NetServer</c> instance to bind the packet handler to.
        /// </summary>
        private NetServer netServer;

        /// <summary>
        /// <c>ServerAuthenticator</c> instance used to check session validity.
        /// </summary>
        private ServerAuthenticator authenticator;

        /// <summary>
        /// <c>ServerUserManager</c> instance used to check login state and source UUID validity.
        /// </summary>
        private ServerUserManager userManager;

        /// <summary>
        /// List of chat messages sent to users on connect.
        /// </summary>
        private List<ChatMessage> messageHistory;
        /// <summary>
        /// Lock file to prevent race conditions when accessing <c>messageHistory</c>.
        /// </summary>
        private object messageHistoryLock = new object();
        /// <summary>
        /// Maximum number of chat messages to buffer in history.
        /// </summary>
        private int messageHistoryCapacity;
        
        public ServerChat(NetServer netServer, ServerAuthenticator authenticator, ServerUserManager userManager, int messageHistoryCapacity)
        {
            this.netServer = netServer;
            this.authenticator = authenticator;
            this.userManager = userManager;
            this.messageHistoryCapacity = messageHistoryCapacity;
            
            this.messageHistory = new List<ChatMessage>();
            
            netServer.RegisterPacketHandler(MODULE_NAME, packetHandler);
            userManager.OnLogin += loginHandler;
        }

        private void loginHandler(object sender, ServerLoginEventArgs args)
        {
            lock (messageHistoryLock)
            {
                // Create a chat history packet
                ChatHistoryPacket packet = new ChatHistoryPacket();
                
                // Convert each chat message to their packet counterparts
                foreach (ChatMessage chatMessage in messageHistory)
                {
                    // Create a chat message packet and add it to the history packet
                    packet.ChatMessages.Add(
                        new ChatMessagePacket
                        {
                            Uuid = chatMessage.SenderUuid,
                            Message = chatMessage.Message,
                            Timestamp = chatMessage.Timestamp.ToTimestamp()
                        }
                    );
                }
                
                // Pack the history packet
                Any packedPacket = ProtobufPacketHelper.Pack(packet);
                
                // Send it on its way
                netServer.Send(args.ConnectionId, MODULE_NAME, packedPacket.ToByteArray());
            }
        }

        /// <summary>
        /// Handles incoming packets.
        /// </summary>
        /// <param name="module">Target module</param>
        /// <param name="connectionId">Original connection ID</param>
        /// <param name="data">Data payload</param>
        private void packetHandler(string module, string connectionId, byte[] data)
        {
            // Discard packet if it fails validation
            if (!ProtobufPacketHelper.ValidatePacket(typeof(ServerChat).Namespace, MODULE_NAME, module, data, out Any message, out TypeUrl typeUrl)) return;

            // Determine what to do with the packet
            switch (typeUrl.Type)
            {
                case "ChatMessagePacket":
                    RaiseLogEntry(new LogEventArgs(string.Format("Got a ChatMessagePacket from {0}", connectionId), LogLevel.DEBUG));
                    chatMessagePacketHandler(connectionId, message.Unpack<ChatMessagePacket>());
                    break;
                default:
                    RaiseLogEntry(new LogEventArgs("Got an unknown packet type (" + typeUrl.Type + "), discarding...", LogLevel.DEBUG));
                    break;
            }
        }

        /// <summary>
        /// Handles incoming <c>ChatMessagePacket</c>s.
        /// </summary>
        /// <param name="module">Target module</param>
        /// <param name="connectionId">Original connection ID</param>
        /// <param name="data">Data payload</param>
        private void chatMessagePacketHandler(string connectionId, ChatMessagePacket packet)
        {
            // Refuse packets from non-authenticated sessions
            if (!authenticator.IsAuthenticated(connectionId, packet.SessionId))
            {
                sendFailedChatMessageResponse(connectionId, packet.MessageId);
            }

            // Refuse packets from non-logged in users
            if (!userManager.IsLoggedIn(connectionId, packet.Uuid))
            {
                sendFailedChatMessageResponse(connectionId, packet.MessageId);
            }

            // Get a copy of the packet's original message ID
            string originalMessageId = packet.MessageId;
            
            // Generate a new, guaranteed-to-be-unique message ID
            string newMessageId = Guid.NewGuid().ToString();
            
            // Sanitise the message content
            string sanitisedMessage = TextHelper.SanitiseRichText(packet.Message);
            
            // Get the current time
            DateTime timestamp = DateTime.UtcNow;
            
            // Add the message to the message history
            addMessageToHistory(new ChatMessage(newMessageId, packet.Uuid, sanitisedMessage, timestamp));
            
            // Send a response to the sender
            sendChatMessageResponse(connectionId, true, originalMessageId, newMessageId, sanitisedMessage);
                    
            // Broadcast the chat packet to everyone but the sender
            broadcastChatMessage(packet.Uuid, newMessageId, sanitisedMessage, timestamp, new[]{connectionId});
        }

        /// <summary>
        /// Broadcasts a <c>ChatMessagePacket</c> to all currently logged-in users.
        /// </summary>
        /// <param name="senderUuid">Sender's UUID</param>
        /// <param name="messageId">Message ID</param>
        /// <param name="message">Message content</param>
        /// <param name="timestamp">Timestamp</param>
        /// <param name="excludedConnectionIds">Array of connection IDs to be excluded from the broadcast</param>
        private void broadcastChatMessage(string senderUuid, string messageId, string message, DateTime timestamp, string[] excludedConnectionIds = null)
        {
            // Create and pack a ChatMessagePacket
            ChatMessagePacket packet = new ChatMessagePacket
            {
                Uuid = senderUuid,
                MessageId = messageId,
                Message = message,
                Timestamp = timestamp.ToTimestamp()
            };
            Any packedPacket = ProtobufPacketHelper.Pack(packet);

            // Get an array of connection IDs for each logged in user
            string[] connectionIds = userManager.GetConnections();
            
            // Remove the connection IDs to be excluded from the broadcast (if any)
            if (excludedConnectionIds != null)
            {
                connectionIds = connectionIds.Except(excludedConnectionIds).ToArray();
            }
            
            // Send it to each of the remaining connection IDs
            foreach (string connectionId in connectionIds)
            {
                try
                {
                    // Try send the chat message
                    netServer.Send(connectionId, MODULE_NAME, packedPacket.ToByteArray());
                }
                catch (NotConnectedException)
                {
                    // Report the failure
                    RaiseLogEntry(new LogEventArgs(string.Format("Tried sending a chat message to connection {0}, but it is closed", connectionId), LogLevel.DEBUG));
                }
            }
        }

        /// <summary>
        /// Sends a <c>ChatMessagePacket</c> to the user.
        /// </summary>
        /// <param name="connectionId">Destination connection ID</param>
        /// <param name="senderUuid">Sender's UUID</param>
        /// <param name="messageId">Message ID</param>
        /// <param name="message">Message content</param>
        /// <param name="timestamp">Timestamp</param>
        private void sendChatMessage(string connectionId, string senderUuid, string messageId, string message, DateTime timestamp)
        {
            // Create and pack our chat message packet
            ChatMessagePacket packet = new ChatMessagePacket
            {
                MessageId = messageId,
                Uuid = senderUuid,
                Message = message,
                Timestamp = timestamp.ToTimestamp()
            };
            Any packedPacket = ProtobufPacketHelper.Pack(packet);
            
            // Send it on its way
            netServer.Send(connectionId, MODULE_NAME, packedPacket.ToByteArray());
        }

        /// <summary>
        /// Adds the given <c>ChatMessage</c> to the message history.
        /// </summary>
        /// <param name="chatMessage"><c>ChatMessage</c> to store</param>
        private void addMessageToHistory(ChatMessage chatMessage)
        {
            lock (messageHistoryLock)
            {
                // Add the message to history
                messageHistory.Add(chatMessage);

                // Check if we've exceeded the history capacity
                if (messageHistory.Count > messageHistoryCapacity)
                {
                    // Remove the oldest message
                    messageHistory.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Creates and sends a <c>ChatMessageResponsePacket</c> to the given connection ID.
        /// </summary>
        /// <param name="connectionId">Destination connection ID</param>
        /// <param name="success">Whether the chat message was processed successfully</param>
        /// <param name="originalMessageId">Original message ID</param>
        /// <param name="newMessageId">Newly-generated message ID</param>
        /// <param name="message">Message content</param>
        private void sendChatMessageResponse(string connectionId, bool success, string originalMessageId, string newMessageId, string message)
        {
            // Prepare and pack a ChatMessageResponsePacket
            ChatMessageResponsePacket packet = new ChatMessageResponsePacket
            {
                Success = success,
                OriginalMessageId = originalMessageId,
                NewMessageId = newMessageId,
                Message = message
            };
            Any packedPacket = ProtobufPacketHelper.Pack(packet);
            
            // Send it on its way
            netServer.Send(connectionId, MODULE_NAME, packedPacket.ToByteArray());
        }
        
        /// <summary>
        /// Creates and sends a failed <c>ChatMessageResponsePacket</c> to the given connection ID.
        /// </summary>
        /// <param name="connectionId">Destination connection ID</param>
        /// <param name="originalMessageId">The original message ID</param>
        private void sendFailedChatMessageResponse(string connectionId, string originalMessageId)
        {
            sendChatMessageResponse(connectionId, false, originalMessageId, "", "");
        }
    }
}
