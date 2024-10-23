using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SystemTarget")]
	public class SystemTarget : BridgeObject, IAddress, ITargetAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SystemTargetBridge";

		public SystemTarget(string systemId)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "SystemTarget____String", new object[1] { systemId });
			Init(num);
		}

		public SystemTarget(string systemId, string installId, IApp app)
			: base(0L)
		{
			string text = JsonHelper.Serialize(app);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "SystemTarget____String_String_App", new object[3] { systemId, installId, text });
			Init(num);
		}

		protected SystemTarget(ObjectId objectId)
			: base(objectId)
		{
		}

		public AddressType getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___AddressType", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<AddressType>(num);
		}

		public string getSystemId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getSystemId___String", new object[1] { GetObjectId() });
		}

		public SystemTarget setApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setApp___SystemTarget_App", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SystemTarget>(json);
		}

		public string getInstallId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getInstallId___String", new object[1] { GetObjectId() });
		}

		public IApp getApp()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getApp___App", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<IApp>(json);
		}
	}
}
