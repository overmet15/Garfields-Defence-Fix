using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IUnsubscribeChannelCallbackProxy : CallbackProxy<IUnsubscribeChannelCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConUnsubscribeChannel_003Ec__AnonStorey4D
		{
			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__31(IUnsubscribeChannelCallback instance)
			{
				instance.onUnsubscribeChannel(_callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.UnsubscribeChannelCallbackProxy";

		public IUnsubscribeChannelCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onUnsubscribeChannel"))
			{
				DebugUtil.Log("Invoking IUnsubscribeChannelCallback.onUnsubscribeChannel");
				try
				{
					onUnsubscribeChannel((int)args[0], (int)args[1], (string)args[2], (string)args[3]);
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

		private void onUnsubscribeChannel(int callbackType, int callbackId, string callbackContext, string exception)
		{
			DebugUtil.Log("IUnsubscribeChannelCallbackProxy.onUnsubscribeChannel");
			try
			{
				_003ConUnsubscribeChannel_003Ec__AnonStorey4D _003ConUnsubscribeChannel_003Ec__AnonStorey4D = new _003ConUnsubscribeChannel_003Ec__AnonStorey4D();
				_003ConUnsubscribeChannel_003Ec__AnonStorey4D._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConUnsubscribeChannel_003Ec__AnonStorey4D._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IUnsubscribeChannelCallback>(callbackType, callbackId, _003ConUnsubscribeChannel_003Ec__AnonStorey4D._003C_003Em__31);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
