using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendCustomMessageCommand")]
	public class SendCustomMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendCustomMessageCommandBridge";

		protected SendCustomMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendCustomMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendCustomMessageCommand>(json);
		}

		public SendCustomMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendCustomMessageCommand>(json);
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

		public CustomMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___CustomMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomMessage>(json);
		}

		public SendCustomMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendCustomMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
