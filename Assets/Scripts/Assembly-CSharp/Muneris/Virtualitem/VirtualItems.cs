using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItems")]
	public class VirtualItems : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.VirtualItemsBridge";

		protected VirtualItems(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindVirtualItemsCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindVirtualItemsCommand", new object[0]);
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}
	}
}
