using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindCustomRequestMessagesCallbackProxy : CallbackProxy<IFindCustomRequestMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindCustomRequestMessages_003Ec__AnonStorey30
		{
			internal List<CustomRequestMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__14(IFindCustomRequestMessagesCallback instance)
			{
				instance.onFindCustomRequestMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindCustomRequestMessagesCallbackProxy";

		public IFindCustomRequestMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindCustomRequestMessages"))
			{
				DebugUtil.Log("Invoking IFindCustomRequestMessagesCallback.onFindCustomRequestMessages");
				try
				{
					onFindCustomRequestMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindCustomRequestMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindCustomRequestMessagesCallbackProxy.onFindCustomRequestMessages");
			try
			{
				_003ConFindCustomRequestMessages_003Ec__AnonStorey30 _003ConFindCustomRequestMessages_003Ec__AnonStorey = new _003ConFindCustomRequestMessages_003Ec__AnonStorey30();
				_003ConFindCustomRequestMessages_003Ec__AnonStorey._messages = JsonHelper.Deserialize<List<CustomRequestMessage>>(messages);
				_003ConFindCustomRequestMessages_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindCustomRequestMessages_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindCustomRequestMessagesCallback>(callbackType, callbackId, _003ConFindCustomRequestMessages_003Ec__AnonStorey._003C_003Em__14);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
