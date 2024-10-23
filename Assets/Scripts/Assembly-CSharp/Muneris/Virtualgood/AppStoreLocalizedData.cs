using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.AppStoreLocalizedData")]
	public class AppStoreLocalizedData : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.AppStoreLocalizedDataBridge";

		public AppStoreLocalizedData(JsonObject info)
			: base(0L)
		{
			string text = JsonHelper.Serialize(info);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "AppStoreLocalizedData____JSONObject", new object[1] { text });
			Init(num);
		}

		protected AppStoreLocalizedData(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getProductTitle()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getProductTitle___String", new object[1] { GetObjectId() });
		}

		public PriceAndCurrency getPriceAndCurrency()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getPriceAndCurrency___PriceAndCurrency", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<PriceAndCurrency>(json);
		}
	}
}
