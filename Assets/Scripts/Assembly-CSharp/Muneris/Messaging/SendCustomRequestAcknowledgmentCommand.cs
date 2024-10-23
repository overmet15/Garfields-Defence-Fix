using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendCustomRequestAcknowledgmentCommand")]
	public class SendCustomRequestAcknowledgmentCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendCustomRequestAcknowledgmentCommandBridge";

		protected SendCustomRequestAcknowledgmentCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public SendCustomRequestAcknowledgmentCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendCustomRequestAcknowledgmentCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomRequestAcknowledgmentCommand>(json);
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

		public CustomRequestAcknowledgment execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___CustomRequestAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomRequestAcknowledgment>(json);
		}

		public SendCustomRequestAcknowledgmentCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendCustomRequestAcknowledgmentCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomRequestAcknowledgmentCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
