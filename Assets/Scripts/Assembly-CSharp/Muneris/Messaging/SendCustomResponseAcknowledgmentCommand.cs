using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendCustomResponseAcknowledgmentCommand")]
	public class SendCustomResponseAcknowledgmentCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendCustomResponseAcknowledgmentCommandBridge";

		protected SendCustomResponseAcknowledgmentCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public SendCustomResponseAcknowledgmentCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendCustomResponseAcknowledgmentCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomResponseAcknowledgmentCommand>(json);
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

		public CustomResponseAcknowledgment execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___CustomResponseAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomResponseAcknowledgment>(json);
		}

		public SendCustomResponseAcknowledgmentCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendCustomResponseAcknowledgmentCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomResponseAcknowledgmentCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
