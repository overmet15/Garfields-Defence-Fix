using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageHandlingException")]
	public class MessageHandlingException : MessageException
	{
		public MessageHandlingException(string msg)
			: base(msg)
		{
		}

		protected MessageHandlingException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
