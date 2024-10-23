using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.ChannelTarget")]
	public class ChannelTarget : BridgeObject, IAddress, ISendableAddress, ITargetAddress
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.ChannelTargetBridge";

		public ChannelTarget(Channel channel)
			: base(0L)
		{
			string text = JsonHelper.Serialize(channel);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "ChannelTarget____Channel", new object[1] { text });
			Init(num);
		}

		public ChannelTarget(Channel channel, string installId, IApp app)
			: base(0L)
		{
			string text = JsonHelper.Serialize(channel);
			string text2 = JsonHelper.Serialize(app);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "ChannelTarget____Channel_String_App", new object[3] { text, installId, text2 });
			Init(num);
		}

		protected ChannelTarget(ObjectId objectId)
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

		public ChannelTarget setApp(IApp app)
		{
			string text = JsonHelper.Serialize(app);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setApp___ChannelTarget_App", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<ChannelTarget>(json);
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
