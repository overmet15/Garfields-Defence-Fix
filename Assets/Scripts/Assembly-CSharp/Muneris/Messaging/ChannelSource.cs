using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.ChannelSource")]
	public class ChannelSource : BridgeObject, IAddress, ISourceAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChannelSourceBridge";

		protected ChannelSource(ObjectId objectId)
			: base(objectId)
		{
		}

		public AddressType getType()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getType___AddressType", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<AddressType>(num);
		}

		public Channel getChannel()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getChannel___Channel", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<Channel>(json);
		}

		public ChannelSource setApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setApp___ChannelSource_App", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<ChannelSource>(json);
		}

		public string getInstallId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getInstallId___String", new object[1] { GetObjectId() });
		}

		public IApp getApp()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getApp___App", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<IApp>(json);
		}
	}
}
