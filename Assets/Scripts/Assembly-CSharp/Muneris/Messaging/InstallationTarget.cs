using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.InstallationTarget")]
	public class InstallationTarget : BridgeObject, IAddress, ITargetAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.InstallationTargetBridge";

		public InstallationTarget(string installationId)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "InstallationTarget____String", new object[1] { installationId });
			Init(num);
		}

		public InstallationTarget(string installId, IApp app)
			: base(0L)
		{
			string text = JsonHelper.Serialize(app);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "InstallationTarget____String_App", new object[2] { installId, text });
			Init(num);
		}

		protected InstallationTarget(ObjectId objectId)
			: base(objectId)
		{
		}

		public AddressType getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___AddressType", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<AddressType>(num);
		}

		public string getInstallId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getInstallId___String", new object[1] { GetObjectId() });
		}

		public InstallationTarget setApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setApp___InstallationTarget_App", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<InstallationTarget>(json);
		}

		public IApp getApp()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getApp___App", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<IApp>(json);
		}
	}
}
