using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Channel")]
	public class Channel : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChannelBridge";

		protected Channel(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getChannelId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getChannelId___String", new object[1] { GetObjectId() });
		}

		public string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[1] { GetObjectId() });
		}

		public SubscribeChannelCommand subscribe()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "subscribe___SubscribeChannelCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<SubscribeChannelCommand>(json);
		}

		public UnsubscribeChannelCommand unsubscribe()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "unsubscribe___UnsubscribeChannelCommand", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<UnsubscribeChannelCommand>(json);
		}
	}
}
