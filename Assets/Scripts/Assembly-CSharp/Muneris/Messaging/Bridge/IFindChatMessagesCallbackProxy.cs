using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindChatMessagesCallbackProxy : CallbackProxy<IFindChatMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindChatMessages_003Ec__AnonStorey2E
		{
			internal List<ChatMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__12(IFindChatMessagesCallback instance)
			{
				instance.onFindChatMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindChatMessagesCallbackProxy";

		public IFindChatMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindChatMessages"))
			{
				DebugUtil.Log("Invoking IFindChatMessagesCallback.onFindChatMessages");
				try
				{
					onFindChatMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindChatMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindChatMessagesCallbackProxy.onFindChatMessages");
			try
			{
				_003ConFindChatMessages_003Ec__AnonStorey2E _003ConFindChatMessages_003Ec__AnonStorey2E = new _003ConFindChatMessages_003Ec__AnonStorey2E();
				_003ConFindChatMessages_003Ec__AnonStorey2E._messages = JsonHelper.Deserialize<List<ChatMessage>>(messages);
				_003ConFindChatMessages_003Ec__AnonStorey2E._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindChatMessages_003Ec__AnonStorey2E._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindChatMessagesCallback>(callbackType, callbackId, _003ConFindChatMessages_003Ec__AnonStorey2E._003C_003Em__12);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
