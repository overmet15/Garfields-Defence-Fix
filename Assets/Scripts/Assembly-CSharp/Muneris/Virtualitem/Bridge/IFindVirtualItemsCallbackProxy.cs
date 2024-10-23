using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Virtualitem.Bridge
{
	public class IFindVirtualItemsCallbackProxy : CallbackProxy<IFindVirtualItemsCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindVirtualItems_003Ec__AnonStorey59
		{
			internal List<VirtualItem> _virtualItems;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__3D(IFindVirtualItemsCallback instance)
			{
				instance.onFindVirtualItems(_virtualItems, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.virtualitem.FindVirtualItemsCallbackProxy";

		public IFindVirtualItemsCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindVirtualItems"))
			{
				DebugUtil.Log("Invoking IFindVirtualItemsCallback.onFindVirtualItems");
				try
				{
					onFindVirtualItems((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindVirtualItems(int callbackType, int callbackId, string virtualItems, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindVirtualItemsCallbackProxy.onFindVirtualItems");
			try
			{
				_003ConFindVirtualItems_003Ec__AnonStorey59 _003ConFindVirtualItems_003Ec__AnonStorey = new _003ConFindVirtualItems_003Ec__AnonStorey59();
				_003ConFindVirtualItems_003Ec__AnonStorey._virtualItems = JsonHelper.Deserialize<List<VirtualItem>>(virtualItems);
				_003ConFindVirtualItems_003Ec__AnonStorey._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindVirtualItems_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindVirtualItemsCallback>(callbackType, callbackId, _003ConFindVirtualItems_003Ec__AnonStorey._003C_003Em__3D);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
