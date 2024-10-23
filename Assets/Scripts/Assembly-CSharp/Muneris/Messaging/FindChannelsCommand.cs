using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.FindChannelsCommand")]
	public class FindChannelsCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.FindChannelsCommandBridge";

		protected FindChannelsCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public IFindChannelsCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindChannelsCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindChannelsCallback>(callbackId);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public FindChannelsCommand setCallback(IFindChannelsCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindChannelsCommand_FindChannelsCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindChannelsCommand>(json);
		}

		public FindChannelsCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___FindChannelsCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<FindChannelsCommand>(json);
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public FindChannelsCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindChannelsCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindChannelsCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
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
	}
}
