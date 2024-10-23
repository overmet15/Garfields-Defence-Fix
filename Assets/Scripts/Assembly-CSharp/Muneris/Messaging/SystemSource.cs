using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SystemSource")]
	public class SystemSource : BridgeObject, IAddress, ISourceAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SystemSourceBridge";

		protected SystemSource(ObjectId objectId)
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

		public SystemSource setApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setApp___SystemSource_App", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SystemSource>(json);
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
