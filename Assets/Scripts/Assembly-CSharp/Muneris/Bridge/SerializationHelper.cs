using System;
using System.Collections;
using System.Collections.Generic;

namespace Muneris.Bridge
{
	public static class SerializationHelper
	{
		private abstract class Serializer<T, S>
		{
			public abstract T Deserialize(S value, Type typeOfT);

			public abstract S Serialize(T value);
		}

		private class DateSerializer : Serializer<DateTime, long>
		{
			public override DateTime Deserialize(long date, Type typeOfT)
			{
				return new DateTime(date);
			}

			public override long Serialize(DateTime date)
			{
				return date.Ticks;
			}
		}

		private class UriSerializer : Serializer<Uri, string>
		{
			public override Uri Deserialize(string uri, Type typeOfT)
			{
				if (string.IsNullOrEmpty(uri))
				{
					return null;
				}
				return new Uri(uri);
			}

			public override string Serialize(Uri uri)
			{
				return uri.ToString();
			}
		}

		private class BridgeObjectSerializer : Serializer<object, Dictionary<string, object>>
		{
			public override object Deserialize(Dictionary<string, object> dict, Type typeOfT)
			{
				long num = (long)dict["id"];
				string nativeClassName = dict["class"] as string;
				return BridgeFactory.Create(nativeClassName, typeOfT, num);
			}

			public override Dictionary<string, object> Serialize(object bridgeObject)
			{
				IBridgeObject bridgeObject2 = bridgeObject as IBridgeObject;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["id"] = bridgeObject2.GetObjectId();
				dictionary["class"] = bridgeObject2.GetNativeClassName();
				return dictionary;
			}
		}

		private class ListSerializer : Serializer<IList, List<object>>
		{
			public override IList Deserialize(List<object> jsonArray, Type type)
			{
				Type[] genericTypeArgs = GetGenericTypeArgs(type);
				Type type2 = genericTypeArgs[0];
				IList list = (IList)Activator.CreateInstance(type);
				foreach (object item in jsonArray)
				{
					object value = DeserializeObject(item, type2);
					list.Add(value);
				}
				return list;
			}

			public override List<object> Serialize(IList list)
			{
				List<object> list2 = new List<object>();
				foreach (object item2 in list)
				{
					object item = SerializeObject(item2);
					list2.Add(item);
				}
				return list2;
			}
		}

		private class DictionarySerializer : Serializer<IDictionary, List<object>>
		{
			public override IDictionary Deserialize(List<object> jsonArray, Type type)
			{
				Type[] genericTypeArgs = GetGenericTypeArgs(type);
				Type type2 = genericTypeArgs[0];
				Type type3 = genericTypeArgs[1];
				IDictionary dictionary = (IDictionary)Activator.CreateInstance(type);
				foreach (object item in jsonArray)
				{
					Dictionary<string, object> dictionary2 = item as Dictionary<string, object>;
					object key = DeserializeObject(dictionary2["key"], type2);
					object value = DeserializeObject(dictionary2["value"], type3);
					dictionary[key] = value;
				}
				return dictionary;
			}

			public override List<object> Serialize(IDictionary dict)
			{
				List<object> list = new List<object>();
				foreach (DictionaryEntry item in dict)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary["key"] = SerializeObject(item.Key);
					dictionary["value"] = SerializeObject(item.Value);
					list.Add(dictionary);
				}
				return list;
			}
		}

		private class JsonArraySerializer : Serializer<JsonArray, List<object>>
		{
			public override JsonArray Deserialize(List<object> jsonArray, Type type)
			{
				return new JsonArray(jsonArray);
			}

			public override List<object> Serialize(JsonArray jsonArray)
			{
				return new List<object>(jsonArray);
			}
		}

		private class JsonObjectSerializer : Serializer<JsonObject, Dictionary<string, object>>
		{
			public override JsonObject Deserialize(Dictionary<string, object> jsonObject, Type type)
			{
				return new JsonObject(jsonObject);
			}

			public override Dictionary<string, object> Serialize(JsonObject jsonObject)
			{
				return new Dictionary<string, object>(jsonObject);
			}
		}

