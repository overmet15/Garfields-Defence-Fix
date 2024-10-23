using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Messaging.Bridge
{
	public class IFindChannelsCallbackProxy : CallbackProxy<IFindChannelsCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConFindChannels_003Ec__AnonStorey2D
		{
			internal List<Channel> _channels;

			internal CallbackContext _callbackContext;

			internal MunerisException _exception;

			internal void _003C_003Em__11(IFindChannelsCallback instance)
			{
				instance.onFindChannels(_channels, _callbackContext, _exception);
			}
		}

		private static string sNativeClassName = "muneris.bridge.messaging.FindChannelsCallbackProxy";

		public IFindChannelsCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onFindChannels"))
			{
				DebugUtil.Log("Invoking IFindChannelsCallback.onFindChannels");
				try
				{
					onFindChannels((int)args[0], (int)args[1], (string)args[2], (string)args[3], (string)args[4]);
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

		private void onFindChannels(int callbackType, int callbackId, string channels, string callbackContext, string exception)
		{
			DebugUtil.Log("IFindChannelsCallbackProxy.onFindChannels");
			try
			{
				_003ConFindChannels_003Ec__AnonStorey2D _003ConFindChannels_003Ec__AnonStorey2D = new _003ConFindChannels_003Ec__AnonStorey2D();
				_003ConFindChannels_003Ec__AnonStorey2D._channels = JsonHelper.Deserialize<List<Channel>>(channels);
				_003ConFindChannels_003Ec__AnonStorey2D._callbackContext = JsonHelper.Deserialize<CallbackContext>(callbackContext);
				_003ConFindChannels_003Ec__AnonStorey2D._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<IFindChannelsCallback>(callbackType, callbackId, _003ConFindChannels_003Ec__AnonStorey2D._003C_003Em__11);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}
	}
}
