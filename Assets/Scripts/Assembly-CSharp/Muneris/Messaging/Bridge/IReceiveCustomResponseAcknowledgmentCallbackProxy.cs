using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomResponseAcknowledgmentCallbackProxy : CallbackProxy<IReceiveCustomResponseAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C
		{
			internal CustomResponseAcknowledgment _acknowledgment;

			internal void _003C_003Em__20(IReceiveCustomResponseAcknowledgmentCallback instance)
			{
				instance.onReceiveCustomResponseAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomResponseAcknowledgmentCallbackProxy";

		public IReceiveCustomResponseAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomResponseAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveCustomResponseAcknowledgmentCallback.onReceiveCustomResponseAcknowledgment");
				try
				{
					onReceiveCustomResponseAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomResponseAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveCustomResponseAcknowledgmentCallbackProxy.onReceiveCustomResponseAcknowledgment");
			try
			{
				_003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C _003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C = new _003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C();
				_003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C._acknowledgment = JsonHelper.Deserialize<CustomResponseAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveCustomResponseAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveCustomResponseAcknowledgment_003Ec__AnonStorey3C._003C_003Em__20);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
