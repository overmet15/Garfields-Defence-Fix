using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendChatAcknowledgmentCallbackProxy : CallbackProxy<ISendChatAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendChatAcknowledgment_003Ec__AnonStorey42
		{
			internal ChatAcknowledgment _acknowledgment;

			internal ChatAcknowledgment _outboxAcknowledgment;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__26(ISendChatAcknowledgmentCallback instance)
			{
				instance.onSendChatAcknowledgment(_acknowledgment, _outboxAcknowledgment, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendChatAcknowledgmentCallbackProxy";

		public ISendChatAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendChatAcknowledgment"))
			{
				DebugUtil.Log("Invoking ISendChatAcknowledgmentCallback.onSendChatAcknowledgment");
				try
				{
					onSendChatAcknowledgment((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendChatAcknowledgment(int callbackType, int callbackId, string acknowledgment, string outboxAcknowledgment, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendChatAcknowledgmentCallbackProxy.onSendChatAcknowledgment");
			try
			{
				_003ConSendChatAcknowledgment_003Ec__AnonStorey42 _003ConSendChatAcknowledgment_003Ec__AnonStorey = new _003ConSendChatAcknowledgment_003Ec__AnonStorey42();
				_003ConSendChatAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<ChatAcknowledgment>(acknowledgment);
				_003ConSendChatAcknowledgment_003Ec__AnonStorey._outboxAcknowledgment = JsonHelper.Deserialize<ChatAcknowledgment>(outboxAcknowledgment);
				_003ConSendChatAcknowledgment_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendChatAcknowledgment_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendChatAcknowledgmentCallback>(callbackType, callbackId, _003ConSendChatAcknowledgment_003Ec__AnonStorey._003C_003Em__26);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
