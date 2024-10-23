using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.StatusAcknowledgment")]
	public class StatusAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.StatusAcknowledgmentBridge";

		public StatusAcknowledgment(JsonObject json, StatusMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "StatusAcknowledgment____JSONObject_StatusMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected StatusAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public StatusMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___StatusMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<StatusMessage>(json);
		}
	}
}
