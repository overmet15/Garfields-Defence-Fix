using System;
using System.Reflection;

namespace Muneris.Bridge
{
	public static class BridgeFactory
	{
		public static IBridgeObject Create(string nativeClassName, Type declaredType, ObjectId objectId)
		{
			Type type = TypeRegistry.GetType(nativeClassName);
			if (type == null && declaredType.IsInterface)
			{
				type = Type.GetType(declaredType.AssemblyQualifiedName + "Proxy");
			}
			if (type != null)
			{
				return (IBridgeObject)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[1] { objectId }, null);
			}
			DebugUtil.LogError("Failed to find C# class for native class: " + nativeClassName);
			return null;
		}

		public static BridgeException CreateException(string nativeClassName, string msg)
		{
			Type type = TypeRegistry.GetType(nativeClassName);
			if (type != null)
			{
				return (BridgeException)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public, null, new object[1] { msg }, null);
			}
			return new MunerisException(msg);
		}
	}
}
