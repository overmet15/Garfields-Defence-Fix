using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageInvalidException")]
	public class MessageInvalidException : MessageException
	{
		public MessageInvalidException(string msg)
			: base(msg)
		{
		}

		protected MessageInvalidException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
