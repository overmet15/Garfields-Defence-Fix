using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.AppStoreSkuNotAvailableException")]
	public class AppStoreSkuNotAvailableException : VirtualGoodsException
	{
		public AppStoreSkuNotAvailableException(string msg)
			: base(msg)
		{
		}

		protected AppStoreSkuNotAvailableException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
