using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Virtualgood.Bridge
{
	public class IPurchaseVirtualGoodCallbackProxy : CallbackProxy<IPurchaseVirtualGoodCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConPurchaseVirtualGood_003Ec__AnonStorey57
		{
			internal VirtualGood _virtualGood;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__3B(IPurchaseVirtualGoodCallback instance)
			{
				instance.onPurchaseVirtualGood(_virtualGood, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.virtualgood.PurchaseVirtualGoodCallbackProxy";

		public IPurchaseVirtualGoodCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onPurchaseVirtualGood"))
			{
				DebugUtil.Log("Invoking IPurchaseVirtualGoodCallback.onPurchaseVirtualGood");
				try
				{
					onPurchaseVirtualGood((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onPurchaseVirtualGood(int callbackType, int callbackId, string virtualGood, string callbackContext, string exception)
		{
			DebugUtil.Log("IPurchaseVirtualGoodCallbackProxy.onPurchaseVirtualGood");
			try
			{
				_003ConPurchaseVirtualGood_003Ec__AnonStorey57 _003ConPurchaseVirtualGood_003Ec__AnonStorey = new _003ConPurchaseVirtualGood_003Ec__AnonStorey57();
				_003ConPurchaseVirtualGood_003Ec__AnonStorey._virtualGood = JsonHelper.Deserialize<VirtualGood>(virtualGood);
				_003ConPurchaseVirtualGood_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConPurchaseVirtualGood_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IPurchaseVirtualGoodCallback>(callbackType, callbackId, _003ConPurchaseVirtualGood_003Ec__AnonStorey._003C_003Em__3B);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
