using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.VirtualItemBundleAcknowledgment")]
	public class VirtualItemBundleAcknowledgment : Acknowledgment
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.VirtualItemBundleAcknowledgmentBridge";

		public VirtualItemBundleAcknowledgment(JsonObject data, VirtualItemBundleMessage message)
			: base(0L)
		{
			string text = JsonHelper.Serialize(data);
			string text2 = JsonHelper.Serialize(message);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "VirtualItemBundleAcknowledgment____JSONObject_VirtualItemBundleMessage", new object[2] { text, text2 });
			BridgeResult<long> bridgeResult = JsonHelper.DeserializeBridgeResult<long>(json);
			if (bridgeResult.Kind == BridgeResult<long>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
			Init(bridgeResult.Value);
		}

		protected VirtualItemBundleAcknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public VirtualItemBundleMessage getMessage()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getMessage___VirtualItemBundleMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundleMessage>(json);
		}
	}
}
