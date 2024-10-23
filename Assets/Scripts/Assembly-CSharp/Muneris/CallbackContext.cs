using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.CallbackContext")]
	public class CallbackContext : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.CallbackContextBridge";

		public CallbackContext(JsonObject jsonObject)
			: base(0L)
		{
			string text = JsonHelper.Serialize(jsonObject);
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "CallbackContext____JSONObject", new object[1] { text });
			Init(num);
		}

		public CallbackContext()
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "CallbackContext___void", new object[0]);
			Init(num);
		}

		protected CallbackContext(ObjectId objectId)
			: base(objectId)
		{
		}

		public static void bindToCargo(JsonObject cargo, CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(cargo);
			string text2 = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "bindToCargo___void_JSONObject_CallbackContext", new object[2] { text, text2 });
			BridgeResult<object> bridgeResult = JsonHelper.DeserializeBridgeResult<object>(json);
			if (bridgeResult.Kind == BridgeResult<object>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
		}

		public HashSet<KeyValuePair<string, string>> entrySet()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "entrySet___Set", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<HashSet<KeyValuePair<string, string>>>(json);
		}

		public List<string> values()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "values___Collection", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public HashSet<string> keySet()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "keySet___Set", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<HashSet<string>>(json);
		}

		public void clear()
		{
			JniHelper.CallStatic(_bridgeClassName, "clear___void", GetObjectId());
		}

		public string remove(string key)
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "remove___String_String", new object[2]
			{
				GetObjectId(),
				key
			});
		}

		public string put(string key, string value)
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "put___String_String_String", new object[3]
			{
				GetObjectId(),
				key,
				value
			});
		}

		public bool containsValue(string value)
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "containsValue___boolean_String", new object[2]
			{
				GetObjectId(),
				value
			});
		}

		public bool containsKey(string key)
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "containsKey___boolean_String", new object[2]
			{
				GetObjectId(),
				key
			});
		}

		public string get(string key)
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "get___String_String", new object[2]
			{
				GetObjectId(),
				key
			});
		}

		public int size()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "size___int", new object[1] { GetObjectId() });
		}

		public bool isEmpty()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isEmpty___boolean", new object[1] { GetObjectId() });
		}

		public JsonObject toJson()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "toJson___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}
	}
}
