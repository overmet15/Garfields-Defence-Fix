using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AccessDeniedException")]
	public class AccessDeniedException : MunerisException
	{
		public AccessDeniedException(string msg)
			: base(msg)
		{
		}

		protected AccessDeniedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
