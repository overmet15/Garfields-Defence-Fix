using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveVirtualItemBundleAcknowledgmentCallbackProxy : CallbackProxy<IReceiveVirtualItemBundleAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B
		{
			internal VirtualItemBundleAcknowledgment _acknowledgment;

			internal void _003C_003Em__3F(IReceiveVirtualItemBundleAcknowledgmentCallback instance)
			{
				instance.onReceiveVirtualItemBundleAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveVirtualItemBundleAcknowledgmentCallbackProxy";

		public IReceiveVirtualItemBundleAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveVirtualItemBundleAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveVirtualItemBundleAcknowledgmentCallback.onReceiveVirtualItemBundleAcknowledgment");
				try
				{
					onReceiveVirtualItemBundleAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveVirtualItemBundleAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveVirtualItemBundleAcknowledgmentCallbackProxy.onReceiveVirtualItemBundleAcknowledgment");
			try
			{
				_003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B _003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B = new _003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B();
				_003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B._acknowledgment = JsonHelper.Deserialize<VirtualItemBundleAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveVirtualItemBundleAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveVirtualItemBundleAcknowledgment_003Ec__AnonStorey5B._003C_003Em__3F);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
