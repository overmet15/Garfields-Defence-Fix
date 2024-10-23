using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverShowNotCalledException")]
	public class TakeoverShowNotCalledException : TakeoverException
	{
		public TakeoverShowNotCalledException(string msg)
			: base(msg)
		{
		}

		protected TakeoverShowNotCalledException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
