using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindCustomResponseMessagesCallbackProxy : CallbackProxy<IFindCustomResponseMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindCustomResponseMessages_003Ec__AnonStorey31
		{
			internal List<CustomResponseMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__15(IFindCustomResponseMessagesCallback instance)
			{
				instance.onFindCustomResponseMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindCustomResponseMessagesCallbackProxy";

		public IFindCustomResponseMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindCustomResponseMessages"))
			{
				DebugUtil.Log("Invoking IFindCustomResponseMessagesCallback.onFindCustomResponseMessages");
				try
				{
					onFindCustomResponseMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindCustomResponseMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindCustomResponseMessagesCallbackProxy.onFindCustomResponseMessages");
			try
			{
				_003ConFindCustomResponseMessages_003Ec__AnonStorey31 _003ConFindCustomResponseMessages_003Ec__AnonStorey = new _003ConFindCustomResponseMessages_003Ec__AnonStorey31();
				_003ConFindCustomResponseMessages_003Ec__AnonStorey._messages = JsonHelper.Deserialize<List<CustomResponseMessage>>(messages);
				_003ConFindCustomResponseMessages_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindCustomResponseMessages_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindCustomResponseMessagesCallback>(callbackType, callbackId, _003ConFindCustomResponseMessages_003Ec__AnonStorey._003C_003Em__15);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
