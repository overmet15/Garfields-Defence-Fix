using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageParamNullException")]
	public class MessageParamNullException : MessageInvalidException
	{
		public MessageParamNullException(string msg)
			: base(msg)
		{
		}

		protected MessageParamNullException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
