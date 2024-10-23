using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AppStore")]
	public class AppStore : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.AppStoreBridge";

		protected AppStore(ObjectId objectId)
			: base(objectId)
		{
		}

		public static void login()
		{
			JniHelper.CallStatic(_bridgeClassName, "login___void");
		}

		public static void logout()
		{
			JniHelper.CallStatic(_bridgeClassName, "logout___void");
		}

		public static string getAppStoreId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getAppStoreId___String", new object[0]);
		}

		public static string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[0]);
		}

		public static bool supportsLoginLogout()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "supportsLoginLogout___boolean", new object[0]);
		}

		public static void launchApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			JniHelper.CallStatic(_bridgeClassName, "launchApp___void_App", text);
		}
	}
}
