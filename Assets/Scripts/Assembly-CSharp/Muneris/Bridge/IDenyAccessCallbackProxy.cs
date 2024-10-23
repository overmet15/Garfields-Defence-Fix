using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Muneris.Bridge
{
	public class IDenyAccessCallbackProxy : CallbackProxy<IDenyAccessCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConDenyAccess_003Ec__AnonStorey25
		{
			internal MunerisException _exception;

			internal void _003C_003Em__9(IDenyAccessCallback instance)
			{
				instance.onDenyAccess(_exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.DenyAccessCallbackProxy";

		public IDenyAccessCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onDenyAccess"))
			{
				DebugUtil.Log("Invoking IDenyAccessCallback.onDenyAccess");
				try
				{
					onDenyAccess((int)args[0], (int)args[1], (string)args[2]);
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

		private void onDenyAccess(int callbackType, int callbackId, string exception)
		{
			DebugUtil.Log("IDenyAccessCallbackProxy.onDenyAccess");
			try
			{
				_003ConDenyAccess_003Ec__AnonStorey25 _003ConDenyAccess_003Ec__AnonStorey = new _003ConDenyAccess_003Ec__AnonStorey25();
				_003ConDenyAccess_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IDenyAccessCallback>(callbackType, callbackId, _003ConDenyAccess_003Ec__AnonStorey._003C_003Em__9);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
