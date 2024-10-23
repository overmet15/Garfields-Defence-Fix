using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomResponseMessageCallbackProxy : CallbackProxy<IReceiveCustomResponseMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D
		{
			internal CustomResponseMessage _message;

			internal void _003C_003Em__21(IReceiveCustomResponseMessageCallback instance)
			{
				instance.onReceiveCustomResponseMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomResponseMessageCallbackProxy";

		public IReceiveCustomResponseMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomResponseMessage"))
			{
				DebugUtil.Log("Invoking IReceiveCustomResponseMessageCallback.onReceiveCustomResponseMessage");
				try
				{
					onReceiveCustomResponseMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomResponseMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveCustomResponseMessageCallbackProxy.onReceiveCustomResponseMessage");
			try
			{
				_003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D _003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D = new _003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D();
				_003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D._message = JsonHelper.Deserialize<CustomResponseMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveCustomResponseMessageCallback>(callbackType, callbackId, _003ConReceiveCustomResponseMessage_003Ec__AnonStorey3D._003C_003Em__21);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
