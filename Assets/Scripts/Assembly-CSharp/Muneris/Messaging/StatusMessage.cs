using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.StatusMessage")]
	public class StatusMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.StatusMessageBridge";

		protected StatusMessage(ObjectId objectId)
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

		public SendStatusAcknowledgmentCommand sendAcknowledgment(string acknowledgmentType)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendStatusAcknowledgmentCommand_String", new object[2]
			{
				GetObjectId(),
				acknowledgmentType
			});
			return JsonHelper.Deserialize<SendStatusAcknowledgmentCommand>(json);
		}

		public StatusAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___StatusAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<StatusAcknowledgment>(json);
		}
	}
}
