using System;
using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendCustomResponseMessageCommand")]
	public class SendCustomResponseMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendCustomResponseMessageCommandBridge";

		protected SendCustomResponseMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public JsonObject getBody()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getBody___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendCustomResponseMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomResponseMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendCustomResponseMessageCommand>(json);
		}

		public SendCustomResponseMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendCustomResponseMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendCustomResponseMessageCommand>(json);
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

		public CustomResponseMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___CustomResponseMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CustomResponseMessage>(json);
		}

		public SendCustomResponseMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendCustomResponseMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendCustomResponseMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
