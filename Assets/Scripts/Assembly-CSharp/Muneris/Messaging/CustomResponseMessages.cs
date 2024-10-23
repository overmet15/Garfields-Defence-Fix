using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomResponseMessages")]
	public class CustomResponseMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomResponseMessagesBridge";

		protected CustomResponseMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendCustomResponseMessageCommand send(ISendableAddress targetAddress, JsonObject body)
		{
			string text = JsonHelper.Serialize(targetAddress);
			string text2 = JsonHelper.Serialize(body);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendCustomResponseMessageCommand_SendableAddress_JSONObject", new object[2] { text, text2 });
			return JsonHelper.Deserialize<SendCustomResponseMessageCommand>(json);
		}

		public static FindCustomResponseMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindCustomResponseMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindCustomResponseMessagesCommand>(json);
		}
	}
}
