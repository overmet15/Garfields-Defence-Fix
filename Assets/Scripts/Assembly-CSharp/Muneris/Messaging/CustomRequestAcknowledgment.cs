using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomRequestAcknowledgment")]
	public class CustomRequestAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomRequestAcknowledgmentBridge";

		public CustomRequestAcknowledgment(JsonObject json, CustomRequestMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "CustomRequestAcknowledgment____JSONObject_CustomRequestMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected CustomRequestAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public CustomRequestMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___CustomRequestMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomRequestMessage>(json);
		}
	}
}
