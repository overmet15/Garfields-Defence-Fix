using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PurchaseCancelledException")]
	public class PurchaseCancelledException : VirtualGoodsException
	{
		public PurchaseCancelledException(string msg)
			: base(msg)
		{
		}

		protected PurchaseCancelledException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
