using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendAlertAcknowledgmentCallbackProxy : CallbackProxy<ISendAlertAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendAlertAcknowledgment_003Ec__AnonStorey40
		{
			internal AlertAcknowledgment _acknowledgment;

			internal AlertAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__24(ISendAlertAcknowledgmentCallback instance)
			{
				instance.onSendAlertAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendAlertAcknowledgmentCallbackProxy";

		public ISendAlertAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendAlertAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendAlertAcknowledgmentCallback.onSendAlertAcknowledgment");
				try
				{
					onSendAlertAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendAlertAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendAlertAcknowledgmentCallbackProxy.onSendAlertAcknowledgment");
			try
			{
				_003ConSendAlertAcknowledgment_003Ec__AnonStorey40 _003ConSendAlertAcknowledgment_003Ec__AnonStorey = new _003ConSendAlertAcknowledgment_003Ec__AnonStorey40();
				_003ConSendAlertAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<AlertAcknowledgment>(acknowledgment);
				_003ConSendAlertAcknowledgment_003Ec__AnonStorey._outboxAcknowledgment = JsonHelper.Deserialize<AlertAcknowledgment>(outboxAcknowledgment);
				_003ConSendAlertAcknowledgment_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendAlertAcknowledgment_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendAlertAcknowledgmentCallback>(callbackType, callbackId, _003ConSendAlertAcknowledgment_003Ec__AnonStorey._003C_003Em__24);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
