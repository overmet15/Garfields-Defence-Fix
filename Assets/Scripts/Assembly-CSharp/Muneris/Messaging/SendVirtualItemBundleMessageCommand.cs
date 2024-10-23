using System;
using Muneris.Bridge;
using Muneris.Virtualitem;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SendVirtualItemBundleMessageCommand")]
	public class SendVirtualItemBundleMessageCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.messaging.SendVirtualItemBundleMessageCommandBridge";

		protected SendVirtualItemBundleMessageCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public VirtualItemBundle getVirtualItemBundle()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemBundle___VirtualItemBundle", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundle>(json);
		}

		public SendVirtualItemBundleMessageCommand setText(string text)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setText___SendVirtualItemBundleMessageCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}

		public SendVirtualItemBundleMessageCommand setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCargo___SendVirtualItemBundleMessageCommand_JSONObject", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public SendVirtualItemBundleMessageCommand setExpiry(DateTime expiry)
		{
			long num = SerializationHelper.Serialize(expiry);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendVirtualItemBundleMessageCommand_Date", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
		}

		public SendVirtualItemBundleMessageCommand setExpiry(long expiry)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setExpiry___SendVirtualItemBundleMessageCommand_long", new object[2]
			{
				GetObjectId(),
				expiry
			});
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
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

		public VirtualItemBundleMessage execute()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "execute___VirtualItemBundleMessage", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualItemBundleMessage>(json);
		}

		public SendVirtualItemBundleMessageCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___SendVirtualItemBundleMessageCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<SendVirtualItemBundleMessageCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
