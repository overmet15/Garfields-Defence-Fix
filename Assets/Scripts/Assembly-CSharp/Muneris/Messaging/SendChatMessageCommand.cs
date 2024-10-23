using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendChatMessageCommand")]
	public class SendChatMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendChatMessageCommandBridge";

		protected SendChatMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public SendChatMessageCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendChatMessageCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendChatMessageCommand>(json);
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendChatMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendChatMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendChatMessageCommand>(json);
		}

		public SendChatMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendChatMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendChatMessageCommand>(json);
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

		public ChatMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___ChatMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ChatMessage>(json);
		}

		public SendChatMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendChatMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendChatMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
