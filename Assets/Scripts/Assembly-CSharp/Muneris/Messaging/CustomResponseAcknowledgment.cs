using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.CustomResponseAcknowledgment")]
	public class CustomResponseAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.CustomResponseAcknowledgmentBridge";

		public CustomResponseAcknowledgment(JsonObject json, CustomResponseMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(json);
			string text2 = JsonHelper.Serialize(message);
			string json2 = JniHelper.CallStatic<string>(_bridgeClassName, "CustomResponseAcknowledgment____JSONObject_CustomResponseMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json2);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected CustomResponseAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public CustomResponseMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___CustomResponseMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomResponseMessage>(json);
		}
	}
}
