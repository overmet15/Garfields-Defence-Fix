using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.PurchaseVirtualGoodCommand")]
	public class PurchaseVirtualGoodCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.PurchaseVirtualGoodCommandBridge";

		protected PurchaseVirtualGoodCommand(ObjectId objectId)
			: base(objectId)
		{
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

		public VirtualGood getVirtualGood()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getVirtualGood___VirtualGood", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<VirtualGood>(json);
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public PurchaseVirtualGoodCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___PurchaseVirtualGoodCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<PurchaseVirtualGoodCommand>(json);
		}

		public CallbackContext getCallbackContext()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCallbackContext___CallbackContext", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<CallbackContext>(json);
		}
	}
}
