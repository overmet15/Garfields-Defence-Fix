using System;
using System.Collections.Generic;
using Muneris.MiniJSON;

namespace Muneris.Bridge
{
	public static class JsonHelper
	{
		private static readonly string VALUE_KEY = "value0";

		private static readonly string EXCEPTION_KEY = "exception";

		private static readonly string EXCEPTION_MSG_KEY = "msg";

		public static string Serialize(object value)
		{
			//Discarded unreachable code: IL_0072, IL_00a1
			try
			{
				object obj = SerializationHelper.Serialize(value);
				if (obj != null)
				{
					if (!typeof(JsonObject).IsAssignableFrom(value.GetType()) && !typeof(JsonArray).IsAssignableFrom(value.GetType()))
					{
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						dictionary[VALUE_KEY] = obj;
						return Json.Serialize(dictionary);
					}
					return Json.Serialize(obj);
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Concat("Failed to Serialize object: ", value, " : ", ex));
			}
		}

		public static T Deserialize<T>(string json)
		{
			//Discarded unreachable code: IL_00c0, IL_00ef
			if (json == null || json.Equals(string.Empty))
			{
				return default(T);
			}
			try
			{
				object obj = Json.Deserialize(json);
				if (!typeof(JsonObject).IsAssignableFrom(typeof(T)) && !typeof(JsonArray).IsAssignableFrom(typeof(T)))
				{
					Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
					if (dictionary != null)
					{
						if (dictionary.Count == 0)
						{
							return default(T);
						}
						if (dictionary.ContainsKey(VALUE_KEY))
						{
							return SerializationHelper.Deserialize<T>(dictionary[VALUE_KEY]);
						}
					}
				}
				return SerializationHelper.Deserialize<T>(obj);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to deserialize json: " + json + " : " + ex);
			}
		}

		public static BridgeResult<T> DeserializeBridgeResult<T>(string json)
		{
			//Discarded unreachable code: IL_00e6, IL_0117
			if (json == null || json.Equals(string.Empty))
			{
				return new BridgeResult<T>(default(T), null);
			}
			try
			{
				Dictionary<string, object> dictionary = Json.Deserialize(json) as Dictionary<string, object>;
				if (dictionary.ContainsKey(EXCEPTION_KEY))
				{
					Dictionary<string, object> dictionary2 = dictionary[EXCEPTION_KEY] as Dictionary<string, object>;
					BridgeException exception = ((dictionary2 == null || !dictionary2.ContainsKey(EXCEPTION_MSG_KEY) || !dictionary2.ContainsKey("class")) ? new MunerisException("Failed to deserialize exception over bridge") : BridgeFactory.CreateException(dictionary2["class"] as string, dictionary2[EXCEPTION_MSG_KEY] as string));
					return new BridgeResult<T>(default(T), exception);
				}
				T value = SerializationHelper.Deserialize<T>(dictionary[VALUE_KEY]);
				return new BridgeResult<T>(value, null);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to deserialize json: " + json + " : " + ex);
			}
		}
	}
}
