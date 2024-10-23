using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Appupgradenotification.Bridge
{
	public class IReceiveAppUpgradeNotificationCallbackProxy : CallbackProxy<IReceiveAppUpgradeNotificationCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReceiveAppUpgradeNotification_003Ec__AnonStorey21
		{
			internal AppUpgradeNotification _appUpgradeNotification;

			internal void _003C_003Em__5(IReceiveAppUpgradeNotificationCallback instance)
			{
				instance.onReceiveAppUpgradeNotification(_appUpgradeNotification);
			}
		}

		private static string sNativeClassName = "muneris.bridge.appupgradenotification.ReceiveAppUpgradeNotificationCallbackProxy";

		public IReceiveAppUpgradeNotificationCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReceiveAppUpgradeNotification"))
			{
				DebugUtil.Log("Invoking IReceiveAppUpgradeNotificationCallback.onReceiveAppUpgradeNotification");
				try
				{
					onReceiveAppUpgradeNotification((int)args[0], (int)args[1], (string)args[2]);
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

		private void onReceiveAppUpgradeNotification(int callbackType, int callbackId, string appUpgradeNotification)
		{
			DebugUtil.Log("IReceiveAppUpgradeNotificationCallbackProxy.onReceiveAppUpgradeNotification");
			try
			{
				_003ConReceiveAppUpgradeNotification_003Ec__AnonStorey21 _003ConReceiveAppUpgradeNotification_003Ec__AnonStorey = new _003ConReceiveAppUpgradeNotification_003Ec__AnonStorey21();
				_003ConReceiveAppUpgradeNotification_003Ec__AnonStorey._appUpgradeNotification = JsonHelper.Deserialize<AppUpgradeNotification>(appUpgradeNotification);
				CallbackCenter.InvokeCallback<IReceiveAppUpgradeNotificationCallback>(callbackType, callbackId, _003ConReceiveAppUpgradeNotification_003Ec__AnonStorey._003C_003Em__5);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
