using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomRequestAcknowledgmentCallbackProxy : CallbackProxy<ISendCustomRequestAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey46
		{
			internal CustomRequestAcknowledgment _acknowledgment;

			internal CustomRequestAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2A(ISendCustomRequestAcknowledgmentCallback instance)
			{
				instance.onSendCustomRequestAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomRequestAcknowledgmentCallbackProxy";

		public ISendCustomRequestAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomRequestAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendCustomRequestAcknowledgmentCallback.onSendCustomRequestAcknowledgment");
				try
				{
					onSendCustomRequestAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomRequestAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomRequestAcknowledgmentCallbackProxy.onSendCustomRequestAcknowledgment");
			try
			{
				_003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey46 _003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey = new _003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey46();
				_003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<CustomRequestAcknowledgment>(acknowledgment);
				_003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey._outboxAcknowledgment = JsonHelper.Deserialize<CustomRequestAcknowledgment>(outboxAcknowledgment);
				_003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomRequestAcknowledgmentCallback>(callbackType, callbackId, _003ConSendCustomRequestAcknowledgment_003Ec__AnonStorey._003C_003Em__2A);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
