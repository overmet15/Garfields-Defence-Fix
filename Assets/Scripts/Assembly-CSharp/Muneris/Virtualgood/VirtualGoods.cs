using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoods")]
	public class VirtualGoods : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.VirtualGoodsBridge";

		protected VirtualGoods(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindVirtualGoodsCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindVirtualGoodsCommand", new object[0]);
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public static RestoreVirtualGoodsCommand restore()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "restore___RestoreVirtualGoodsCommand", new object[0]);
			return JsonHelper.Deserialize<RestoreVirtualGoodsCommand>(json);
		}
	}
}
