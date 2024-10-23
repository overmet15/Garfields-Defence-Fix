using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.UnsupportedMessageException")]
	public class UnsupportedMessageException : MessageException
	{
		public UnsupportedMessageException(string msg)
			: base(msg)
		{
		}

		protected UnsupportedMessageException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
