using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IDetectEnvarsCargoChangeCallbackProxy : CallbackProxy<IDetectEnvarsCargoChangeCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConDetectEnvarsCargoChange_003Ec__AnonStorey26
		{
			internal JsonObject _cargo;

			internal void _003C_003Em__A(IDetectEnvarsCargoChangeCallback instance)
			{
				instance.onDetectEnvarsCargoChange(_cargo);
			}
		}

		private static string sNativeClassName = "muneris.bridge.DetectEnvarsCargoChangeCallbackProxy";

		public IDetectEnvarsCargoChangeCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onDetectEnvarsCargoChange"))
			{
				DebugUtil.Log("Invoking IDetectEnvarsCargoChangeCallback.onDetectEnvarsCargoChange");
				try
				{
					onDetectEnvarsCargoChange((int)args[0], (int)args[1], (string)args[2]);
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

		private void onDetectEnvarsCargoChange(int callbackType, int callbackId, string cargo)
		{
			DebugUtil.Log("IDetectEnvarsCargoChangeCallbackProxy.onDetectEnvarsCargoChange");
			try
			{
				_003ConDetectEnvarsCargoChange_003Ec__AnonStorey26 _003ConDetectEnvarsCargoChange_003Ec__AnonStorey = new _003ConDetectEnvarsCargoChange_003Ec__AnonStorey26();
				_003ConDetectEnvarsCargoChange_003Ec__AnonStorey._cargo = JsonHelper.Deserialize<JsonObject>(cargo);
				CallbackCenter.InvokeCallback<IDetectEnvarsCargoChangeCallback>(callbackType, callbackId, _003ConDetectEnvarsCargoChange_003Ec__AnonStorey._003C_003Em__A);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
