using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendStatusAcknowledgmentCallbackProxy : CallbackProxy<ISendStatusAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendStatusAcknowledgment_003Ec__AnonStorey4A
		{
			internal StatusAcknowledgment _acknowledgment;

			internal StatusAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2E(ISendStatusAcknowledgmentCallback instance)
			{
				instance.onSendStatusAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendStatusAcknowledgmentCallbackProxy";

		public ISendStatusAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendStatusAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendStatusAcknowledgmentCallback.onSendStatusAcknowledgment");
				try
				{
					onSendStatusAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendStatusAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendStatusAcknowledgmentCallbackProxy.onSendStatusAcknowledgment");
			try
			{
				_003ConSendStatusAcknowledgment_003Ec__AnonStorey4A _003ConSendStatusAcknowledgment_003Ec__AnonStorey4A = new _003ConSendStatusAcknowledgment_003Ec__AnonStorey4A();
				_003ConSendStatusAcknowledgment_003Ec__AnonStorey4A._acknowledgment = JsonHelper.Deserialize<StatusAcknowledgment>(acknowledgment);
				_003ConSendStatusAcknowledgment_003Ec__AnonStorey4A._outboxAcknowledgment = JsonHelper.Deserialize<StatusAcknowledgment>(outboxAcknowledgment);
				_003ConSendStatusAcknowledgment_003Ec__AnonStorey4A._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendStatusAcknowledgment_003Ec__AnonStorey4A._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendStatusAcknowledgmentCallback>(callbackType, callbackId, _003ConSendStatusAcknowledgment_003Ec__AnonStorey4A._003C_003Em__2E);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
