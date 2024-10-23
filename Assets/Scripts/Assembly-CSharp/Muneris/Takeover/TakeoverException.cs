using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverException")]
	public class TakeoverException : MunerisException
	{
		public TakeoverException(string msg)
			: base(msg)
		{
		}

		protected TakeoverException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
