using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveVirtualItemBundleMessageCallbackProxy : CallbackProxy<IReceiveVirtualItemBundleMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C
		{
			internal VirtualItemBundleMessage _message;

			internal void _003C_003Em__40(IReceiveVirtualItemBundleMessageCallback instance)
			{
				instance.onReceiveVirtualItemBundleMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveVirtualItemBundleMessageCallbackProxy";

		public IReceiveVirtualItemBundleMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveVirtualItemBundleMessage"))
			{
				DebugUtil.Log("Invoking IReceiveVirtualItemBundleMessageCallback.onReceiveVirtualItemBundleMessage");
				try
				{
					onReceiveVirtualItemBundleMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveVirtualItemBundleMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveVirtualItemBundleMessageCallbackProxy.onReceiveVirtualItemBundleMessage");
			try
			{
				_003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C _003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C = new _003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C();
				_003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C._message = JsonHelper.Deserialize<VirtualItemBundleMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveVirtualItemBundleMessageCallback>(callbackType, callbackId, _003ConReceiveVirtualItemBundleMessage_003Ec__AnonStorey5C._003C_003Em__40);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
