using System;
using System.Runtime.CompilerServices;
using Muneris.Bridge;
using UnityEngine;

namespace Muneris.Takeover.Bridge
{
	public class ITakeoverCallbackProxy : CallbackProxy<ITakeoverCallback>
	{
		[CompilerGenerated]
		private sealed class _003ConStartTakeoverRequest_003Ec__AnonStorey51
		{
			internal TakeoverEvent _takeoverEvent;

			internal void _003C_003Em__35(ITakeoverCallback instance)
			{
				instance.onStartTakeoverRequest(_takeoverEvent);
			}
		}

		[CompilerGenerated]
		private sealed class _003ConLoadTakeover_003Ec__AnonStorey52
		{
			internal TakeoverEvent _takeoverEvent;

			internal TakeoverResponse _takeoverResponse;

			internal void _003C_003Em__36(ITakeoverCallback instance)
			{
				instance.onLoadTakeover(_takeoverEvent, _takeoverResponse);
			}
		}

		[CompilerGenerated]
		private sealed class _003ConDismissTakeover_003Ec__AnonStorey53
		{
			internal TakeoverEvent _takeoverEvent;

			internal void _003C_003Em__37(ITakeoverCallback instance)
			{
				instance.onDismissTakeover(_takeoverEvent);
			}
		}

		[CompilerGenerated]
		private sealed class _003ConFailTakeover_003Ec__AnonStorey54
		{
			internal TakeoverEvent _takeoverEvent;

			internal MunerisException _exception;

			internal void _003C_003Em__38(ITakeoverCallback instance)
			{
				instance.onFailTakeover(_takeoverEvent, _exception);
			}
		}

		[CompilerGenerated]
		private sealed class _003ConEndTakeoverRequest_003Ec__AnonStorey55
		{
			internal TakeoverEvent _takeoverEvent;

			internal void _003C_003Em__39(ITakeoverCallback instance)
			{
				instance.onEndTakeoverRequest(_takeoverEvent);
			}
		}

		private static string sNativeClassName = "muneris.bridge.takeover.TakeoverCallbackProxy";

		public ITakeoverCallbackProxy()
			: base(sNativeClassName)
		{
		}

		public override AndroidJavaObject Invoke(string methodName, object[] args)
		{
			if (methodName.Equals("onStartTakeoverRequest"))
			{
				DebugUtil.Log("Invoking ITakeoverCallback.onStartTakeoverRequest");
				try
				{
					onStartTakeoverRequest((int)args[0], (int)args[1], (string)args[2]);
				}
				catch (Exception ex)
				{
					DebugUtil.Log("Invocation error", ex);
				}
			}
			else if (methodName.Equals("onLoadTakeover"))
			{
				DebugUtil.Log("Invoking ITakeoverCallback.onLoadTakeover");
				try
				{
					onLoadTakeover((int)args[0], (int)args[1], (string)args[2], (string)args[3]);
				}
				catch (Exception ex2)
				{
					DebugUtil.Log("Invocation error", ex2);
				}
			}
			else if (methodName.Equals("onDismissTakeover"))
			{
				DebugUtil.Log("Invoking ITakeoverCallback.onDismissTakeover");
				try
				{
					onDismissTakeover((int)args[0], (int)args[1], (string)args[2]);
				}
				catch (Exception ex3)
				{
					DebugUtil.Log("Invocation error", ex3);
				}
			}
			else if (methodName.Equals("onFailTakeover"))
			{
				DebugUtil.Log("Invoking ITakeoverCallback.onFailTakeover");
				try
				{
					onFailTakeover((int)args[0], (int)args[1], (string)args[2], (string)args[3]);
				}
				catch (Exception ex4)
				{
					DebugUtil.Log("Invocation error", ex4);
				}
			}
			else if (methodName.Equals("onEndTakeoverRequest"))
			{
				DebugUtil.Log("Invoking ITakeoverCallback.onEndTakeoverRequest");
				try
				{
					onEndTakeoverRequest((int)args[0], (int)args[1], (string)args[2]);
				}
				catch (Exception ex5)
				{
					DebugUtil.Log("Invocation error", ex5);
				}
			}
			else
			{
				DebugUtil.Log("Invocation error, no such method:" + methodName);
			}
			return null;
		}

