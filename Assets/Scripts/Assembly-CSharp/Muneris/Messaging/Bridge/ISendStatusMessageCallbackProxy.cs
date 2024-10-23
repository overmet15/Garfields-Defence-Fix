using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendStatusMessageCallbackProxy : CallbackProxy<ISendStatusMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendStatusMessage_003Ec__AnonStorey4B
		{
			internal StatusMessage _message;

			internal StatusMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2F(ISendStatusMessageCallback instance)
			{
				instance.onSendStatusMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendStatusMessageCallbackProxy";

		public ISendStatusMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendStatusMessage"))
			{
				DebugUtil.Log("Invoking ISendStatusMessageCallback.onSendStatusMessage");
				try
				{
					onSendStatusMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendStatusMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendStatusMessageCallbackProxy.onSendStatusMessage");
			try
			{
				_003ConSendStatusMessage_003Ec__AnonStorey4B _003ConSendStatusMessage_003Ec__AnonStorey4B = new _003ConSendStatusMessage_003Ec__AnonStorey4B();
				_003ConSendStatusMessage_003Ec__AnonStorey4B._message = JsonHelper.Deserialize<StatusMessage>(message);
				_003ConSendStatusMessage_003Ec__AnonStorey4B._outboxMessage = JsonHelper.Deserialize<StatusMessage>(outboxMessage);
				_003ConSendStatusMessage_003Ec__AnonStorey4B._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendStatusMessage_003Ec__AnonStorey4B._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendStatusMessageCallback>(callbackType, callbackId, _003ConSendStatusMessage_003Ec__AnonStorey4B._003C_003Em__2F);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
