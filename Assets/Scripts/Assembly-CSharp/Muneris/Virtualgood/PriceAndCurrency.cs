using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PriceAndCurrency")]
	public class PriceAndCurrency : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.PriceAndCurrencyBridge";

		protected PriceAndCurrency(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getPrice()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getPrice___String", new object[1] { GetObjectId() });
		}

		public string getCurrency()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getCurrency___String", new object[1] { GetObjectId() });
		}

		public double getValue()
		{
			return JniHelper.CallStatic<double>(_bridgeClassName, "getValue___double", new object[1] { GetObjectId() });
		}

		public bool hasValue()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "hasValue___boolean", new object[1] { GetObjectId() });
		}
	}
}
