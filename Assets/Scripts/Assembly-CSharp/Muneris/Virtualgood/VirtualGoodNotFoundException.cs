using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoodNotFoundException")]
	public class VirtualGoodNotFoundException : VirtualGoodsException
	{
		public VirtualGoodNotFoundException(string msg)
			: base(msg)
		{
		}

		protected VirtualGoodNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
