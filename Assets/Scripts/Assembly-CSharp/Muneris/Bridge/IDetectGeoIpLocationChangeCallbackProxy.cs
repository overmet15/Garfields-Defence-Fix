using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IDetectGeoIpLocationChangeCallbackProxy : CallbackProxy<IDetectGeoIpLocationChangeCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConDetectGeoIpLocationChange_003Ec__AnonStorey27
		{
			internal GeoIpLocation _location;

			internal void _003C_003Em__B(IDetectGeoIpLocationChangeCallback instance)
			{
				instance.onDetectGeoIpLocationChange(_location);
			}
		}

		private static string sNativeClassName = "muneris.bridge.DetectGeoIpLocationChangeCallbackProxy";

		public IDetectGeoIpLocationChangeCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onDetectGeoIpLocationChange"))
			{
				DebugUtil.Log("Invoking IDetectGeoIpLocationChangeCallback.onDetectGeoIpLocationChange");
				try
				{
					onDetectGeoIpLocationChange((int)args[0], (int)args[1], (string)args[2]);
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

		private void onDetectGeoIpLocationChange(int callbackType, int callbackId, string location)
		{
			DebugUtil.Log("IDetectGeoIpLocationChangeCallbackProxy.onDetectGeoIpLocationChange");
			try
			{
				_003ConDetectGeoIpLocationChange_003Ec__AnonStorey27 _003ConDetectGeoIpLocationChange_003Ec__AnonStorey = new _003ConDetectGeoIpLocationChange_003Ec__AnonStorey27();
				_003ConDetectGeoIpLocationChange_003Ec__AnonStorey._location = JsonHelper.Deserialize<GeoIpLocation>(location);
				CallbackCenter.InvokeCallback<IDetectGeoIpLocationChangeCallback>(callbackType, callbackId, _003ConDetectGeoIpLocationChange_003Ec__AnonStorey._003C_003Em__B);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
