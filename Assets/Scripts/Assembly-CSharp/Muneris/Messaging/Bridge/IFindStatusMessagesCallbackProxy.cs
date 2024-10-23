using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindStatusMessagesCallbackProxy : CallbackProxy<IFindStatusMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindStatusMessages_003Ec__AnonStorey33
		{
			internal List<StatusMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__17(IFindStatusMessagesCallback instance)
			{
				instance.onFindStatusMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindStatusMessagesCallbackProxy";

		public IFindStatusMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindStatusMessages"))
			{
				DebugUtil.Log("Invoking IFindStatusMessagesCallback.onFindStatusMessages");
				try
				{
					onFindStatusMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindStatusMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindStatusMessagesCallbackProxy.onFindStatusMessages");
			try
			{
				_003ConFindStatusMessages_003Ec__AnonStorey33 _003ConFindStatusMessages_003Ec__AnonStorey = new _003ConFindStatusMessages_003Ec__AnonStorey33();
				_003ConFindStatusMessages_003Ec__AnonStorey._messages = JsonHelper.Deserialize<List<StatusMessage>>(messages);
				_003ConFindStatusMessages_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindStatusMessages_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindStatusMessagesCallback>(callbackType, callbackId, _003ConFindStatusMessages_003Ec__AnonStorey._003C_003Em__17);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
