using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveAlertAcknowledgmentCallbackProxy : CallbackProxy<IReceiveAlertAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveAlertAcknowledgment_003Ec__AnonStorey34
		{
			internal AlertAcknowledgment _acknowledgment;

			internal void _003C_003Em__18(IReceiveAlertAcknowledgmentCallback instance)
			{
				instance.onReceiveAlertAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveAlertAcknowledgmentCallbackProxy";

		public IReceiveAlertAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveAlertAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveAlertAcknowledgmentCallback.onReceiveAlertAcknowledgment");
				try
				{
					onReceiveAlertAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveAlertAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveAlertAcknowledgmentCallbackProxy.onReceiveAlertAcknowledgment");
			try
			{
				_003ConReceiveAlertAcknowledgment_003Ec__AnonStorey34 _003ConReceiveAlertAcknowledgment_003Ec__AnonStorey = new _003ConReceiveAlertAcknowledgment_003Ec__AnonStorey34();
				_003ConReceiveAlertAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<AlertAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveAlertAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveAlertAcknowledgment_003Ec__AnonStorey._003C_003Em__18);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
