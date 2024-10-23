using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageSyncException")]
	public class MessageSyncException : MessageException
	{
		public MessageSyncException(string msg)
			: base(msg)
		{
		}

		protected MessageSyncException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
