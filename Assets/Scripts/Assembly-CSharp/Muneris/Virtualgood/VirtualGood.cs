using System.Collections.Generic;
using Muneris.Bridge;
using Muneris.Virtualitem;
using Muneris.Virtualitem.Util;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGood")]
	public class VirtualGood : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.VirtualGoodBridge";

		protected VirtualGood(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getVirtualGoodId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualGoodId___String", new object[1] { GetObjectId() });
		}

		public string getSku()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getSku___String", new object[1] { GetObjectId() });
		}

		public VirtualItemBundle getVirtualItemBundle()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemBundle___VirtualItemBundle", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundle>(json);
		}

		public string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[1] { GetObjectId() });
		}

		public string getDescription()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getDescription___String", new object[1] { GetObjectId() });
		}

		public PriceAndCurrency getPriceAndCurrency()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getPriceAndCurrency___PriceAndCurrency", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<PriceAndCurrency>(json);
		}

		public List<string> getCategories()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCategories___ArrayList", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public ImageValue getImage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getImage___ImageValue", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ImageValue>(json);
		}

		public AppStoreLocalizedData getAppStoreLocalizedData()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAppStoreLocalizedData___AppStoreLocalizedData", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<AppStoreLocalizedData>(json);
		}

		public List<VirtualGoodAnnotation> getAnnotations()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAnnotations___ArrayList", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<VirtualGoodAnnotation>>(json);
		}

		public int getSeqNo()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getSeqNo___int", new object[1] { GetObjectId() });
		}

		public bool isVariableQuantity()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isVariableQuantity___boolean", new object[1] { GetObjectId() });
		}

		public bool isVisible()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isVisible___boolean", new object[1] { GetObjectId() });
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public PurchaseVirtualGoodCommand purchase()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "purchase___PurchaseVirtualGoodCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<PurchaseVirtualGoodCommand>(json);
		}
	}
}
