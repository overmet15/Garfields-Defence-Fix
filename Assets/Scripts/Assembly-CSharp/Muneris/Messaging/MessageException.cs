using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageException")]
	public class MessageException : MunerisException
	{
		public MessageException(string msg)
			: base(msg)
		{
		}

		protected MessageException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
