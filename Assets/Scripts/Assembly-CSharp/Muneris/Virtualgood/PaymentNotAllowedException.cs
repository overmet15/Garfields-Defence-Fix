using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PaymentNotAllowedException")]
	public class PaymentNotAllowedException : VirtualGoodsException
	{
		public PaymentNotAllowedException(string msg)
			: base(msg)
		{
		}

		protected PaymentNotAllowedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
