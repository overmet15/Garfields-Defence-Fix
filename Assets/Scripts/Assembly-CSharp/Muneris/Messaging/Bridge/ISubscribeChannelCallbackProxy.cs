using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISubscribeChannelCallbackProxy : CallbackProxy<ISubscribeChannelCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSubscribeChannel_003Ec__AnonStorey4C
		{
			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__30(ISubscribeChannelCallback instance)
			{
				instance.onSubscribeChannel(_callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SubscribeChannelCallbackProxy";

		public ISubscribeChannelCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSubscribeChannel"))
			{
				DebugUtil.Log("Invoking ISubscribeChannelCallback.onSubscribeChannel");
				try
				{
					onSubscribeChannel((int)args[0], (int)args[1], (string)args[2], (string)args[3]);
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

		private void onSubscribeChannel(int callbackType, int callbackId, string callbackContext, string exception)
		{
			DebugUtil.Log("ISubscribeChannelCallbackProxy.onSubscribeChannel");
			try
			{
				_003ConSubscribeChannel_003Ec__AnonStorey4C _003ConSubscribeChannel_003Ec__AnonStorey4C = new _003ConSubscribeChannel_003Ec__AnonStorey4C();
				_003ConSubscribeChannel_003Ec__AnonStorey4C._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSubscribeChannel_003Ec__AnonStorey4C._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISubscribeChannelCallback>(callbackType, callbackId, _003ConSubscribeChannel_003Ec__AnonStorey4C._003C_003Em__30);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
