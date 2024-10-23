using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageBodyEmptyException")]
	public class MessageBodyEmptyException : MessageInvalidException
	{
		public MessageBodyEmptyException(string msg)
			: base(msg)
		{
		}

		protected MessageBodyEmptyException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
