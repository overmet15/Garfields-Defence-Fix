using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.FindAppsCommand")]
	public class FindAppsCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.FindAppsCommandBridge";

		protected FindAppsCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public List<string> getAppIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAppIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
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

		public FindAppsCommand setCallback(IFindAppsCallback findAppsCallback)
		{
			int num = CallbackCenter.RegisterInlineCallback(findAppsCallback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindAppsCommand_FindAppsCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindAppsCommand>(json);
		}

		public IFindAppsCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindAppsCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindAppsCallback>(callbackId);
		}

		public FindAppsCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___FindAppsCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<FindAppsCommand>(json);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public FindAppsCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindAppsCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindAppsCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
