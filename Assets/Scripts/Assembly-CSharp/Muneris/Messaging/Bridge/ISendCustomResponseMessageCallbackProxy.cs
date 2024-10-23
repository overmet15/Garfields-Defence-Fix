using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomResponseMessageCallbackProxy : CallbackProxy<ISendCustomResponseMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomResponseMessage_003Ec__AnonStorey49
		{
			internal CustomResponseMessage _message;

			internal CustomResponseMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__2D(ISendCustomResponseMessageCallback instance)
			{
				instance.onSendCustomResponseMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomResponseMessageCallbackProxy";

		public ISendCustomResponseMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomResponseMessage"))
			{
				DebugUtil.Log("Invoking ISendCustomResponseMessageCallback.onSendCustomResponseMessage");
				try
				{
					onSendCustomResponseMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomResponseMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomResponseMessageCallbackProxy.onSendCustomResponseMessage");
			try
			{
				_003ConSendCustomResponseMessage_003Ec__AnonStorey49 _003ConSendCustomResponseMessage_003Ec__AnonStorey = new _003ConSendCustomResponseMessage_003Ec__AnonStorey49();
				_003ConSendCustomResponseMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<CustomResponseMessage>(message);
				_003ConSendCustomResponseMessage_003Ec__AnonStorey._outboxMessage = JsonHelper.Deserialize<CustomResponseMessage>(outboxMessage);
				_003ConSendCustomResponseMessage_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomResponseMessage_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomResponseMessageCallback>(callbackType, callbackId, _003ConSendCustomResponseMessage_003Ec__AnonStorey._003C_003Em__2D);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
