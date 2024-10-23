using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.AlertMessage")]
	public class AlertMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.AlertMessageBridge";

		protected AlertMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public string getText(string defaultString)
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String_String", new object[2]
			{
				GetObjectId(),
				defaultString
			});
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendAlertAcknowledgmentCommand sendAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendAlertAcknowledgmentCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SendAlertAcknowledgmentCommand>(json);
		}

		public AlertAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___AlertAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<AlertAcknowledgment>(json);
		}
	}
}
