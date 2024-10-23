using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.AppStoreNotAvailableException")]
	public class AppStoreNotAvailableException : VirtualGoodsException
	{
		public AppStoreNotAvailableException(string msg)
			: base(msg)
		{
		}

		protected AppStoreNotAvailableException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
