using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.DeviceIdentifier")]
	public class DeviceIdentifier : BridgeObject
	{
		public enum Type
		{
			InstallId = 0,
			TelephonyDeviceId = 1,
			Odin1 = 2,
			OpenUdid = 3,
			AndroidId = 4,
			AdvertisingId = 5,
			MacAddress = 6,
			SerialNo = 7,
			VendorId = 8
		}

		private static string _bridgeClassName = "muneris.bridge.DeviceIdentifierBridge";

		public DeviceIdentifier(Type type, string id)
			: base(0L)
		{
			int num = SerializationHelper.Serialize(type);
			long num2 = JniHelper.CallStatic<long>(_bridgeClassName, "DeviceIdentifier____DeviceIdentifier_Type_String", new object[2] { num, id });
			Init(num2);
		}

		protected DeviceIdentifier(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getId___String", new object[1] { GetObjectId() });
		}

		public Type getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___DeviceIdentifier_Type", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Type>(num);
		}

		public bool isEmpty()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isEmpty___boolean", new object[1] { GetObjectId() });
		}
	}
}
