using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.AlertMessages")]
	public class AlertMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.AlertMessagesBridge";

		protected AlertMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendAlertMessageCommand send(ISendableAddress targetAddress, string text)
		{
			string text2 = JsonHelper.Serialize(targetAddress);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendAlertMessageCommand_SendableAddress_String", new object[2] { text2, text });
			return JsonHelper.Deserialize<SendAlertMessageCommand>(json);
		}

		public static FindAlertMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindAlertMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindAlertMessagesCommand>(json);
		}
	}
}
