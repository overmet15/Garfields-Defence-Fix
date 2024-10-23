using Muneris.Bridge;

namespace Muneris.Pushnotification
{
	[BridgeObjectInfo(NativeClass = "muneris.android.pushnotification.PushNotifications")]
	public class PushNotifications : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.pushnotification.PushNotificationsBridge";

		protected PushNotifications(ObjectId objectId)
			: base(objectId)
		{
		}

		public static RegisterPushNotificationCommand registerDevice()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "registerDevice___RegisterPushNotificationCommand", new object[0]);
			return JsonHelper.Deserialize<RegisterPushNotificationCommand>(json);
		}

		public static UnregisterPushNotificationCommand unregisterDevice()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "unregisterDevice___UnregisterPushNotificationCommand", new object[0]);
			return JsonHelper.Deserialize<UnregisterPushNotificationCommand>(json);
		}

		public static string getRegistrationId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getRegistrationId___String", new object[0]);
		}
	}
}
