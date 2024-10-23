using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class ISendVirtualItemBundleMessageCallbackProxy : CallbackProxy<ISendVirtualItemBundleMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E
		{
			internal VirtualItemBundleMessage _message;

			internal VirtualItemBundleMessage _outboxMessage;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__42(ISendVirtualItemBundleMessageCallback instance)
			{
				instance.onSendVirtualItemBundleMessage(_message, _outboxMessage, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.SendVirtualItemBundleMessageCallbackProxy";

		public ISendVirtualItemBundleMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onSendVirtualItemBundleMessage"))
			{
				DebugUtil.Log("Invoking ISendVirtualItemBundleMessageCallback.onSendVirtualItemBundleMessage");
				try
				{
					onSendVirtualItemBundleMessage((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onSendVirtualItemBundleMessage(int callbackType, int callbackId, string message, string outboxMessage, string callbackContext, string exception)
		{
			DebugUtil.Log("ISendVirtualItemBundleMessageCallbackProxy.onSendVirtualItemBundleMessage");
			try
			{
				_003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E _003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E = new _003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E();
				_003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E._message = JsonHelper.Deserialize<VirtualItemBundleMessage>(message);
				_003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E._outboxMessage = JsonHelper.Deserialize<VirtualItemBundleMessage>(outboxMessage);
				_003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ISendVirtualItemBundleMessageCallback>(callbackType, callbackId, _003ConSendVirtualItemBundleMessage_003Ec__AnonStorey5E._003C_003Em__42);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
