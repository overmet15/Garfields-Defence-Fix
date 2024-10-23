using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Pushnotification.Bridge
{
	public class IOpenPushNotificationCallbackProxy : CallbackProxy<IOpenPushNotificationCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConOpenPushNotification_003Ec__AnonStorey4E
		{
			internal JsonObject _data;

			internal void _003C_003Em__32(IOpenPushNotificationCallback instance)
			{
				instance.onOpenPushNotification(_data);
			}
		}

		private static string sNativeClassName = "muneris.bridge.pushnotification.OpenPushNotificationCallbackProxy";

		public IOpenPushNotificationCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onOpenPushNotification"))
			{
				DebugUtil.Log("Invoking IOpenPushNotificationCallback.onOpenPushNotification");
				try
				{
					onOpenPushNotification((int)args[0], (int)args[1], (string)args[2]);
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

		private void onOpenPushNotification(int callbackType, int callbackId, string data)
		{
			DebugUtil.Log("IOpenPushNotificationCallbackProxy.onOpenPushNotification");
			try
			{
				_003ConOpenPushNotification_003Ec__AnonStorey4E _003ConOpenPushNotification_003Ec__AnonStorey4E = new _003ConOpenPushNotification_003Ec__AnonStorey4E();
				_003ConOpenPushNotification_003Ec__AnonStorey4E._data = JsonHelper.Deserialize<JsonObject>(data);
				CallbackCenter.InvokeCallback<IOpenPushNotificationCallback>(callbackType, callbackId, _003ConOpenPushNotification_003Ec__AnonStorey4E._003C_003Em__32);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
