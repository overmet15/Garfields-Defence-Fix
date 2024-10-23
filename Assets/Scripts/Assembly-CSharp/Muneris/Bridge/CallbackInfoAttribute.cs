using System;

namespace Muneris.Bridge
{
	[AttributeUsage(AttributeTargets.Interface)]
	public class CallbackInfoAttribute : Attribute
	{
		public Type ProxyType { get; set; }
	}
}
