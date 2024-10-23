using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.AlertAcknowledgment")]
	public class AlertAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.AlertAcknowledgmentBridge";

		public AlertAcknowledgment(JsonObject json, AlertMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "AlertAcknowledgment____JSONObject_AlertMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected AlertAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public AlertMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___AlertMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<AlertMessage>(json);
		}
	}
}
