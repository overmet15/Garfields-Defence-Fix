using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendStatusMessageCommand")]
	public class SendStatusMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendStatusMessageCommandBridge";

		protected SendStatusMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public SendStatusMessageCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendStatusMessageCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendStatusMessageCommand>(json);
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendStatusMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendStatusMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendStatusMessageCommand>(json);
		}

		public SendStatusMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendStatusMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendStatusMessageCommand>(json);
		}

		public long getExpiry()
		{
			return JniHelper.CallStatic<long>(_bridgeClassName, "getExpiry___long", new object[1] { GetObjectId() });
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

		public StatusMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___StatusMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<StatusMessage>(json);
		}

		public SendStatusMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendStatusMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendStatusMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
