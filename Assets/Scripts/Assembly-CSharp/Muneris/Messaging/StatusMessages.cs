using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.StatusMessages")]
	public class StatusMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.StatusMessagesBridge";

		protected StatusMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static SendStatusMessageCommand send(ISendableAddress targetAddress, string text)
		{
			string text2 = JsonHelper.Serialize(targetAddress);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendStatusMessageCommand_SendableAddress_String", new object[2] { text2, text });
			return JsonHelper.Deserialize<SendStatusMessageCommand>(json);
		}

		public static FindStatusMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindStatusMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindStatusMessagesCommand>(json);
		}
	}
}
