using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.InvalidPaymentException")]
	public class InvalidPaymentException : VirtualGoodsException
	{
		public InvalidPaymentException(string msg)
			: base(msg)
		{
		}

		protected InvalidPaymentException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
