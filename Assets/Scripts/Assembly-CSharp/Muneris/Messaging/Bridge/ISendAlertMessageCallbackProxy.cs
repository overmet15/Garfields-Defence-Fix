using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendAlertMessageCallbackProxy : CallbackProxy<ISendAlertMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendAlertMessage_003Ec__AnonStorey41
		{
			internal AlertMessage _message;

			internal AlertMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__25(ISendAlertMessageCallback instance)
			{
				instance.onSendAlertMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendAlertMessageCallbackProxy";

		public ISendAlertMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendAlertMessage"))
			{
				DebugUtil.Log("Invoking ISendAlertMessageCallback.onSendAlertMessage");
				try
				{
					onSendAlertMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendAlertMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendAlertMessageCallbackProxy.onSendAlertMessage");
			try
			{
				_003ConSendAlertMessage_003Ec__AnonStorey41 _003ConSendAlertMessage_003Ec__AnonStorey = new _003ConSendAlertMessage_003Ec__AnonStorey41();
				_003ConSendAlertMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<AlertMessage>(message);
				_003ConSendAlertMessage_003Ec__AnonStorey._outboxMessage = JsonHelper.Deserialize<AlertMessage>(outboxMessage);
				_003ConSendAlertMessage_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendAlertMessage_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendAlertMessageCallback>(callbackType, callbackId, _003ConSendAlertMessage_003Ec__AnonStorey._003C_003Em__25);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
