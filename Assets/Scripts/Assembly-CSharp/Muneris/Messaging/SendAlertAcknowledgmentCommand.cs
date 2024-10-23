using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendAlertAcknowledgmentCommand")]
	public class SendAlertAcknowledgmentCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendAlertAcknowledgmentCommandBridge";

		protected SendAlertAcknowledgmentCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public SendAlertAcknowledgmentCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendAlertAcknowledgmentCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendAlertAcknowledgmentCommand>(json);
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
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

		public AlertAcknowledgment execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___AlertAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<AlertAcknowledgment>(json);
		}

		public SendAlertAcknowledgmentCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendAlertAcknowledgmentCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendAlertAcknowledgmentCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
