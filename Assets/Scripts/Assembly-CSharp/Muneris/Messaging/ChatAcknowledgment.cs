using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.ChatAcknowledgment")]
	public class ChatAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChatAcknowledgmentBridge";

		public ChatAcknowledgment(JsonObject json, ChatMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "ChatAcknowledgment____JSONObject_ChatMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected ChatAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public ChatMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___ChatMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ChatMessage>(json);
		}
	}
}
