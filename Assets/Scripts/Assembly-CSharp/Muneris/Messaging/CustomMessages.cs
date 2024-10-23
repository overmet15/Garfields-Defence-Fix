using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomMessages")]
	public class CustomMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomMessagesBridge";

		protected CustomMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendCustomMessageCommand send(ISendableAddress targetAddress, JsonObject body)
		{
			string text = JsonHelper.Serialize(targetAddress);
			string text2 = JsonHelper.Serialize(body);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendCustomMessageCommand_SendableAddress_JSONObject", new object[2] { text, text2 });
			return JsonHelper.Deserialize<SendCustomMessageCommand>(json);
		}

		public static FindCustomMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindCustomMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindCustomMessagesCommand>(json);
		}
	}
}
