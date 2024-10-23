using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Pushnotification.Bridge
{
	public class IRegisterPushNotificationCallbackProxy : CallbackProxy<IRegisterPushNotificationCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConRegisterPushNotification_003Ec__AnonStorey4F
		{
			internal string _registrationId;

			internal PushNotificationServiceProvider _provider;

			internal MunerisException _exception;

			internal void _003C_003Em__33(IRegisterPushNotificationCallback instance)
			{
				instance.onRegisterPushNotification(_registrationId, _provider, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.pushnotification.RegisterPushNotificationCallbackProxy";

		public IRegisterPushNotificationCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onRegisterPushNotification"))
			{
				DebugUtil.Log("Invoking IRegisterPushNotificationCallback.onRegisterPushNotification");
				try
				{
					onRegisterPushNotification((int)args[0], (int)args[1], (string)args[2], (int)args[3], (string)args[4]);
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

		private void onRegisterPushNotification(int callbackType, int callbackId, string registrationId, int provider, string exception)
		{
			DebugUtil.Log("IRegisterPushNotificationCallbackProxy.onRegisterPushNotification");
			try
			{
				_003ConRegisterPushNotification_003Ec__AnonStorey4F _003ConRegisterPushNotification_003Ec__AnonStorey4F = new _003ConRegisterPushNotification_003Ec__AnonStorey4F();
				_003ConRegisterPushNotification_003Ec__AnonStorey4F._registrationId = registrationId;
				_003ConRegisterPushNotification_003Ec__AnonStorey4F._provider = SerializationHelper.Deserialize<PushNotificationServiceProvider>(provider);
				_003ConRegisterPushNotification_003Ec__AnonStorey4F._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IRegisterPushNotificationCallback>(callbackType, callbackId, _003ConRegisterPushNotification_003Ec__AnonStorey4F._003C_003Em__33);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
