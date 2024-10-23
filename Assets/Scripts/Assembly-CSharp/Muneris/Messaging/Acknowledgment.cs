using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Acknowledgment")]
	public abstract class Acknowledgment : BridgeObject
	{
		[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Acknowledgment.AcknowledgmentType")]
		public static class AcknowledgmentType
		{
			public const string Read = "read";

			public const string Accept = "accept";

			public const string Decline = "decline";
		}

		private static string _bridgeClassName = "muneris.bridge.messaging.AcknowledgmentBridge";

		protected Acknowledgment(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getAcknowledgmentId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgmentId___String", new object[1] { GetObjectId() });
		}

		public string getAcknowledgmentType()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getAcknowledgmentType___String", new object[1] { GetObjectId() });
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public DateTime getCreationDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getCreationDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public DateTime getExpiryDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getExpiryDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public bool isInOutbox()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInOutbox___boolean", new object[1] { GetObjectId() });
		}

		public string getType()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getType___String", new object[1] { GetObjectId() });
		}
	}
}
