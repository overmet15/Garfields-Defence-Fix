using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SubscribeChannelCommand")]
	public class SubscribeChannelCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SubscribeChannelCommandBridge";

		protected SubscribeChannelCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public Channel getChannel()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getChannel___Channel", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<Channel>(json);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public SubscribeChannelCommand setCallback(ISubscribeChannelCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___SubscribeChannelCommand_SubscribeChannelCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SubscribeChannelCommand>(json);
		}

		public SubscribeChannelCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___SubscribeChannelCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<SubscribeChannelCommand>(json);
		}

		public ISubscribeChannelCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___SubscribeChannelCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<ISubscribeChannelCallback>(callbackId);
		}

		public void validate()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "validate___void", new object[1] { GetObjectId() });
			BridgeResult<object> bridgeResult = JsonHelper.DeserializeBridgeResult<object>(json);
			if (bridgeResult.Kind == BridgeResult<object>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public SubscribeChannelCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SubscribeChannelCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SubscribeChannelCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
