using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomRequestMessage")]
	public class CustomRequestMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomRequestMessageBridge";

		protected CustomRequestMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendCustomRequestAcknowledgmentCommand sendAcknowledgment(string acknowledgmentType)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendCustomRequestAcknowledgmentCommand_String", new object[2]
			{
				GetObjectId(),
				acknowledgmentType
			});
			return JsonHelper.Deserialize<SendCustomRequestAcknowledgmentCommand>(json);
		}

		public CustomRequestAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___CustomRequestAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomRequestAcknowledgment>(json);
		}
	}
}
