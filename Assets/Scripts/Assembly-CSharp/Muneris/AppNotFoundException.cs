using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AppNotFoundException")]
	public class AppNotFoundException : MunerisException
	{
		public AppNotFoundException(string msg)
			: base(msg)
		{
		}

		protected AppNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
