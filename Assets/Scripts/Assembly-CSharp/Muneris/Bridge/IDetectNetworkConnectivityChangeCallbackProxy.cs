using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IDetectNetworkConnectivityChangeCallbackProxy : CallbackProxy<IDetectNetworkConnectivityChangeCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConDetectNetworkConnectivityChange_003Ec__AnonStorey28
		{
			internal bool _online;

			internal void _003C_003Em__C(IDetectNetworkConnectivityChangeCallback instance)
			{
				instance.onDetectNetworkConnectivityChange(_online);
			}
		}

		private static string sNativeClassName = "muneris.bridge.DetectNetworkConnectivityChangeCallbackProxy";

		public IDetectNetworkConnectivityChangeCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onDetectNetworkConnectivityChange"))
			{
				DebugUtil.Log("Invoking IDetectNetworkConnectivityChangeCallback.onDetectNetworkConnectivityChange");
				try
				{
					onDetectNetworkConnectivityChange((int)args[0], (int)args[1], (bool)args[2]);
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

		private void onDetectNetworkConnectivityChange(int callbackType, int callbackId, bool online)
		{
			DebugUtil.Log("IDetectNetworkConnectivityChangeCallbackProxy.onDetectNetworkConnectivityChange");
			try
			{
				_003ConDetectNetworkConnectivityChange_003Ec__AnonStorey28 _003ConDetectNetworkConnectivityChange_003Ec__AnonStorey = new _003ConDetectNetworkConnectivityChange_003Ec__AnonStorey28();
				_003ConDetectNetworkConnectivityChange_003Ec__AnonStorey._online = online;
				CallbackCenter.InvokeCallback<IDetectNetworkConnectivityChangeCallback>(callbackType, callbackId, _003ConDetectNetworkConnectivityChange_003Ec__AnonStorey._003C_003Em__C);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
