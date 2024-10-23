using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Channels")]
	public class Channels : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChannelsBridge";

		protected Channels(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindChannelsCommand find()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindChannelsCommand", new object[0]);
			return JsonHelper.Deserialize<FindChannelsCommand>(json);
		}
	}
}
