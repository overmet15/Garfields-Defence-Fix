using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Pushnotification.Bridge
{
	public class IUnregisterPushNotificationCallbackProxy : CallbackProxy<IUnregisterPushNotificationCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConUnregisterPushNotification_003Ec__AnonStorey50
		{
			internal string _registrationId;

			internal PushNotificationServiceProvider _provider;

			internal MunerisException _exception;

			internal void _003C_003Em__34(IUnregisterPushNotificationCallback instance)
			{
				instance.onUnregisterPushNotification(_registrationId, _provider, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.pushnotification.UnregisterPushNotificationCallbackProxy";

		public IUnregisterPushNotificationCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onUnregisterPushNotification"))
			{
				DebugUtil.Log("Invoking IUnregisterPushNotificationCallback.onUnregisterPushNotification");
				try
				{
					onUnregisterPushNotification((int)args[0], (int)args[1], (string)args[2], (int)args[3], (string)args[4]);
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

		private void onUnregisterPushNotification(int callbackType, int callbackId, string registrationId, int provider, string exception)
		{
			DebugUtil.Log("IUnregisterPushNotificationCallbackProxy.onUnregisterPushNotification");
			try
			{
				_003ConUnregisterPushNotification_003Ec__AnonStorey50 _003ConUnregisterPushNotification_003Ec__AnonStorey = new _003ConUnregisterPushNotification_003Ec__AnonStorey50();
				_003ConUnregisterPushNotification_003Ec__AnonStorey._registrationId = registrationId;
				_003ConUnregisterPushNotification_003Ec__AnonStorey._provider = SerializationHelper.Deserialize<PushNotificationServiceProvider>(provider);
				_003ConUnregisterPushNotification_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IUnregisterPushNotificationCallback>(callbackType, callbackId, _003ConUnregisterPushNotification_003Ec__AnonStorey._003C_003Em__34);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
