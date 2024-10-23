using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverWebviewUnknownMethodException")]
	public class TakeoverWebviewUnknownMethodException : TakeoverWebviewException
	{
		public TakeoverWebviewUnknownMethodException(string msg)
			: base(msg)
		{
		}

		protected TakeoverWebviewUnknownMethodException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
