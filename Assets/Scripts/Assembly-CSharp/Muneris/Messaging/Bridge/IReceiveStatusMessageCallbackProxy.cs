using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveStatusMessageCallbackProxy : CallbackProxy<IReceiveStatusMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveStatusMessage_003Ec__AnonStorey3F
		{
			internal StatusMessage _message;

			internal void _003C_003Em__23(IReceiveStatusMessageCallback instance)
			{
				instance.onReceiveStatusMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveStatusMessageCallbackProxy";

		public IReceiveStatusMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveStatusMessage"))
			{
				DebugUtil.Log("Invoking IReceiveStatusMessageCallback.onReceiveStatusMessage");
				try
				{
					onReceiveStatusMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveStatusMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveStatusMessageCallbackProxy.onReceiveStatusMessage");
			try
			{
				_003ConReceiveStatusMessage_003Ec__AnonStorey3F _003ConReceiveStatusMessage_003Ec__AnonStorey3F = new _003ConReceiveStatusMessage_003Ec__AnonStorey3F();
				_003ConReceiveStatusMessage_003Ec__AnonStorey3F._message = JsonHelper.Deserialize<StatusMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveStatusMessageCallback>(callbackType, callbackId, _003ConReceiveStatusMessage_003Ec__AnonStorey3F._003C_003Em__23);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
