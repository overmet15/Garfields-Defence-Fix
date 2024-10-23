using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomRequestMessageCallbackProxy : CallbackProxy<IReceiveCustomRequestMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B
		{
			internal CustomRequestMessage _message;

			internal void _003C_003Em__1F(IReceiveCustomRequestMessageCallback instance)
			{
				instance.onReceiveCustomRequestMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomRequestMessageCallbackProxy";

		public IReceiveCustomRequestMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomRequestMessage"))
			{
				DebugUtil.Log("Invoking IReceiveCustomRequestMessageCallback.onReceiveCustomRequestMessage");
				try
				{
					onReceiveCustomRequestMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomRequestMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveCustomRequestMessageCallbackProxy.onReceiveCustomRequestMessage");
			try
			{
				_003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B _003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B = new _003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B();
				_003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B._message = JsonHelper.Deserialize<CustomRequestMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveCustomRequestMessageCallback>(callbackType, callbackId, _003ConReceiveCustomRequestMessage_003Ec__AnonStorey3B._003C_003Em__1F);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
