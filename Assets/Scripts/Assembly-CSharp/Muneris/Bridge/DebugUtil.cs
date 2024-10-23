using System;
using UnityEngine;

namespace Muneris.Bridge
{
	public static class DebugUtil
	{
		private static readonly string BRIDGE_TAG = "MUNERIS_UNITY_BRIDGE";

		public static void Assert(bool condition, string msg = "")
		{
			if (!condition)
			{
				throw new Exception("Assertion failure: " + msg);
			}
		}

		public static void Log(string msg, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				Debug.LogFormat(BRIDGE_TAG + ": " + msg, args);
			}
			else
			{
				Debug.Log(BRIDGE_TAG + ": " + msg);
			}
		}

		public static void LogWarning(string msg, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				Debug.LogWarningFormat(BRIDGE_TAG + ": " + msg, args);
			}
			else
			{
				Debug.LogWarning(BRIDGE_TAG + ": " + msg);
			}
		}

		public static void LogError(string msg, params object[] args)
		{
			if (args != null && args.Length > 0)
			{
				Debug.LogErrorFormat(BRIDGE_TAG + ": " + msg, args);
			}
			else
			{
				Debug.LogError(BRIDGE_TAG + ": " + msg);
			}
		}

		public static void LogException(Exception exception)
		{
			Debug.LogException(exception);
		}

		public static T DummyCall<T>(string methodName, params object[] args)
		{
			string text = string.Empty;
			if (args != null)
			{
				for (int i = 0; i < args.Length; i++)
				{
					text = string.Concat(text, args[i], ", ");
				}
			}
			Log("DummyBridgeCall: " + methodName + "(" + text + ")");
			return default(T);
		}

		public static void DummyCall(string methodName, params object[] args)
		{
			DummyCall<object>(methodName, args);
		}
	}
}
