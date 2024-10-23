using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IReceiveStatusAcknowledgmentCallbackProxy : CallbackProxy<IReceiveStatusAcknowledgmentCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E
		{
			internal StatusAcknowledgment _acknowledgment;

			internal void _003C_003Em__22(IReceiveStatusAcknowledgmentCallback instance)
			{
				instance.onReceiveStatusAcknowledgment(_acknowledgment);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.ReceiveStatusAcknowledgmentCallbackProxy";

		public IReceiveStatusAcknowledgmentCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveStatusAcknowledgment"))
			{
				DebugUtil.Log("Invoking IReceiveStatusAcknowledgmentCallback.onReceiveStatusAcknowledgment");
				try
				{
					onReceiveStatusAcknowledgment((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveStatusAcknowledgment(int callbackType, int callbackId, string acknowledgment)
		{
			DebugUtil.Log("IReceiveStatusAcknowledgmentCallbackProxy.onReceiveStatusAcknowledgment");
			try
			{
				_003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E _003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E = new _003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E();
				_003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E._acknowledgment = JsonHelper.Deserialize<StatusAcknowledgment>(acknowledgment);
				CallbackCenter.InvokeCallback<IReceiveStatusAcknowledgmentCallback>(callbackType, callbackId, _003ConReceiveStatusAcknowledgment_003Ec__AnonStorey3E._003C_003Em__22);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
