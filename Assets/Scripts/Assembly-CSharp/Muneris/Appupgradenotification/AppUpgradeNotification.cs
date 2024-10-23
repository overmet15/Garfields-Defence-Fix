using Muneris.Bridge;

namespace Muneris.Appupgradenotification
{
	[BridgeObjectInfo(NativeClass = "muneris.android.appupgradenotification.AppUpgradeNotification")]
	public class AppUpgradeNotification : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.appupgradenotification.AppUpgradeNotificationBridge";

		protected AppUpgradeNotification(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public bool isCritical()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isCritical___boolean", new object[1] { GetObjectId() });
		}

		public void gotoAppStore()
		{
			JniHelper.CallStatic(_bridgeClassName, "gotoAppStore___void", GetObjectId());
		}
	}
}
