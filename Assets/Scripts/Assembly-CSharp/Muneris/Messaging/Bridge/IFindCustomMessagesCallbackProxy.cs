using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindCustomMessagesCallbackProxy : CallbackProxy<IFindCustomMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindCustomMessages_003Ec__AnonStorey2F
		{
			internal List<CustomMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__13(IFindCustomMessagesCallback instance)
			{
				instance.onFindCustomMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindCustomMessagesCallbackProxy";

		public IFindCustomMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindCustomMessages"))
			{
				DebugUtil.Log("Invoking IFindCustomMessagesCallback.onFindCustomMessages");
				try
				{
					onFindCustomMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindCustomMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindCustomMessagesCallbackProxy.onFindCustomMessages");
			try
			{
				_003ConFindCustomMessages_003Ec__AnonStorey2F _003ConFindCustomMessages_003Ec__AnonStorey2F = new _003ConFindCustomMessages_003Ec__AnonStorey2F();
				_003ConFindCustomMessages_003Ec__AnonStorey2F._messages = JsonHelper.Deserialize<List<CustomMessage>>(messages);
				_003ConFindCustomMessages_003Ec__AnonStorey2F._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindCustomMessages_003Ec__AnonStorey2F._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindCustomMessagesCallback>(callbackType, callbackId, _003ConFindCustomMessages_003Ec__AnonStorey2F._003C_003Em__13);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
