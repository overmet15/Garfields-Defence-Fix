using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomResponseAcknowledgmentCallbackProxy : CallbackProxy<ISendCustomResponseAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey48
		{
			internal CustomResponseAcknowledgment _acknowledgment;

			internal CustomResponseAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2C(ISendCustomResponseAcknowledgmentCallback instance)
			{
				instance.onSendCustomResponseAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomResponseAcknowledgmentCallbackProxy";

		public ISendCustomResponseAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomResponseAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendCustomResponseAcknowledgmentCallback.onSendCustomResponseAcknowledgment");
				try
				{
					onSendCustomResponseAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomResponseAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomResponseAcknowledgmentCallbackProxy.onSendCustomResponseAcknowledgment");
			try
			{
				_003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey48 _003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey = new _003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey48();
				_003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<CustomResponseAcknowledgment>(acknowledgment);
				_003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey._outboxAcknowledgment = JsonHelper.Deserialize<CustomResponseAcknowledgment>(outboxAcknowledgment);
				_003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomResponseAcknowledgmentCallback>(callbackType, callbackId, _003ConSendCustomResponseAcknowledgment_003Ec__AnonStorey._003C_003Em__2C);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
