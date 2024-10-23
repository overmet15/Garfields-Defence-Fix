using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendChatMessageCallbackProxy : CallbackProxy<ISendChatMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendChatMessage_003Ec__AnonStorey43
		{
			internal ChatMessage _message;

			internal ChatMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__27(ISendChatMessageCallback instance)
			{
				instance.onSendChatMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendChatMessageCallbackProxy";

		public ISendChatMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendChatMessage"))
			{
				DebugUtil.Log("Invoking ISendChatMessageCallback.onSendChatMessage");
				try
				{
					onSendChatMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendChatMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendChatMessageCallbackProxy.onSendChatMessage");
			try
			{
				_003ConSendChatMessage_003Ec__AnonStorey43 _003ConSendChatMessage_003Ec__AnonStorey = new _003ConSendChatMessage_003Ec__AnonStorey43();
				_003ConSendChatMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<ChatMessage>(message);
				_003ConSendChatMessage_003Ec__AnonStorey._outboxMessage = JsonHelper.Deserialize<ChatMessage>(outboxMessage);
				_003ConSendChatMessage_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendChatMessage_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendChatMessageCallback>(callbackType, callbackId, _003ConSendChatMessage_003Ec__AnonStorey._003C_003Em__27);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
