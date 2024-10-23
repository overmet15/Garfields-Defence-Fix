using System;
using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendCustomRequestMessageCommand")]
	public class SendCustomRequestMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendCustomRequestMessageCommandBridge";

		protected SendCustomRequestMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendCustomRequestMessageCommand setAutoGenerateResponseMessageForAcknowledgmentTypes(List<string> acknowledgmentTypes)
		{
			string text = JsonHelper.Serialize(acknowledgmentTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setAutoGenerateResponseMessageForAcknowledgmentTypes___SendCustomRequestMessageCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public SendCustomRequestMessageCommand addAutoGenerateResponseMessageForAcknowledgmentType(string acknowledgmentType)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addAutoGenerateResponseMessageForAcknowledgmentType___SendCustomRequestMessageCommand_String", new object[2]
			{
				GetObjectId(),
				acknowledgmentType
			});
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public List<string> getAutoGenerateResponseMessageForAcknowledgmentTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getAutoGenerateResponseMessageForAcknowledgmentTypes___ArrayList", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public SendCustomRequestMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomRequestMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public SendCustomRequestMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomRequestMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public long getExpiry()
		{
			return JniHelper.CallStatic<long>(_bridgeClassName, "getExpiry___long", new object[1] { GetObjectId() });
		}

		public void validate()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "validate___void", new object[1] { GetObjectId() });
			BridgeResult<object> bridgeResult = JsonHelper.DeserializeBridgeResult<object>(json);
			if (bridgeResult.Kind == BridgeResult<object>.Type.Exception)
			{
				throw bridgeResult.Exception;
			}
		}

		public CustomRequestMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___CustomRequestMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomRequestMessage>(json);
		}

		public SendCustomRequestMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendCustomRequestMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomRequestMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
