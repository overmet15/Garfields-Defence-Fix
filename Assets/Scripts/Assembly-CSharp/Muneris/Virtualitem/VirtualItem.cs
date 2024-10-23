using Muneris.Bridge;
using Muneris.Virtualitem.Util;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItem")]
	public class VirtualItem : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.VirtualItemBridge";

		protected VirtualItem(ObjectId objectId)
			: base(objectId)
		{
		}

		public ImageValue getImage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getImage___ImageValue", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ImageValue>(json);
		}

		public string getVirtualItemId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemId___String", new object[1] { GetObjectId() });
		}

		public VirtualItemType getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___VirtualItemType", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<VirtualItemType>(num);
		}

		public string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[1] { GetObjectId() });
		}

		public string getDescription()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getDescription___String", new object[1] { GetObjectId() });
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}
	}
}
