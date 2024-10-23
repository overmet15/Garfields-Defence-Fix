using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.ChatMessage")]
	public class ChatMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChatMessageBridge";

		protected ChatMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public string getText(string defaultString)
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String_String", new object[2]
			{
				GetObjectId(),
				defaultString
			});
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendChatAcknowledgmentCommand sendAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendChatAcknowledgmentCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SendChatAcknowledgmentCommand>(json);
		}

		public ChatAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___ChatAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ChatAcknowledgment>(json);
		}
	}
}
