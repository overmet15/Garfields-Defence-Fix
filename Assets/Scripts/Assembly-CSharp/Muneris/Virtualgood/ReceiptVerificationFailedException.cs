using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.ReceiptVerificationFailedException")]
	public class ReceiptVerificationFailedException : VirtualGoodsException
	{
		public ReceiptVerificationFailedException(string msg)
			: base(msg)
		{
		}

		protected ReceiptVerificationFailedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
