using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomAcknowledgmentCallbackProxy : CallbackProxy<ISendCustomAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomAcknowledgment_003Ec__AnonStorey44
		{
			internal CustomAcknowledgment _acknowledgment;

			internal CustomAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__28(ISendCustomAcknowledgmentCallback instance)
			{
				instance.onSendCustomAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomAcknowledgmentCallbackProxy";

		public ISendCustomAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendCustomAcknowledgmentCallback.onSendCustomAcknowledgment");
				try
				{
					onSendCustomAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomAcknowledgmentCallbackProxy.onSendCustomAcknowledgment");
			try
			{
				_003ConSendCustomAcknowledgment_003Ec__AnonStorey44 _003ConSendCustomAcknowledgment_003Ec__AnonStorey = new _003ConSendCustomAcknowledgment_003Ec__AnonStorey44();
				_003ConSendCustomAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<CustomAcknowledgment>(acknowledgment);
				_003ConSendCustomAcknowledgment_003Ec__AnonStorey._outboxAcknowledgment = JsonHelper.Deserialize<CustomAcknowledgment>(outboxAcknowledgment);
				_003ConSendCustomAcknowledgment_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomAcknowledgment_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomAcknowledgmentCallback>(callbackType, callbackId, _003ConSendCustomAcknowledgment_003Ec__AnonStorey._003C_003Em__28);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
