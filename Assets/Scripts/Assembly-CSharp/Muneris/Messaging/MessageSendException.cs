using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageSendException")]
	public class MessageSendException : MessageException
	{
		public MessageSendException(string msg)
			: base(msg)
		{
		}

		protected MessageSendException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
