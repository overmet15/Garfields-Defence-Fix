using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItemBundle")]
	public class VirtualItemBundle : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.VirtualItemBundleBridge";

		public VirtualItemBundle(List<VirtualItemAndQuantity> virtualItemAndQuantities)
			: base(0L)
		{
			string text = JsonHelper.Serialize(virtualItemAndQuantities);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "VirtualItemBundle____ArrayList", new object[1] { text });
			Init(num);
		}

		protected VirtualItemBundle(ObjectId objectId)
			: base(objectId)
		{
		}

		public List<VirtualItemAndQuantity> getVirtualItemAndQuantities()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemAndQuantities___ArrayList", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<VirtualItemAndQuantity>>(json);
		}
	}
}
