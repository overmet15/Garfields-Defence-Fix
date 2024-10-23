using System;
using System.Collections.Generic;
using Muneris;
using Muneris.Bridge;

public static class Callbacks
{
	public static void add(params ICallback[] callbacks)
	{
		foreach (ICallback callback in callbacks)
		{
			Singleton<CallbackCenter>.Instance.AddCallback(callback);
		}
	}

	public static void remove(params ICallback[] callbacks)
	{
		foreach (ICallback callback in callbacks)
		{
			Singleton<CallbackCenter>.Instance.RemoveCallback(callback);
		}
	}

	public static void set(params ICallback[] callbacks)
	{
		foreach (ICallback callback in callbacks)
		{
			IList<Type> callbackInterfaces = CallbackCenter.GetCallbackInterfaces(callback);
			foreach (Type item in callbackInterfaces)
			{
				remove(item);
			}
		}
		add(callbacks);
	}

	public static void remove(params Type[] callbackTypes)
	{
		foreach (Type callbackType in callbackTypes)
		{
			Singleton<CallbackCenter>.Instance.RemoveCallbacks(callbackType);
		}
	}
}
