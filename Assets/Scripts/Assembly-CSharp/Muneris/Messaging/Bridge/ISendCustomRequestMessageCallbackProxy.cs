using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomRequestMessageCallbackProxy : CallbackProxy<ISendCustomRequestMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomRequestMessage_003Ec__AnonStorey47
		{
			internal CustomRequestMessage _message;

			internal CustomRequestMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2B(ISendCustomRequestMessageCallback instance)
			{
				instance.onSendCustomRequestMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomRequestMessageCallbackProxy";

		public ISendCustomRequestMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomRequestMessage"))
			{
				DebugUtil.Log("Invoking ISendCustomRequestMessageCallback.onSendCustomRequestMessage");
				try
				{
					onSendCustomRequestMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomRequestMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomRequestMessageCallbackProxy.onSendCustomRequestMessage");
			try
			{
				_003ConSendCustomRequestMessage_003Ec__AnonStorey47 _003ConSendCustomRequestMessage_003Ec__AnonStorey = new _003ConSendCustomRequestMessage_003Ec__AnonStorey47();
				_003ConSendCustomRequestMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<CustomRequestMessage>(message);
				_003ConSendCustomRequestMessage_003Ec__AnonStorey._outboxMessage = JsonHelper.Deserialize<CustomRequestMessage>(outboxMessage);
				_003ConSendCustomRequestMessage_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomRequestMessage_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomRequestMessageCallback>(callbackType, callbackId, _003ConSendCustomRequestMessage_003Ec__AnonStorey._003C_003Em__2B);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
