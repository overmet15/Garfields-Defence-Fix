using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoodAlreadyOwnedException")]
	public class VirtualGoodAlreadyOwnedException : VirtualGoodsException
	{
		public VirtualGoodAlreadyOwnedException(string msg)
			: base(msg)
		{
		}

		protected VirtualGoodAlreadyOwnedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
