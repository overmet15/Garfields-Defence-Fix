using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverCustomViewLoadException")]
	public class TakeoverCustomViewLoadException : TakeoverException
	{
		public TakeoverCustomViewLoadException(string msg)
			: base(msg)
		{
		}

		protected TakeoverCustomViewLoadException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
