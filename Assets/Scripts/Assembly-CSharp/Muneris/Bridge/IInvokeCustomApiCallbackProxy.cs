using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IInvokeCustomApiCallbackProxy : CallbackProxy<IInvokeCustomApiCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConInvokeCustomApi_003Ec__AnonStorey2A
		{
			internal string _apiMethod;

			internal JsonObject _apiParams;

			internal CallbackContext _callbackContext;

			internal MunerisException _munerisException;

			internal void _003C_003Em__E(IInvokeCustomApiCallback instance)
			{
				instance.onInvokeCustomApi(_apiMethod, _apiParams, _callbackContext, _munerisException);
			}
		}

		private static string sNativeClassName = "muneris.bridge.InvokeCustomApiCallbackProxy";

		public IInvokeCustomApiCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onInvokeCustomApi"))
			{
				DebugUtil.Log("Invoking IInvokeCustomApiCallback.onInvokeCustomApi");
				try
				{
					onInvokeCustomApi((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
				}
				catch (Exception ex)
				{
					DebugUtil.Log("Invocation error", ex);
				}
			}
			else
			{
				DebugUtil.Log("Invocation error, no such method:" + methodName);
			}
			return null;
		}

		private void onInvokeCustomApi(int callbackType, int callbackId, string apiMethod, string apiParams, string callbackContext, string munerisException)
		{
			DebugUtil.Log("IInvokeCustomApiCallbackProxy.onInvokeCustomApi");
			try
			{
				_003ConInvokeCustomApi_003Ec__AnonStorey2A _003ConInvokeCustomApi_003Ec__AnonStorey2A = new _003ConInvokeCustomApi_003Ec__AnonStorey2A();
				_003ConInvokeCustomApi_003Ec__AnonStorey2A._apiMethod = apiMethod;
				_003ConInvokeCustomApi_003Ec__AnonStorey2A._apiParams = JsonHelper.Deserialize<JsonObject>(apiParams);
				_003ConInvokeCustomApi_003Ec__AnonStorey2A._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConInvokeCustomApi_003Ec__AnonStorey2A._munerisException = JsonHelper.Deserialize<MunerisException>(munerisException);
				CallbackCenter.InvokeCallback<IInvokeCustomApiCallback>(callbackType, callbackId, _003ConInvokeCustomApi_003Ec__AnonStorey2A._003C_003Em__E);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
