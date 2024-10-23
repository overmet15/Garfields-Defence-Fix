using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindMessagesCallbackProxy : CallbackProxy<IFindMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindMessages_003Ec__AnonStorey32
		{
			internal List<Message> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__16(IFindMessagesCallback instance)
			{
				instance.onFindMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindMessagesCallbackProxy";

		public IFindMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindMessages"))
			{
				DebugUtil.Log("Invoking IFindMessagesCallback.onFindMessages");
				try
				{
					onFindMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindMessagesCallbackProxy.onFindMessages");
			try
			{
				_003ConFindMessages_003Ec__AnonStorey32 _003ConFindMessages_003Ec__AnonStorey = new _003ConFindMessages_003Ec__AnonStorey32();
				_003ConFindMessages_003Ec__AnonStorey._messages = JsonHelper.Deserialize<List<Message>>(messages);
				_003ConFindMessages_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindMessages_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindMessagesCallback>(callbackType, callbackId, _003ConFindMessages_003Ec__AnonStorey._003C_003Em__16);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
