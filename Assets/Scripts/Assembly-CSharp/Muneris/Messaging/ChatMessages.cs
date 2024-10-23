using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.ChatMessages")]
	public class ChatMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChatMessagesBridge";

		protected ChatMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendChatMessageCommand send(ISendableAddress targetAddress, string text)
		{
			string text2 = JsonHelper.Serialize(targetAddress);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendChatMessageCommand_SendableAddress_String", new object[2] { text2, text });
			return JsonHelper.Deserialize<SendChatMessageCommand>(json);
		}

		public static FindChatMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindChatMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindChatMessagesCommand>(json);
		}
	}
}
