using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendCustomMessageCallbackProxy : CallbackProxy<ISendCustomMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendCustomMessage_003Ec__AnonStorey45
		{
			internal CustomMessage _message;

			internal CustomMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__29(ISendCustomMessageCallback instance)
			{
				instance.onSendCustomMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendCustomMessageCallbackProxy";

		public ISendCustomMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendCustomMessage"))
			{
				DebugUtil.Log("Invoking ISendCustomMessageCallback.onSendCustomMessage");
				try
				{
					onSendCustomMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendCustomMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendCustomMessageCallbackProxy.onSendCustomMessage");
			try
			{
				_003ConSendCustomMessage_003Ec__AnonStorey45 _003ConSendCustomMessage_003Ec__AnonStorey = new _003ConSendCustomMessage_003Ec__AnonStorey45();
				_003ConSendCustomMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<CustomMessage>(message);
				_003ConSendCustomMessage_003Ec__AnonStorey._outboxMessage = JsonHelper.Deserialize<CustomMessage>(outboxMessage);
				_003ConSendCustomMessage_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendCustomMessage_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendCustomMessageCallback>(callbackType, callbackId, _003ConSendCustomMessage_003Ec__AnonStorey._003C_003Em__29);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
