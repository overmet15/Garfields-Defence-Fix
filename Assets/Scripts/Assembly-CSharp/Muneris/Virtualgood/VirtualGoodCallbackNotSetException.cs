using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoodCallbackNotSetException")]
	public class VirtualGoodCallbackNotSetException : VirtualGoodsException
	{
		public VirtualGoodCallbackNotSetException(string msg)
			: base(msg)
		{
		}

		protected VirtualGoodCallbackNotSetException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
