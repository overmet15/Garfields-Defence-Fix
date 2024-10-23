using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverConfigException")]
	public class TakeoverConfigException : TakeoverException
	{
		public TakeoverConfigException(string msg)
			: base(msg)
		{
		}

		protected TakeoverConfigException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
