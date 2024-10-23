using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.FindVirtualItemsCommand")]
	public class FindVirtualItemsCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.FindVirtualItemsCommandBridge";

		protected FindVirtualItemsCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindVirtualItemsCommand setVirtualItemIds(List<string> virtualItemIds)
		{
			string text = JsonHelper.Serialize(virtualItemIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setVirtualItemIds___FindVirtualItemsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public FindVirtualItemsCommand addVirtualItemIds(List<string> virtualItemIds)
		{
			string text = JsonHelper.Serialize(virtualItemIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addVirtualItemIds___FindVirtualItemsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public List<string> getVirtualItemIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindVirtualItemsCommand setVirtualItemTypes(List<VirtualItemType> virtualItemTypes)
		{
			string text = JsonHelper.Serialize(virtualItemTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setVirtualItemTypes___FindVirtualItemsCommand_VirtualItemType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public FindVirtualItemsCommand addVirtualItemTypes(List<VirtualItemType> virtualItemTypes)
		{
			string text = JsonHelper.Serialize(virtualItemTypes);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addVirtualItemTypes___FindVirtualItemsCommand_VirtualItemType", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public List<VirtualItemType> getVirtualItemTypes()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualItemTypes___VirtualItemType", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<VirtualItemType>>(json);
		}

		public FindVirtualItemsCommand setCallback(IFindVirtualItemsCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindVirtualItemsCommand_FindVirtualItemsCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public IFindVirtualItemsCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindVirtualItemsCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindVirtualItemsCallback>(callbackId);
		}

		public FindVirtualItemsCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___FindVirtualItemsCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public FindVirtualItemsCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindVirtualItemsCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualItemsCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
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
	}
}
