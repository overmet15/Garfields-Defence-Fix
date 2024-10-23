using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageResendException")]
	public class MessageResendException : MessageSendException
	{
		public MessageResendException(string msg)
			: base(msg)
		{
		}

		protected MessageResendException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
