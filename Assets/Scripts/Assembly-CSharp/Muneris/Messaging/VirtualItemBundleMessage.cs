using Muneris.Bridge;
using Muneris.Virtualitem;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.VirtualItemBundleMessage")]
	public class VirtualItemBundleMessage : Message
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.VirtualItemBundleMessageBridge";

		protected VirtualItemBundleMessage(ObjectId objectId)
			: base(objectId)
		{
		}

		public VirtualItemBundle getVirtualItemBundle()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemBundle___VirtualItemBundle", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundle>(json);
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

		public SendVirtualItemBundleAcknowledgmentCommand sendAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "sendAcknowledgment___SendVirtualItemBundleAcknowledgmentCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SendVirtualItemBundleAcknowledgmentCommand>(json);
		}

		public VirtualItemBundleAcknowledgment getAcknowledgment()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgment___VirtualItemBundleAcknowledgment", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundleAcknowledgment>(json);
		}
	}
}
