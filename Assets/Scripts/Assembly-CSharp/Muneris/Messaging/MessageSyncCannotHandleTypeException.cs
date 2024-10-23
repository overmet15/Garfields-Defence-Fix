using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageSyncCannotHandleTypeException")]
	public class MessageSyncCannotHandleTypeException : MessageSyncException
	{
		public MessageSyncCannotHandleTypeException(string msg)
			: base(msg)
		{
		}

		protected MessageSyncCannotHandleTypeException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
