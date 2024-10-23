using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverWebviewApiFailedException")]
	public class TakeoverWebviewApiFailedException : TakeoverWebviewException
	{
		public TakeoverWebviewApiFailedException(string msg)
			: base(msg)
		{
		}

		protected TakeoverWebviewApiFailedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
