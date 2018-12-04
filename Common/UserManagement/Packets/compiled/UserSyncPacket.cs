// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Packets/UserSyncPacket.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace UserManagement {

  /// <summary>Holder for reflection information generated from Packets/UserSyncPacket.proto</summary>
  public static partial class UserSyncPacketReflection {

    #region Descriptor
    /// <summary>File descriptor for Packets/UserSyncPacket.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static UserSyncPacketReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChxQYWNrZXRzL1VzZXJTeW5jUGFja2V0LnByb3RvEg5Vc2VyTWFuYWdlbWVu",
            "dBoQVXNlcnMvVXNlci5wcm90byI1Cg5Vc2VyU3luY1BhY2tldBIjCgVVc2Vy",
            "cxgBIAMoCzIULlVzZXJNYW5hZ2VtZW50LlVzZXJiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::UserManagement.UserReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::UserManagement.UserSyncPacket), global::UserManagement.UserSyncPacket.Parser, new[]{ "Users" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UserSyncPacket : pb::IMessage<UserSyncPacket> {
    private static readonly pb::MessageParser<UserSyncPacket> _parser = new pb::MessageParser<UserSyncPacket>(() => new UserSyncPacket());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserSyncPacket> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::UserManagement.UserSyncPacketReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserSyncPacket() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserSyncPacket(UserSyncPacket other) : this() {
      users_ = other.users_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UserSyncPacket Clone() {
      return new UserSyncPacket(this);
    }

    /// <summary>Field number for the "Users" field.</summary>
    public const int UsersFieldNumber = 1;
    private static readonly pb::FieldCodec<global::UserManagement.User> _repeated_users_codec
        = pb::FieldCodec.ForMessage(10, global::UserManagement.User.Parser);
    private readonly pbc::RepeatedField<global::UserManagement.User> users_ = new pbc::RepeatedField<global::UserManagement.User>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::UserManagement.User> Users {
      get { return users_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UserSyncPacket);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UserSyncPacket other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!users_.Equals(other.users_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= users_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      users_.WriteTo(output, _repeated_users_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += users_.CalculateSize(_repeated_users_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UserSyncPacket other) {
      if (other == null) {
        return;
      }
      users_.Add(other.users_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            users_.AddEntriesFrom(input, _repeated_users_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code