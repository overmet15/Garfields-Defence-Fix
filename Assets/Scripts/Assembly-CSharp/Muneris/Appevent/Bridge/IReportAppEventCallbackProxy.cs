using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Appevent.Bridge
{
	public class IReportAppEventCallbackProxy : CallbackProxy<IReportAppEventCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConReportAppEvent_003Ec__AnonStorey20
		{
			internal string _eventName;

			internal Dictionary<string, string> _parameters;

			internal CallbackContext _callbackContext;

			internal void _003C_003Em__4(IReportAppEventCallback instance)
			{
				instance.onReportAppEvent(_eventName, _parameters, _callbackContext);
			}
		}

		private static string sNativeClassName = "muneris.bridge.appevent.ReportAppEventCallbackProxy";

		public IReportAppEventCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onReportAppEvent"))
			{
				DebugUtil.Log("Invoking IReportAppEventCallback.onReportAppEvent");
				try
				{
					onReportAppEvent((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onReportAppEvent(int callbackType, int callbackId, string eventName, string parameters, string callbackContext)
		{
			DebugUtil.Log("IReportAppEventCallbackProxy.onReportAppEvent");
			try
			{
				_003ConReportAppEvent_003Ec__AnonStorey20 _003ConReportAppEvent_003Ec__AnonStorey = new _003ConReportAppEvent_003Ec__AnonStorey20();
				_003ConReportAppEvent_003Ec__AnonStorey._eventName = eventName;
				_003ConReportAppEvent_003Ec__AnonStorey._parameters = JsonHelper.Deserialize<Dictionary<string, string>>(parameters);
				_003ConReportAppEvent_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				CallbackCenter.InvokeCallback<IReportAppEventCallback>(callbackType, callbackId, _003ConReportAppEvent_003Ec__AnonStorey._003C_003Em__4);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
