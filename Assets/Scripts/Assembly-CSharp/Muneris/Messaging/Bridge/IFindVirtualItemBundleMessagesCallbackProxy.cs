using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindVirtualItemBundleMessagesCallbackProxy : CallbackProxy<IFindVirtualItemBundleMessagesCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A
		{
			internal List<VirtualItemBundleMessage> _messages;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__3E(IFindVirtualItemBundleMessagesCallback instance)
			{
				instance.onFindVirtualItemBundleMessages(_messages, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindVirtualItemBundleMessagesCallbackProxy";

		public IFindVirtualItemBundleMessagesCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindVirtualItemBundleMessages"))
			{
				DebugUtil.Log("Invoking IFindVirtualItemBundleMessagesCallback.onFindVirtualItemBundleMessages");
				try
				{
					onFindVirtualItemBundleMessages((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindVirtualItemBundleMessages(int callbackType, int callbackId, string messages, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindVirtualItemBundleMessagesCallbackProxy.onFindVirtualItemBundleMessages");
			try
			{
				_003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A _003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A = new _003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A();
				_003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A._messages = JsonHelper.Deserialize<List<VirtualItemBundleMessage>>(messages);
				_003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindVirtualItemBundleMessagesCallback>(callbackType, callbackId, _003ConFindVirtualItemBundleMessages_003Ec__AnonStorey5A._003C_003Em__3E);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
