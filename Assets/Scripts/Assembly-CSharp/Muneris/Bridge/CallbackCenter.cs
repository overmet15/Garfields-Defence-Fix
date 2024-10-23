using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Muneris.Bridge
{
	public class CallbackCenter : Singleton<CallbackCenter>
	{
		private static class InlineCallbackRegistry<T> where T : ICallback
		{
			private class Integer
			{
				private int value;

				public Integer(int value)
				{
					this.value = value;
				}

				public override bool Equals(object obj)
				{
					Integer integer = obj as Integer;
					return integer != null && value == integer.value;
				}

				public override int GetHashCode()
				{
					return value;
				}

				public static implicit operator Integer(int value)
				{
					return new Integer(value);
				}

				public static implicit operator int(Integer integer)
				{
					return integer.value;
				}

				public static int operator +(Integer one, Integer two)
				{
					return one.value + two.value;
				}

				public static Integer operator +(int one, Integer two)
				{
					return new Integer(one + two);
				}

				public static int operator -(Integer one, Integer two)
				{
					return one.value - two.value;
				}

				public static Integer operator -(int one, Integer two)
				{
					return new Integer(one - two);
				}
			}

			private static Dictionary<Integer, T> sItems = new Dictionary<Integer, T>();

			private static object sLock = new object();

			private static int sId = 1;

			public static int Add(T item)
			{
				//Discarded unreachable code: IL_004c
				lock (sLock)
				{
					int num = sId;
					sItems[num] = item;
					sId++;
					if (sId == 0)
					{
						sId++;
					}
					return num;
				}
			}

			public static T Get(int id)
			{
				//Discarded unreachable code: IL_0046
				lock (sLock)
				{
					if (sItems.ContainsKey(id))
					{
						return sItems[id];
					}
					return default(T);
				}
			}

			public static T Remove(int id)
			{
				//Discarded unreachable code: IL_0059
				lock (sLock)
				{
					if (sItems.ContainsKey(id))
					{
						T result = sItems[id];
						sItems.Remove(id);
						return result;
					}
					return default(T);
				}
			}
		}

		public delegate void InvokeCallbackDelegate<T>(T callback) where T : class, ICallback;

		public delegate void InvokeCallbackFunctionDelegate<D>(D callbackFunction) where D : class;

		[CompilerGenerated]
		private sealed class _003CInvokeGlobalCallbacks_003Ec__AnonStorey22<T> where T : class, ICallback
		{
			internal InvokeCallbackDelegate<T> invokeDelegate;
		}

		[CompilerGenerated]
		private sealed class _003CInvokeGlobalCallbacks_003Ec__AnonStorey23<T> where T : class, ICallback
		{
			internal T callbackAsT;

			internal _003CInvokeGlobalCallbacks_003Ec__AnonStorey22<T> _003C_003Ef__ref_002434;

			internal void _003C_003Em__7()
			{
				_003C_003Ef__ref_002434.invokeDelegate(callbackAsT);
			}
		}

		[CompilerGenerated]
		private sealed class _003CInvokeInlineCallback_003Ec__AnonStorey24<T> where T : class, ICallback
		{
			internal InvokeCallbackDelegate<T> invokeDelegate;

			internal T callback;

			internal void _003C_003Em__8()
			{
				invokeDelegate(callback);
			}
		}

		public static readonly int CALLBACK_TYPE_GLOBAL;

		public static readonly int CALLBACK_TYPE_INLINE = 1;

		private IList<ICallback> mGlobalCallbacks = new List<ICallback>();

		private object mGlobalCallbacksLock = new object();

		private IDictionary<Type, ICallbackProxy> mCallbackProxies = new Dictionary<Type, ICallbackProxy>();

		private object mCallbackProxiesLock = new object();

		private CallbackCenter()
		{
			DebugUtil.Log("Initializing Muneris.Bridge.CallbackCenter");
			TaskDispatcher.Init();
		}

		public static void InvokeCallbackSync<T>(int callbackType, int callbackId, InvokeCallbackDelegate<T> invokeDelegate) where T : class, ICallback
		{
			InvokeCallback(callbackType, callbackId, invokeDelegate, true);
		}

		public static void InvokeCallback<T>(int callbackType, int callbackId, InvokeCallbackDelegate<T> invokeDelegate, bool synchronous = false) where T : class, ICallback
		{
			if (callbackType == CALLBACK_TYPE_GLOBAL)
			{
				InvokeGlobalCallbacks(invokeDelegate, synchronous);
			}
			else
			{
				InvokeInlineCallback(callbackId, invokeDelegate, synchronous);
			}
		}

		private static void InvokeGlobalCallbacks<T>(InvokeCallbackDelegate<T> invokeDelegate, bool synchronous) where T : class, ICallback
		{
			_003CInvokeGlobalCallbacks_003Ec__AnonStorey22<T> _003CInvokeGlobalCallbacks_003Ec__AnonStorey = new _003CInvokeGlobalCallbacks_003Ec__AnonStorey22<T>();
			_003CInvokeGlobalCallbacks_003Ec__AnonStorey.invokeDelegate = invokeDelegate;
			IList<ICallback> callbacks = Singleton<CallbackCenter>.Instance.GetCallbacks();
			foreach (ICallback item in callbacks)
			{
				if (item is T)
				{
					_003CInvokeGlobalCallbacks_003Ec__AnonStorey23<T> _003CInvokeGlobalCallbacks_003Ec__AnonStorey2 = new _003CInvokeGlobalCallbacks_003Ec__AnonStorey23<T>();
					_003CInvokeGlobalCallbacks_003Ec__AnonStorey2._003C_003Ef__ref_002434 = _003CInvokeGlobalCallbacks_003Ec__AnonStorey;
					_003CInvokeGlobalCallbacks_003Ec__AnonStorey2.callbackAsT = item as T;
					if (synchronous)
					{
						_003CInvokeGlobalCallbacks_003Ec__AnonStorey.invokeDelegate(_003CInvokeGlobalCallbacks_003Ec__AnonStorey2.callbackAsT);
					}
					else
					{
						TaskDispatcher.RunOnMainThread(_003CInvokeGlobalCallbacks_003Ec__AnonStorey2._003C_003Em__7);
					}
				}
			}
		}

		private static void InvokeInlineCallback<T>(int callbackId, InvokeCallbackDelegate<T> invokeDelegate, bool synchronous) where T : class, ICallback
		{
			_003CInvokeInlineCallback_003Ec__AnonStorey24<T> _003CInvokeInlineCallback_003Ec__AnonStorey = new _003CInvokeInlineCallback_003Ec__AnonStorey24<T>();
			_003CInvokeInlineCallback_003Ec__AnonStorey.invokeDelegate = invokeDelegate;
			_003CInvokeInlineCallback_003Ec__AnonStorey.callback = InlineCallbackRegistry<T>.Remove(callbackId);
			if (_003CInvokeInlineCallback_003Ec__AnonStorey.callback != null)
			{
				if (synchronous)
				{
					_003CInvokeInlineCallback_003Ec__AnonStorey.invokeDelegate(_003CInvokeInlineCallback_003Ec__AnonStorey.callback);
				}
				else
				{
					TaskDispatcher.RunOnMainThread(_003CInvokeInlineCallback_003Ec__AnonStorey._003C_003Em__8);
				}
			}
			else
			{
				DebugUtil.LogError(string.Concat("Unable to retreive inline callback instance. Type : ", typeof(T), " id : ", callbackId));
			}
		}

		public static int RegisterInlineCallback<C>(C callback) where C : ICallback
		{
			int result = InlineCallbackRegistry<C>.Add(callback);
			Singleton<CallbackCenter>.Instance.AddCallbackProxy(typeof(C));
			return result;
		}

		public static C GetInlineCallback<C>(int callbackId) where C : ICallback
		{
			return InlineCallbackRegistry<C>.Get(callbackId);
		}

		public void AddCallback(ICallback callback)
		{
			lock (mGlobalCallbacksLock)
			{
				mGlobalCallbacks.Add(callback);
			}
			AddRemoveNativeCallback(callback, true);
		}

		public void RemoveCallback(ICallback callback)
		{
			lock (mGlobalCallbacksLock)
			{
				mGlobalCallbacks.Remove(callback);
			}
			AddRemoveNativeCallback(callback, false);
		}

		public void RemoveCallbacks(Type callbackType)
		{
			lock (mGlobalCallbacksLock)
			{
				IList<ICallback> callbacks = GetCallbacks();
				foreach (ICallback item in callbacks)
				{
					if (callbackType.IsAssignableFrom(item.GetType()))
					{
						RemoveCallback(item);
					}
				}
			}
		}

		public static IList<Type> GetCallbackInterfaces(ICallback callback)
		{
			List<Type> list = new List<Type>();
			Type[] interfaces = callback.GetType().GetInterfaces();
			Type[] array = interfaces;
			foreach (Type type in array)
			{
				if (typeof(ICallback).IsAssignableFrom(type) && type != typeof(ICallback))
				{
					list.Add(type);
				}
			}
			return list;
		}

		private void AddRemoveNativeCallback(ICallback callback, bool add)
		{
			IList<Type> callbackInterfaces = GetCallbackInterfaces(callback);
			foreach (Type item in callbackInterfaces)
			{
				ICallbackProxy callbackProxy = GetCallbackProxy(item);
				DebugUtil.Log("{0} retain count of native callback proxy {1}", (!add) ? "Decrement" : "Increment", callbackProxy.NativeClassName);
				if (add)
				{
					JniHelper.CallStatic("muneris.bridgehelper.CallbackManager", "retainCallback", callbackProxy.NativeClassName);
				}
				else
				{
					JniHelper.CallStatic("muneris.bridgehelper.CallbackManager", "releaseCallback", callbackProxy.NativeClassName);
				}
			}
		}

		private IList<ICallback> GetCallbacks()
		{
			//Discarded unreachable code: IL_001e
			lock (mGlobalCallbacksLock)
			{
				return new List<ICallback>(mGlobalCallbacks);
			}
		}

		public ICallbackProxy GetCallbackProxy(Type type)
		{
			//Discarded unreachable code: IL_0079
			lock (mCallbackProxiesLock)
			{
				if (mCallbackProxies.ContainsKey(type))
				{
					return mCallbackProxies[type];
				}
				Type callbackProxyType = TypeRegistry.GetCallbackProxyType(type);
				ICallbackProxy callbackProxy = (ICallbackProxy)Activator.CreateInstance(callbackProxyType);
				mCallbackProxies[type] = callbackProxy;
				JniHelper.CallStatic("muneris.bridgehelper.CallbackManager", "addUnityProxy", callbackProxy.NativeClassName, callbackProxy);
				return callbackProxy;
			}
		}

		private void AddCallbackProxy(Type type)
		{
			GetCallbackProxy(type);
		}

		public static void Test()
		{
			DebugUtil.Log("callbacks size " + Singleton<CallbackCenter>.Instance.mGlobalCallbacks.Count);
		}
	}
}
