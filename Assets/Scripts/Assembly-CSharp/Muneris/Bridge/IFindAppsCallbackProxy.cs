using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IFindAppsCallbackProxy : CallbackProxy<IFindAppsCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindApps_003Ec__AnonStorey29
		{
			internal List<IApp> _apps;

			internal FindAppsCommand _more;

			internal CallbackContext _callbackContext;

			internal MunerisException _munerisException;

			internal void _003C_003Em__D(IFindAppsCallback instance)
			{
				instance.onFindApps(_apps, _more, _callbackContext, _munerisException);
			}
		}

		private static string sNativeClassName = "muneris.bridge.FindAppsCallbackProxy";

		public IFindAppsCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindApps"))
			{
				DebugUtil.Log("Invoking IFindAppsCallback.onFindApps");
				try
				{
					onFindApps((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5]);
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

		private void onFindApps(int callbackType, int callbackId, string apps, string more, string callbackContext, string munerisException)
		{
			DebugUtil.Log("IFindAppsCallbackProxy.onFindApps");
			try
			{
				_003ConFindApps_003Ec__AnonStorey29 _003ConFindApps_003Ec__AnonStorey = new _003ConFindApps_003Ec__AnonStorey29();
				_003ConFindApps_003Ec__AnonStorey._apps = JsonHelper.Deserialize<List<IApp>>(apps);
				_003ConFindApps_003Ec__AnonStorey._more = JsonHelper.Deserialize<FindAppsCommand>(more);
				_003ConFindApps_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindApps_003Ec__AnonStorey._munerisException = JsonHelper.Deserialize<MunerisException>(munerisException);
				CallbackCenter.InvokeCallback<IFindAppsCallback>(callbackType, callbackId, _003ConFindApps_003Ec__AnonStorey._003C_003Em__D);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
