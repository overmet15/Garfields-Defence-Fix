using System;

namespace Muneris.Bridge
{
	public abstract class Singleton<T> where T : class
	{
		private static T sInstance;

		private static object sLock = new object();

		public static T Instance
		{
			get
			{
				if (sInstance == null)
				{
					lock (sLock)
					{
						if (sInstance == null)
						{
							sInstance = Activator.CreateInstance(typeof(T), true) as T;
						}
					}
				}
				return sInstance;
			}
		}
	}
}
