using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveCustomRequestAcknowledgmentCallbackProxy : CallbackProxy<IReceiveCustomRequestAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A
		{
			internal CustomRequestAcknowledgment _acknowledgment;

			internal void _003C_003Em__1E(IReceiveCustomRequestAcknowledgmentCallback instance)
			{
				instance.onReceiveCustomRequestAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveCustomRequestAcknowledgmentCallbackProxy";

		public IReceiveCustomRequestAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveCustomRequestAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveCustomRequestAcknowledgmentCallback.onReceiveCustomRequestAcknowledgment");
				try
				{
					onReceiveCustomRequestAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveCustomRequestAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveCustomRequestAcknowledgmentCallbackProxy.onReceiveCustomRequestAcknowledgment");
			try
			{
				_003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A _003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A = new _003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A();
				_003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A._acknowledgment = JsonHelper.Deserialize<CustomRequestAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveCustomRequestAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveCustomRequestAcknowledgment_003Ec__AnonStorey3A._003C_003Em__1E);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
