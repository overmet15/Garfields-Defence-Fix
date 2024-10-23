using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AccessBannedException")]
	public class AccessBannedException : AccessDeniedException
	{
		public AccessBannedException(string msg)
			: base(msg)
		{
		}

		protected AccessBannedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