		private void onStartTakeoverRequest(int callbackType, int callbackId, string takeoverEvent)
		{
			DebugUtil.Log("ITakeoverCallbackProxy.onStartTakeoverRequest");
			try
			{
				_003ConStartTakeoverRequest_003Ec__AnonStorey51 _003ConStartTakeoverRequest_003Ec__AnonStorey = new _003ConStartTakeoverRequest_003Ec__AnonStorey51();
				_003ConStartTakeoverRequest_003Ec__AnonStorey._takeoverEvent = JsonHelper.Deserialize<TakeoverEvent>(takeoverEvent);
				CallbackCenter.InvokeCallback<ITakeoverCallback>(callbackType, callbackId, _003ConStartTakeoverRequest_003Ec__AnonStorey._003C_003Em__35);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}

		private void onLoadTakeover(int callbackType, int callbackId, string takeoverEvent, string takeoverResponse)
		{
			DebugUtil.Log("ITakeoverCallbackProxy.onLoadTakeover");
			try
			{
				_003ConLoadTakeover_003Ec__AnonStorey52 _003ConLoadTakeover_003Ec__AnonStorey = new _003ConLoadTakeover_003Ec__AnonStorey52();
				_003ConLoadTakeover_003Ec__AnonStorey._takeoverEvent = JsonHelper.Deserialize<TakeoverEvent>(takeoverEvent);
				_003ConLoadTakeover_003Ec__AnonStorey._takeoverResponse = JsonHelper.Deserialize<TakeoverResponse>(takeoverResponse);
				CallbackCenter.InvokeCallbackSync<ITakeoverCallback>(callbackType, callbackId, _003ConLoadTakeover_003Ec__AnonStorey._003C_003Em__36);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}

		private void onDismissTakeover(int callbackType, int callbackId, string takeoverEvent)
		{
			DebugUtil.Log("ITakeoverCallbackProxy.onDismissTakeover");
			try
			{
				_003ConDismissTakeover_003Ec__AnonStorey53 _003ConDismissTakeover_003Ec__AnonStorey = new _003ConDismissTakeover_003Ec__AnonStorey53();
				_003ConDismissTakeover_003Ec__AnonStorey._takeoverEvent = JsonHelper.Deserialize<TakeoverEvent>(takeoverEvent);
				CallbackCenter.InvokeCallback<ITakeoverCallback>(callbackType, callbackId, _003ConDismissTakeover_003Ec__AnonStorey._003C_003Em__37);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}

		private void onFailTakeover(int callbackType, int callbackId, string takeoverEvent, string exception)
		{
			DebugUtil.Log("ITakeoverCallbackProxy.onFailTakeover");
			try
			{
				_003ConFailTakeover_003Ec__AnonStorey54 _003ConFailTakeover_003Ec__AnonStorey = new _003ConFailTakeover_003Ec__AnonStorey54();
				_003ConFailTakeover_003Ec__AnonStorey._takeoverEvent = JsonHelper.Deserialize<TakeoverEvent>(takeoverEvent);
				_003ConFailTakeover_003Ec__AnonStorey._exception = JsonHelper.Deserialize<MunerisException>(exception);
				CallbackCenter.InvokeCallback<ITakeoverCallback>(callbackType, callbackId, _003ConFailTakeover_003Ec__AnonStorey._003C_003Em__38);
			}
			catch (Exception exception2)
			{
				DebugUtil.LogException(exception2);
			}
		}

		private void onEndTakeoverRequest(int callbackType, int callbackId, string takeoverEvent)
		{
			DebugUtil.Log("ITakeoverCallbackProxy.onEndTakeoverRequest");
			try
			{
				_003ConEndTakeoverRequest_003Ec__AnonStorey55 _003ConEndTakeoverRequest_003Ec__AnonStorey = new _003ConEndTakeoverRequest_003Ec__AnonStorey55();
				_003ConEndTakeoverRequest_003Ec__AnonStorey._takeoverEvent = JsonHelper.Deserialize<TakeoverEvent>(takeoverEvent);
				CallbackCenter.InvokeCallback<ITakeoverCallback>(callbackType, callbackId, _003ConEndTakeoverRequest_003Ec__AnonStorey._003C_003Em__39);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}
	}
}
