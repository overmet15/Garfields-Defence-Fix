using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageSourceIdNotFoundException")]
	public class MessageSourceIdNotFoundException : MessageInvalidException
	{
		public MessageSourceIdNotFoundException(string msg)
			: base(msg)
		{
		}

		protected MessageSourceIdNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
