using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveChatMessageCallbackProxy : CallbackProxy<IReceiveChatMessageCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveChatMessage_003Ec__AnonStorey37
		{
			internal ChatMessage _message;

			internal void _003C_003Em__1B(IReceiveChatMessageCallback instance)
			{
				instance.onReceiveChatMessage(_message);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveChatMessageCallbackProxy";

		public IReceiveChatMessageCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveChatMessage"))
			{
				DebugUtil.Log("Invoking IReceiveChatMessageCallback.onReceiveChatMessage");
				try
				{
					onReceiveChatMessage((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveChatMessage(int callbackType, int callbackId, string message)
		{
			DebugUtil.Log("IReceiveChatMessageCallbackProxy.onReceiveChatMessage");
			try
			{
				_003ConReceiveChatMessage_003Ec__AnonStorey37 _003ConReceiveChatMessage_003Ec__AnonStorey = new _003ConReceiveChatMessage_003Ec__AnonStorey37();
				_003ConReceiveChatMessage_003Ec__AnonStorey._message = JsonHelper.Deserialize<ChatMessage>(message);
				CallbackCenter.InvokeCallback<IReceiveChatMessageCallback>(callbackType, callbackId, _003ConReceiveChatMessage_003Ec__AnonStorey._003C_003Em__1B);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
