using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverOpenInExternalBrowserException")]
	public class TakeoverOpenInExternalBrowserException : TakeoverException
	{
		public TakeoverOpenInExternalBrowserException(string msg)
			: base(msg)
		{
		}

		protected TakeoverOpenInExternalBrowserException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
