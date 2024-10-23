using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Virtualgood.Bridge
{
	public class IRestoreVirtualGoodsCallbackProxy : CallbackProxy<IRestoreVirtualGoodsCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConRestoreVirtualGoods_003Ec__AnonStorey58
		{
			internal List<VirtualGood> _virtualGoods;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__3C(IRestoreVirtualGoodsCallback instance)
			{
				instance.onRestoreVirtualGoods(_virtualGoods, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.virtualgood.RestoreVirtualGoodsCallbackProxy";

		public IRestoreVirtualGoodsCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onRestoreVirtualGoods"))
			{
				DebugUtil.Log("Invoking IRestoreVirtualGoodsCallback.onRestoreVirtualGoods");
				try
				{
					onRestoreVirtualGoods((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onRestoreVirtualGoods(int callbackType, int callbackId, string virtualGoods, string callbackContext, string exception)
		{
			DebugUtil.Log("IRestoreVirtualGoodsCallbackProxy.onRestoreVirtualGoods");
			try
			{
				_003ConRestoreVirtualGoods_003Ec__AnonStorey58 _003ConRestoreVirtualGoods_003Ec__AnonStorey = new _003ConRestoreVirtualGoods_003Ec__AnonStorey58();
				_003ConRestoreVirtualGoods_003Ec__AnonStorey._virtualGoods = JsonHelper.Deserialize<List<VirtualGood>>(virtualGoods);
				_003ConRestoreVirtualGoods_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConRestoreVirtualGoods_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IRestoreVirtualGoodsCallback>(callbackType, callbackId, _003ConRestoreVirtualGoods_003Ec__AnonStorey._003C_003Em__3C);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
