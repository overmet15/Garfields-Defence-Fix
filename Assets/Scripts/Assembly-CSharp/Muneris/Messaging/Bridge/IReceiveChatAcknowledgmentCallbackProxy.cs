using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveChatAcknowledgmentCallbackProxy : CallbackProxy<IReceiveChatAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveChatAcknowledgment_003Ec__AnonStorey36
		{
			internal ChatAcknowledgment _acknowledgment;

			internal void _003C_003Em__1A(IReceiveChatAcknowledgmentCallback instance)
			{
				instance.onReceiveChatAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveChatAcknowledgmentCallbackProxy";

		public IReceiveChatAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveChatAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveChatAcknowledgmentCallback.onReceiveChatAcknowledgment");
				try
				{
					onReceiveChatAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveChatAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveChatAcknowledgmentCallbackProxy.onReceiveChatAcknowledgment");
			try
			{
				_003ConReceiveChatAcknowledgment_003Ec__AnonStorey36 _003ConReceiveChatAcknowledgment_003Ec__AnonStorey = new _003ConReceiveChatAcknowledgment_003Ec__AnonStorey36();
				_003ConReceiveChatAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<ChatAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveChatAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveChatAcknowledgment_003Ec__AnonStorey._003C_003Em__1A);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
