using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PurchaseDeferredException")]
	public class PurchaseDeferredException : VirtualGoodsException
	{
		public PurchaseDeferredException(string msg)
			: base(msg)
		{
		}

		protected PurchaseDeferredException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
