using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendVirtualItemBundleAcknowledgmentCallbackProxy : CallbackProxy<ISendVirtualItemBundleAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D
		{
			internal VirtualItemBundleAcknowledgment _acknowledgment;

			internal VirtualItemBundleAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__41(ISendVirtualItemBundleAcknowledgmentCallback instance)
			{
				instance.onSendVirtualItemBundleAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendVirtualItemBundleAcknowledgmentCallbackProxy";

		public ISendVirtualItemBundleAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendVirtualItemBundleAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendVirtualItemBundleAcknowledgmentCallback.onSendVirtualItemBundleAcknowledgment");
				try
				{
					onSendVirtualItemBundleAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendVirtualItemBundleAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendVirtualItemBundleAcknowledgmentCallbackProxy.onSendVirtualItemBundleAcknowledgment");
			try
			{
				_003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D _003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D = new _003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D();
				_003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D._acknowledgment = JsonHelper.Deserialize<VirtualItemBundleAcknowledgment>(acknowledgment);
				_003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D._outboxAcknowledgment = JsonHelper.Deserialize<VirtualItemBundleAcknowledgment>(outboxAcknowledgment);
				_003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendVirtualItemBundleAcknowledgmentCallback>(callbackType, callbackId, _003ConSendVirtualItemBundleAcknowledgment_003Ec__AnonStorey5D._003C_003Em__41);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
