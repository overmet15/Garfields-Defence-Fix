using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.FindVirtualGoodsCommand")]
	public class FindVirtualGoodsCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.FindVirtualGoodsCommandBridge";

		protected FindVirtualGoodsCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public FindVirtualGoodsCommand setCategories(List<string> categories)
		{
			string text = JsonHelper.Serialize(categories);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCategories___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public FindVirtualGoodsCommand addCategories(List<string> categories)
		{
			string text = JsonHelper.Serialize(categories);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addCategories___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public List<string> getCategories()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCategories___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindVirtualGoodsCommand setVirtualGoodIds(List<string> virtualGoodIds)
		{
			string text = JsonHelper.Serialize(virtualGoodIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setVirtualGoodIds___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public FindVirtualGoodsCommand addVirtualGoodIds(List<string> virtualGoodIds)
		{
			string text = JsonHelper.Serialize(virtualGoodIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addVirtualGoodIds___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public List<string> getVirtualGoodIds()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualGoodIds___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindVirtualGoodsCommand setSkus(List<string> skus)
		{
			string text = JsonHelper.Serialize(skus);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setSkus___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public FindVirtualGoodsCommand addSkus(List<string> skus)
		{
			string text = JsonHelper.Serialize(skus);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addSkus___FindVirtualGoodsCommand_String", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public List<string> getSkus()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getSkus___String", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<string>>(json);
		}

		public FindVirtualGoodsCommand setCallback(IFindVirtualGoodsCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___FindVirtualGoodsCommand_FindVirtualGoodsCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public IFindVirtualGoodsCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___FindVirtualGoodsCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IFindVirtualGoodsCallback>(callbackId);
		}

		public FindVirtualGoodsCommand setInvokeAllCallbacks(bool invokeAllCallbacks)
		{
			bool flag = invokeAllCallbacks;
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setInvokeAllCallbacks___FindVirtualGoodsCommand_boolean", new object[2]
			{
				GetObjectId(),
				flag
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
		}

		public bool isInvokeAllCallbacks()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isInvokeAllCallbacks___boolean", new object[1] { GetObjectId() });
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public FindVirtualGoodsCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___FindVirtualGoodsCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<FindVirtualGoodsCommand>(json);
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
