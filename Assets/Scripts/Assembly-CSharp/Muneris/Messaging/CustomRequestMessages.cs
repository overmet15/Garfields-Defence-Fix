using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomRequestMessages")]
	public class CustomRequestMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomRequestMessagesBridge";

		protected CustomRequestMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendCustomRequestMessageCommand send(ISendableAddress targetAddress, JsonObject body)
		{
			string text = JsonHelper.Serialize(targetAddress);
			string text2 = JsonHelper.Serialize(body);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendCustomRequestMessageCommand_SendableAddress_JSONObject", new object[2] { text, text2 });
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public static FindCustomRequestMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindCustomRequestMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindCustomRequestMessagesCommand>(json);
		}
	}
}
