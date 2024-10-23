using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.Message")]
	public abstract class Message : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.MessageBridge";

		protected Message(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getMessageId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getMessageId___String", new object[1] { GetObjectId() });
		}

		public ISourceAddress getSource()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSource___SourceAddress", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ISourceAddress>(json);
		}

		public ITargetAddress getTarget()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getTarget___TargetAddress", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<ITargetAddress>(json);
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
