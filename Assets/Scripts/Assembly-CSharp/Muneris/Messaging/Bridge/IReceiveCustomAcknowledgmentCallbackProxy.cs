using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomAcknowledgmentCallbackProxy : CallbackProxy<IReceiveCustomAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomAcknowledgment_003Ec__AnonStorey38
		{
			internal CustomAcknowledgment _acknowledgment;

			internal void _003C_003Em__1C(IReceiveCustomAcknowledgmentCallback instance)
			{
				instance.onReceiveCustomAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomAcknowledgmentCallbackProxy";

		public IReceiveCustomAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveCustomAcknowledgmentCallback.onReceiveCustomAcknowledgment");
				try
				{
					onReceiveCustomAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveCustomAcknowledgmentCallbackProxy.onReceiveCustomAcknowledgment");
			try
			{
				_003ConReceiveCustomAcknowledgment_003Ec__AnonStorey38 _003ConReceiveCustomAcknowledgment_003Ec__AnonStorey = new _003ConReceiveCustomAcknowledgment_003Ec__AnonStorey38();
				_003ConReceiveCustomAcknowledgment_003Ec__AnonStorey._acknowledgment = JsonHelper.Deserialize<CustomAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveCustomAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveCustomAcknowledgment_003Ec__AnonStorey._003C_003Em__1C);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
