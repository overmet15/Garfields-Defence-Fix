using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageTargetIdNotFoundException")]
	public class MessageTargetIdNotFoundException : MessageInvalidException
	{
		public MessageTargetIdNotFoundException(string msg)
			: base(msg)
		{
		}

		protected MessageTargetIdNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
