using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomMessageCallbackProxy : CallbackProxy<IReceiveCustomMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomMessage_003Ec__AnonStorey39
		{
			internal CustomMessage _message;

			internal void _003C_003Em__1D(IReceiveCustomMessageCallback instance)
			{
				instance.onReceiveCustomMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomMessageCallbackProxy";

		public IReceiveCustomMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomMessage"))
			{
				DebugUtil.Log("Invoking IReceiveCustomMessageCallback.onReceiveCustomMessage");
				try
				{
					onReceiveCustomMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveCustomMessageCallbackProxy.onReceiveCustomMessage");
			try
			{
				_003ConReceiveCustomMessage_003Ec__AnonStorey39 _003ConReceiveCustomMessage_003Ec__AnonStorey = new _003ConReceiveCustomMessage_003Ec__AnonStorey39();
				_003ConReceiveCustomMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<CustomMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveCustomMessageCallback>(callbackType, callbackId, _003ConReceiveCustomMessage_003Ec__AnonStorey._003C_003Em__1D);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
