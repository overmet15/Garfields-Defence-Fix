using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageTargetTypeNotFoundException")]
	public class MessageTargetTypeNotFoundException : MessageInvalidException
	{
		public MessageTargetTypeNotFoundException(string msg)
			: base(msg)
		{
		}

		protected MessageTargetTypeNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
