using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverFeatureNotSupportedException")]
	public class TakeoverFeatureNotSupportedException : TakeoverException
	{
		public TakeoverFeatureNotSupportedException(string msg)
			: base(msg)
		{
		}

		protected TakeoverFeatureNotSupportedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
