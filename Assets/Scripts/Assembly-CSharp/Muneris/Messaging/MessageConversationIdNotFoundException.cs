using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageConversationIdNotFoundException")]
	public class MessageConversationIdNotFoundException : MessageInvalidException
	{
		public MessageConversationIdNotFoundException(string msg)
			: base(msg)
		{
		}

		protected MessageConversationIdNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
