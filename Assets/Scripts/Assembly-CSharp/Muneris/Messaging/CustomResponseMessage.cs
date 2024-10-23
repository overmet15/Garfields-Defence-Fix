using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomResponseMessage")]
	public class CustomResponseMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomResponseMessageBridge";

		protected CustomResponseMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public string getRequestAcknowledgmentType()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getRequestAcknowledgmentType___String", new object[1] { GetObjectId() });
		}

		public SendCustomResponseAcknowledgmentCommand sendAcknowledgment(string acknowledgmentType)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendCustomResponseAcknowledgmentCommand_String", new object[2]
			{
				GetObjectId(),
				acknowledgmentType
			});
			return JsonHelper.Deserialize<SendCustomResponseAcknowledgmentCommand>(json);
		}

		public CustomResponseAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___CustomResponseAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomResponseAcknowledgment>(json);
		}
	}
}