		private static DateSerializer sDateSerializer = new DateSerializer();

		private static UriSerializer sUriSerializer = new UriSerializer();

		private static BridgeObjectSerializer sBridgeObjectSerializer = new BridgeObjectSerializer();

		private static ListSerializer sListSerializer = new ListSerializer();

		private static DictionarySerializer sDictionarySerializer = new DictionarySerializer();

		private static JsonArraySerializer sJsonArraySerializer = new JsonArraySerializer();

		private static JsonObjectSerializer sJsonObjectSerializer = new JsonObjectSerializer();

		public static T Deserialize<T>(object value)
		{
			return (T)DeserializeObject(value, typeof(T));
		}

		private static object DeserializeObject(object value, Type type)
		{
			if (value == null)
			{
				return null;
			}
			if (typeof(JsonArray).IsAssignableFrom(type))
			{
				return sJsonArraySerializer.Deserialize((List<object>)value, type);
			}
			if (typeof(JsonObject).IsAssignableFrom(type))
			{
				return sJsonObjectSerializer.Deserialize((Dictionary<string, object>)value, type);
			}
			if (typeof(IList).IsAssignableFrom(type))
			{
				return sListSerializer.Deserialize((List<object>)value, type);
			}
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				return sDictionarySerializer.Deserialize((List<object>)value, type);
			}
			if (typeof(Uri).IsAssignableFrom(type))
			{
				return sUriSerializer.Deserialize((string)value, type);
			}
			if (typeof(DateTime).IsAssignableFrom(type))
			{
				return sDateSerializer.Deserialize((long)value, type);
			}
			if (typeof(IBridgeObject).IsAssignableFrom(type) || type.IsInterface)
			{
				return sBridgeObjectSerializer.Deserialize((Dictionary<string, object>)value, type);
			}
			if (type.IsEnum)
			{
				return Enum.Parse(type, value.ToString());
			}
			if (isNumericType(type))
			{
				return Convert.ChangeType(value, type);
			}
			return value;
		}

		public static object Serialize(object value)
		{
			return SerializeObject(value);
		}

		public static long Serialize(DateTime date)
		{
			return sDateSerializer.Serialize(date);
		}

		public static string Serialize(Uri uri)
		{
			return sUriSerializer.Serialize(uri);
		}

		public static int Serialize<E>(E enumValue) where E : struct, IComparable, IFormattable, IConvertible
		{
			if (typeof(E).IsEnum)
			{
				return (int)(object)enumValue;
			}
			throw new Exception("Serialization error. Was expecting an enum type");
		}

		private static object SerializeObject(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (typeof(JsonArray).IsAssignableFrom(type))
			{
				return sJsonArraySerializer.Serialize(value as JsonArray);
			}
			if (typeof(JsonObject).IsAssignableFrom(type))
			{
				return sJsonObjectSerializer.Serialize(value as JsonObject);
			}
			if (typeof(IList).IsAssignableFrom(type))
			{
				return sListSerializer.Serialize(value as IList);
			}
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				return sDictionarySerializer.Serialize(value as IDictionary);
			}
			if (typeof(Uri).IsAssignableFrom(type))
			{
				return sUriSerializer.Serialize(value as Uri);
			}
			if (typeof(DateTime).IsAssignableFrom(type))
			{
				return sDateSerializer.Serialize((DateTime)value);
			}
			if (typeof(BridgeObject).IsAssignableFrom(type))
			{
				return sBridgeObjectSerializer.Serialize(value as BridgeObject);
			}
			if (type.IsEnum)
			{
				return (int)value;
			}
			return value;
		}

		private static bool isNumericType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return true;
			default:
				return false;
			}
		}

		private static Type[] GetGenericTypeArgs(Type type)
		{
			if (type.IsGenericType)
			{
				return type.GetGenericArguments();
			}
			if (type.BaseType.IsGenericType)
			{
				return type.BaseType.GetGenericArguments();
			}
			throw new Exception("Error failed to get generic type args for type: " + type);
		}
	}
}
