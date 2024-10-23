using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Virtualgood.Bridge
{
	public class IFindVirtualGoodsCallbackProxy : CallbackProxy<IFindVirtualGoodsCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindVirtualGoods_003Ec__AnonStorey56
		{
			internal List<VirtualGood> _virtualGoods;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__3A(IFindVirtualGoodsCallback instance)
			{
				instance.onFindVirtualGoods(_virtualGoods, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.virtualgood.FindVirtualGoodsCallbackProxy";

		public IFindVirtualGoodsCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindVirtualGoods"))
			{
				DebugUtil.Log("Invoking IFindVirtualGoodsCallback.onFindVirtualGoods");
				try
				{
					onFindVirtualGoods((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindVirtualGoods(int callbackType, int callbackId, string virtualGoods, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindVirtualGoodsCallbackProxy.onFindVirtualGoods");
			try
			{
				_003ConFindVirtualGoods_003Ec__AnonStorey56 _003ConFindVirtualGoods_003Ec__AnonStorey = new _003ConFindVirtualGoods_003Ec__AnonStorey56();
				_003ConFindVirtualGoods_003Ec__AnonStorey._virtualGoods = JsonHelper.Deserialize<List<VirtualGood>>(virtualGoods);
				_003ConFindVirtualGoods_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindVirtualGoods_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindVirtualGoodsCallback>(callbackType, callbackId, _003ConFindVirtualGoods_003Ec__AnonStorey._003C_003Em__3A);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
