using Muneris.Bridge;

namespace Muneris.Pushnotification
{
	[BridgeObjectInfo(NativeClass = "muneris.android.pushnotification.UnregisterPushNotificationCommand")]
	public class UnregisterPushNotificationCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.pushnotification.UnregisterPushNotificationCommandBridge";

		protected UnregisterPushNotificationCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public UnregisterPushNotificationCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___UnregisterPushNotificationCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<UnregisterPushNotificationCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}

		public void validate()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "validate___void", new object[1] { GetObjectId() });
			BridgeResult<object> bridgeResult = JsonHelper.DeserializeBridgeResult<object>(json);
			if (bridgeResult.Kind == BridgeResult<object>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
		}
	}
}
