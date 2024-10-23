using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindAlertMessagesCallbackProxy : CallbackProxy<IFindAlertMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindAlertMessages_003Ec__AnonStorey2C
		{
			internal List<AlertMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__10(IFindAlertMessagesCallback instance)
			{
				instance.onFindAlertMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindAlertMessagesCallbackProxy";

		public IFindAlertMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindAlertMessages"))
			{
				DebugUtil.Log("Invoking IFindAlertMessagesCallback.onFindAlertMessages");
				try
				{
					onFindAlertMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindAlertMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindAlertMessagesCallbackProxy.onFindAlertMessages");
			try
			{
				_003ConFindAlertMessages_003Ec__AnonStorey2C _003ConFindAlertMessages_003Ec__AnonStorey2C = new _003ConFindAlertMessages_003Ec__AnonStorey2C();
				_003ConFindAlertMessages_003Ec__AnonStorey2C._messages = JsonHelper.Deserialize<List<AlertMessage>>(messages);
				_003ConFindAlertMessages_003Ec__AnonStorey2C._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindAlertMessages_003Ec__AnonStorey2C._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindAlertMessagesCallback>(callbackType, callbackId, _003ConFindAlertMessages_003Ec__AnonStorey2C._003C_003Em__10);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
