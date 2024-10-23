using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverWebviewException")]
	public class TakeoverWebviewException : TakeoverException
	{
		public TakeoverWebviewException(string msg)
			: base(msg)
		{
		}

		protected TakeoverWebviewException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
