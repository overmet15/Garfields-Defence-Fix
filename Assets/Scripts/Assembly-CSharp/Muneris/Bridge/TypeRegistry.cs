using System;
using System.Collections.Generic;
using System.Reflection;

namespace Muneris.Bridge
{
	public static class TypeRegistry
	{
		private static IDictionary<string, Type> sNativeClassToType;

		private static IDictionary<Type, string> sTypeToNativeClass;

		private static IDictionary<Type, Type> sCallbackTypeToProxyType;

		static TypeRegistry()
		{
			sNativeClassToType = new Dictionary<string, Type>();
			sTypeToNativeClass = new Dictionary<Type, string>();
			sCallbackTypeToProxyType = new Dictionary<Type, Type>();
			try
			{
				DebugUtil.Log("Initialializing type Muneris.Bridge.TypeRegistry");
				List<Type> subTypes = GetSubTypes(typeof(BridgeObject));
				subTypes.AddRange(GetSubTypes(typeof(BridgeException)));
				foreach (Type item in subTypes)
				{
					BridgeObjectInfoAttribute atrribute = GetAtrribute<BridgeObjectInfoAttribute>(item);
					if (atrribute != null)
					{
						Register(item, atrribute.NativeClass);
					}
					else
					{
						DebugUtil.LogError("Missing BridgeObjectInfo Attribute for type : " + item);
					}
				}
				List<Type> subTypes2 = GetSubTypes(typeof(ICallbackProxy));
				foreach (Type item2 in subTypes2)
				{
					if (!item2.IsAbstract)
					{
						Type callbackType = item2.BaseType.GetGenericArguments()[0];
						RegisterCallbackProxyType(item2, callbackType);
					}
				}
				List<string> value = new List<string>(sNativeClassToType.Keys);
				string text = JsonHelper.Serialize(value);
				DebugUtil.Log("Initialializing native MunerisBridge");
				JniHelper.CallStatic("muneris.bridgehelper.Bridge", "registerWrapperClasses", text);
			}
			catch (Exception exception)
			{
				DebugUtil.LogException(exception);
			}
		}

		public static Type GetType(string nativeClassName)
		{
			if (sNativeClassToType.ContainsKey(nativeClassName))
			{
				return sNativeClassToType[nativeClassName];
			}
			return null;
		}

		public static string GetNativeClassName(Type type)
		{
			if (sTypeToNativeClass.ContainsKey(type))
			{
				return sTypeToNativeClass[type];
			}
			return null;
		}

		public static void RegisterCallbackProxyType(Type proxyType, Type callbackType)
		{
			sCallbackTypeToProxyType.Add(callbackType, proxyType);
		}

		public static Type GetCallbackProxyType(Type callbackType)
		{
			if (sCallbackTypeToProxyType.ContainsKey(callbackType))
			{
				return sCallbackTypeToProxyType[callbackType];
			}
			return null;
		}

		private static A GetAtrribute<A>(Type type) where A : Attribute
		{
			object[] customAttributes = type.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				if (obj is A)
				{
					return obj as A;
				}
			}
			return (A)null;
		}

		private static void Register(Type type, string nativeClassName)
		{
			sNativeClassToType[nativeClassName] = type;
			sTypeToNativeClass[type] = nativeClassName;
		}

		private static List<Type> GetSubTypes(Type baseType)
		{
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (IsSystemAssembly(assembly))
				{
					continue;
				}
				Type[] types = assembly.GetTypes();
				foreach (Type type in types)
				{
					if (baseType != type && baseType.IsAssignableFrom(type) && type != null && type.Namespace != null)
					{
						list.Add(type);
					}
				}
			}
			return list;
		}

		private static bool IsSystemAssembly(Assembly assembly)
		{
			return assembly.FullName.StartsWith("Mono.Cecil") || assembly.FullName.StartsWith("UnityScript") || assembly.FullName.StartsWith("Boo.Lan") || assembly.FullName.StartsWith("System") || assembly.FullName.StartsWith("I18N") || assembly.FullName.StartsWith("UnityEngine") || assembly.FullName.StartsWith("UnityEditor") || assembly.FullName.StartsWith("mscorlib");
		}
	}
}
