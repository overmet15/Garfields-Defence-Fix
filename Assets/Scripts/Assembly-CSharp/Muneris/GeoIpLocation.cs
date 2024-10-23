using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.GeoIpLocation")]
	public class GeoIpLocation : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.GeoIpLocationBridge";

		public GeoIpLocation()
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "GeoIpLocation___void", new object[0]);
			Init(num);
		}

		public GeoIpLocation(string country, string city, string region)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "GeoIpLocation____String_String_String", new object[3] { country, city, region });
			Init(num);
		}

		protected GeoIpLocation(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getCountry()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getCountry___String", new object[1] { GetObjectId() });
		}

		public string getCity()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getCity___String", new object[1] { GetObjectId() });
		}

		public string getRegion()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getRegion___String", new object[1] { GetObjectId() });
		}
	}
}
