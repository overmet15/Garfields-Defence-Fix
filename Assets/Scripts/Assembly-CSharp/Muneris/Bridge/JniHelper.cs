using System;
using UnityEngine;

namespace Muneris.Bridge
{
	public static class JniHelper
	{
		public static T CallStatic<T>(string className, string methodName, params object[] args)
		{
			//Discarded unreachable code: IL_004d, IL_005f, IL_00b0
			try
			{
				int num = AndroidJNI.AttachCurrentThread();
				if (num != 0)
				{
					DebugUtil.LogError("AndroidJNI.AttachCurrentThread failed: " + num);
					return default(T);
				}
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(className))
				{
					return androidJavaClass.CallStatic<T>(methodName, args);
				}
			}
			catch (Exception ex)
			{
				DebugUtil.LogError("JNI bridge call failed: " + className + "." + methodName + " : ", ex);
				return default(T);
			}
		}

		public static void CallStatic(string className, string methodName, params object[] args)
		{
			try
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(className))
				{
					androidJavaClass.CallStatic(methodName, args);
				}
			}
			catch (Exception ex)
			{
				DebugUtil.LogError("JNI bridge call failed: " + className + "." + methodName + " : ", ex);
			}
		}
	}
}
