using UnityEngine;

namespace Muneris.Bridge
{
	public abstract class CallbackProxy<T> : AndroidJavaProxy, ICallbackProxy where T : ICallback
	{
		public string NativeClassName { get; private set; }

		protected CallbackProxy(string className)
			: base(className + "$UnityProxy")
		{
			NativeClassName = className;
		}
	}
}
