using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomMessage")]
	public class CustomMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomMessageBridge";

		protected CustomMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendCustomAcknowledgmentCommand sendAcknowledgment(string acknowledgmentType)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendCustomAcknowledgmentCommand_String", new object[2]
			{
				GetObjectId(),
				acknowledgmentType
			});
			return JsonHelper.Deserialize<SendCustomAcknowledgmentCommand>(json);
		}

		public CustomAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___CustomAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomAcknowledgment>(json);
		}
	}
}
