using System;

namespace Muneris.Bridge
{
	[AttributeUsage(AttributeTargets.Class)]
	public class BridgeObjectInfoAttribute : Attribute
	{
		public string NativeClass { get; set; }
	}
}
