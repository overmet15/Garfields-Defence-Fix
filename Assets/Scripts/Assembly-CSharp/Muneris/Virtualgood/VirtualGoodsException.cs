using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoodsException")]
	public class VirtualGoodsException : MunerisException
	{
		public VirtualGoodsException(string msg)
			: base(msg)
		{
		}

		protected VirtualGoodsException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
