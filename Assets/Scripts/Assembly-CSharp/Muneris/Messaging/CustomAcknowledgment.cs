using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomAcknowledgment")]
	public class CustomAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomAcknowledgmentBridge";

		public CustomAcknowledgment(JsonObject json, CustomMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "CustomAcknowledgment____JSONObject_CustomMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected CustomAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public CustomMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___CustomMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomMessage>(json);
		}
	}
}
