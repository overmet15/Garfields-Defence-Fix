using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendStatusAcknowledgmentCommand")]
	public class SendStatusAcknowledgmentCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendStatusAcknowledgmentCommandBridge";

		protected SendStatusAcknowledgmentCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public SendStatusAcknowledgmentCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendStatusAcknowledgmentCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendStatusAcknowledgmentCommand>(json);
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

		public StatusAcknowledgment execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___StatusAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<StatusAcknowledgment>(json);
		}

		public SendStatusAcknowledgmentCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendStatusAcknowledgmentCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendStatusAcknowledgmentCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
