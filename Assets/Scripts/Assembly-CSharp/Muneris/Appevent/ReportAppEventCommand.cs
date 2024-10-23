using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Appevent
{
	[BridgeObjectInfo(NativeClass = "muneris.android.appevent.ReportAppEventCommand")]
	public class ReportAppEventCommand : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.appevent.ReportAppEventCommandBridge";

		protected ReportAppEventCommand(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getEventName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getEventName___String", new object[1] { GetObjectId() });
		}

		public ReportAppEventCommand addParameter(string name, string value)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "addParameter___ReportAppEventCommand_String_String", new object[3]
			{
				GetObjectId(),
				name,
				value
			});
			return JsonHelper.Deserialize<ReportAppEventCommand>(json);
		}

		public ReportAppEventCommand setParameters(Dictionary<string, string> parameters)
		{
			string text = JsonHelper.Serialize(parameters);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setParameters___ReportAppEventCommand_HashMap", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<ReportAppEventCommand>(json);
		}

		public Dictionary<string, string> getParameters()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getParameters___Map", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<Dictionary<string, string>>(json);
		}

		public ReportAppEventCommand setCallback(IReportAppEventCallback callback)
		{
			int num = CallbackCenter.RegisterInlineCallback(callback);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallback___ReportAppEventCommand_ReportAppEventCallback", new object[2]
			{
				GetObjectId(),
				num
			});
			return JsonHelper.Deserialize<ReportAppEventCommand>(json);
		}

		public IReportAppEventCallback getCallback()
		{
			int callbackId = JniHelper.CallStatic<int>(_bridgeClassName, "getCallback___ReportAppEventCallback", new object[1] { GetObjectId() });
			return CallbackCenter.GetInlineCallback<IReportAppEventCallback>(callbackId);
		}

		public void execute()
		{
			JniHelper.CallStatic(_bridgeClassName, "execute___Void", GetObjectId());
		}

		public ReportAppEventCommand setCallbackContext(CallbackContext callbackContext)
		{
			string text = JsonHelper.Serialize(callbackContext);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "setCallbackContext___ReportAppEventCommand_CallbackContext", new object[2]
			{
				GetObjectId(),
				text
			});
			return JsonHelper.Deserialize<ReportAppEventCommand>(json);
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
