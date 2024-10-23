using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Messages")]
	public class Messages : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.MessagesBridge";

		protected Messages(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindMessagesCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindMessagesCommand", new object[0]);
			return JsonHelper.Deserialize<FindMessagesCommand>(json);
		}
	}
}
