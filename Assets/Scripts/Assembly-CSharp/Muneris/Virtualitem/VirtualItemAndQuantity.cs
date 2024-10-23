using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItemAndQuantity")]
	public class VirtualItemAndQuantity : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.VirtualItemAndQuantityBridge";

		public VirtualItemAndQuantity(VirtualItem virtualItem, int quantity)
			: base(0L)
		{
			string text = JsonHelper.Serialize(virtualItem);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "VirtualItemAndQuantity____VirtualItem_int", new object[2] { text, quantity });
			Init(num);
		}

		protected VirtualItemAndQuantity(ObjectId objectId)
			: base(objectId)
		{
		}

		public VirtualItem getVirtualItem()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItem___VirtualItem", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItem>(json);
		}

		public int getQuantity()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getQuantity___int", new object[1] { GetObjectId() });
		}
	}
}
