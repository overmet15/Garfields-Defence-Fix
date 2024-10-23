using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.InvalidMessageException")]
	public class InvalidMessageException : MessageException
	{
		public InvalidMessageException(string msg)
			: base(msg)
		{
		}

		protected InvalidMessageException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
