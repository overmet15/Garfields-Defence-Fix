using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IReceiveDeepLinkDataCallbackProxy : CallbackProxy<IReceiveDeepLinkDataCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConDeepLinkDataReceive_003Ec__AnonStorey2B
		{
			internal JsonObject _data;

			internal void _003C_003Em__F(IReceiveDeepLinkDataCallback instance)
			{
				instance.onDeepLinkDataReceive(_data);
			}
		}

		private static string sNativeClassName = "muneris.bridge.ReceiveDeepLinkDataCallbackProxy";

		public IReceiveDeepLinkDataCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onDeepLinkDataReceive"))
			{
				DebugUtil.Log("Invoking IReceiveDeepLinkDataCallback.onDeepLinkDataReceive");
				try
				{
					onDeepLinkDataReceive((int)args[0], (int)args[1], (string)args[2]);
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

		private void onDeepLinkDataReceive(int callbackType, int callbackId, string data)
		{
			DebugUtil.Log("IReceiveDeepLinkDataCallbackProxy.onDeepLinkDataReceive");
			try
			{
				_003ConDeepLinkDataReceive_003Ec__AnonStorey2B _003ConDeepLinkDataReceive_003Ec__AnonStorey2B = new _003ConDeepLinkDataReceive_003Ec__AnonStorey2B();
				_003ConDeepLinkDataReceive_003Ec__AnonStorey2B._data = JsonHelper.Deserialize<JsonObject>(data);
				CallbackCenter.InvokeCallback<IReceiveDeepLinkDataCallback>(callbackType, callbackId, _003ConDeepLinkDataReceive_003Ec__AnonStorey2B._003C_003Em__F);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
