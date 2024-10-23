using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PurchaseFailedException")]
	public class PurchaseFailedException : VirtualGoodsException
	{
		public PurchaseFailedException(string msg)
			: base(msg)
		{
		}

		protected PurchaseFailedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
