using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveAlertMessageCallbackProxy : CallbackProxy<IReceiveAlertMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveAlertMessage_003Ec__AnonStorey35
		{
			internal AlertMessage _message;

			internal void _003C_003Em__19(IReceiveAlertMessageCallback instance)
			{
				instance.onReceiveAlertMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveAlertMessageCallbackProxy";

		public IReceiveAlertMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveAlertMessage"))
			{
				DebugUtil.Log("Invoking IReceiveAlertMessageCallback.onReceiveAlertMessage");
				try
				{
					onReceiveAlertMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveAlertMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveAlertMessageCallbackProxy.onReceiveAlertMessage");
			try
			{
				_003ConReceiveAlertMessage_003Ec__AnonStorey35 _003ConReceiveAlertMessage_003Ec__AnonStorey = new _003ConReceiveAlertMessage_003Ec__AnonStorey35();
				_003ConReceiveAlertMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<AlertMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveAlertMessageCallback>(callbackType, callbackId, _003ConReceiveAlertMessage_003Ec__AnonStorey._003C_003Em__19);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
