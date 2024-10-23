using Muneris.Bridge;
using Muneris.Virtualitem;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.VirtualItemBundleMessages")]
	public class VirtualItemBundleMessages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.VirtualItemBundleMessagesBridge";

		protected VirtualItemBundleMessages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindVirtualItemBundleMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindVirtualItemBundleMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindVirtualItemBundleMessagesCommand>(json);
		}

		public static SendVirtualItemBundleMessageCommand send(ISendableAddress targetAddress, VirtualItemBundle virtualItemBundle)
		{
			string text = JsonHelper.Serialize(targetAddress);
			string text2 = JsonHelper.Serialize(virtualItemBundle);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "send___SendVirtualItemBundleMessageCommand_SendableAddress_VirtualItemBundle", new object[2] { text, text2 });
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
		}
	}
}
