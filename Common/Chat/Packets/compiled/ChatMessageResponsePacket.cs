// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Packets/ChatMessageResponsePacket.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Chat {

  /// <summary>Holder for reflection information generated from Packets/ChatMessageResponsePacket.proto</summary>
  public static partial class ChatMessageResponsePacketReflection {

    #region Descriptor
    /// <summary>File descriptor for Packets/ChatMessageResponsePacket.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ChatMessageResponsePacketReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CidQYWNrZXRzL0NoYXRNZXNzYWdlUmVzcG9uc2VQYWNrZXQucHJvdG8SBENo",
            "YXQibgoZQ2hhdE1lc3NhZ2VSZXNwb25zZVBhY2tldBIPCgdTdWNjZXNzGAEg",
            "ASgIEhkKEU9yaWdpbmFsTWVzc2FnZUlkGAIgASgJEhQKDE5ld01lc3NhZ2VJ",
            "ZBgDIAEoCRIPCgdNZXNzYWdlGAQgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Chat.ChatMessageResponsePacket), global::Chat.ChatMessageResponsePacket.Parser, new[]{ "Success", "OriginalMessageId", "NewMessageId", "Message" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ChatMessageResponsePacket : pb::IMessage<ChatMessageResponsePacket> {
    private static readonly pb::MessageParser<ChatMessageResponsePacket> _parser = new pb::MessageParser<ChatMessageResponsePacket>(() => new ChatMessageResponsePacket());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ChatMessageResponsePacket> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Chat.ChatMessageResponsePacketReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessageResponsePacket() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessageResponsePacket(ChatMessageResponsePacket other) : this() {
      success_ = other.success_;
      originalMessageId_ = other.originalMessageId_;
      newMessageId_ = other.newMessageId_;
      message_ = other.message_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessageResponsePacket Clone() {
      return new ChatMessageResponsePacket(this);
    }

    /// <summary>Field number for the "Success" field.</summary>
    public const int SuccessFieldNumber = 1;
    private bool success_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Success {
      get { return success_; }
      set {
        success_ = value;
      }
    }

    /// <summary>Field number for the "OriginalMessageId" field.</summary>
    public const int OriginalMessageIdFieldNumber = 2;
    private string originalMessageId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string OriginalMessageId {
      get { return originalMessageId_; }
      set {
        originalMessageId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "NewMessageId" field.</summary>
    public const int NewMessageIdFieldNumber = 3;
    private string newMessageId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string NewMessageId {
      get { return newMessageId_; }
      set {
        newMessageId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Message" field.</summary>
    public const int MessageFieldNumber = 4;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ChatMessageResponsePacket);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ChatMessageResponsePacket other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Success != other.Success) return false;
      if (OriginalMessageId != other.OriginalMessageId) return false;
      if (NewMessageId != other.NewMessageId) return false;
      if (Message != other.Message) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Success != false) hash ^= Success.GetHashCode();
      if (OriginalMessageId.Length != 0) hash ^= OriginalMessageId.GetHashCode();
      if (NewMessageId.Length != 0) hash ^= NewMessageId.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Success != false) {
        output.WriteRawTag(8);
        output.WriteBool(Success);
      }
      if (OriginalMessageId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(OriginalMessageId);
      }
      if (NewMessageId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(NewMessageId);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Message);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Success != false) {
        size += 1 + 1;
      }
      if (OriginalMessageId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(OriginalMessageId);
      }
      if (NewMessageId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(NewMessageId);
      }
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ChatMessageResponsePacket other) {
      if (other == null) {
        return;
      }
      if (other.Success != false) {
        Success = other.Success;
      }
      if (other.OriginalMessageId.Length != 0) {
        OriginalMessageId = other.OriginalMessageId;
      }
      if (other.NewMessageId.Length != 0) {
        NewMessageId = other.NewMessageId;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Success = input.ReadBool();
            break;
          }
          case 18: {
            OriginalMessageId = input.ReadString();
            break;
          }
          case 26: {
            NewMessageId = input.ReadString();
            break;
          }
          case 34: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
