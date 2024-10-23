using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.InvokeCustomApiCommand")]
	public class InvokeCustomApiCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.InvokeCustomApiCommandBridge";

		protected InvokeCustomApiCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getApiMethod()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getApiMethod___String", new object[1] { GetObjectId() });
		}

		public JsonObject getApiParams()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getApiParams___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
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

		public InvokeCustomApiCommand setCallback(IInvokeCustomApiCallback invokeCustomApiCallback)
		{
			int num = CallbackCenter.RegisterInlineCallback(invokeCustomApiCallback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___InvokeCustomApiCommand_InvokeCustomApiCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<InvokeCustomApiCommand>(json);
		}

		public IInvokeCustomApiCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___InvokeCustomApiCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IInvokeCustomApiCallback>(callbackId);
		}

		public InvokeCustomApiCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___InvokeCustomApiCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<InvokeCustomApiCommand>(json);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public InvokeCustomApiCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___InvokeCustomApiCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<InvokeCustomApiCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
