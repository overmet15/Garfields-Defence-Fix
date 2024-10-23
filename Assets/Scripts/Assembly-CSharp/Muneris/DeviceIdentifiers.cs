using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.DeviceIdentifiers")]
	public class DeviceIdentifiers : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.DeviceIdentifiersBridge";

		protected DeviceIdentifiers(ObjectId objectId)
			: base(objectId)
		{
		}

		public static DeviceIdentifier getInstallationId()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getInstallationId___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getTelephonyDeviceId()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTelephonyDeviceId___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getOdin1()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getOdin1___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getOpenUdid()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getOpenUdid___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getSerialNo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSerialNo___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getAndroidId()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAndroidId___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getMacAddress()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMacAddress___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getAdvertisingId()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAdvertisingId___DeviceIdentifier", new object[0]);
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}

		public static DeviceIdentifier getVendorId()
		{
			string json = null;
			return JsonHelper.Deserialize<DeviceIdentifier>(json);
		}
	}
}
