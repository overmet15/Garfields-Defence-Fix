using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverEvent")]
	public class TakeoverEvent : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.takeover.TakeoverEventBridge";

		protected TakeoverEvent(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getEvent()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getEvent___String", new object[1] { GetObjectId() });
		}

		public void setEvent(string @event)
		{
			JniHelper.CallStatic(_bridgeClassName, "setEvent___void_String", GetObjectId(), @event);
		}

		public JsonObject getCargo()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getCargo___JSONObject", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<JsonObject>(json);
		}

		public void setCargo(JsonObject cargo)
		{
			string text = JsonHelper.Serialize(cargo);
			JniHelper.CallStatic(_bridgeClassName, "setCargo___void_JSONObject", GetObjectId(), text);
		}

		public bool isFirst()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isFirst___boolean", new object[1] { GetObjectId() });
		}

		public void setFirst(bool first)
		{
			bool flag = first;
			JniHelper.CallStatic(_bridgeClassName, "setFirst___void_boolean", GetObjectId(), flag);
		}

		public bool isLast()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isLast___boolean", new object[1] { GetObjectId() });
		}

		public void setLast(bool last)
		{
			bool flag = last;
			JniHelper.CallStatic(_bridgeClassName, "setLast___void_boolean", GetObjectId(), flag);
		}

		public List<MunerisException> getExceptions()
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "getExceptions___ArrayList", new object[1] { GetObjectId() });
			return JsonHelper.Deserialize<List<MunerisException>>(json);
		}

		public void setExceptions(List<MunerisException> exceptions)
		{
			string text = JsonHelper.Serialize(exceptions);
			JniHelper.CallStatic(_bridgeClassName, "setExceptions___void_ArrayList", GetObjectId(), text);
		}

		public void addException(MunerisException e)
		{
			string text = JsonHelper.Serialize(e);
			JniHelper.CallStatic(_bridgeClassName, "addException___void_MunerisException", GetObjectId(), text);
		}

		public void useRootViewOrActivity()
		{
			JniHelper.CallStatic(_bridgeClassName, "useRootViewOrActivity___void", GetObjectId());
		}

		public void showSpinner(bool spinnerEnabled)
		{
			bool flag = spinnerEnabled;
			JniHelper.CallStatic(_bridgeClassName, "showSpinner___void_boolean", GetObjectId(), flag);
		}

		public bool getSpinnerEnabled()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "getSpinnerEnabled___Boolean", new object[1] { GetObjectId() });
		}

		public bool isCompleted()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isCompleted___boolean", new object[1] { GetObjectId() });
		}

		public void setCompleted(bool isCompleted)
		{
			bool flag = isCompleted;
			JniHelper.CallStatic(_bridgeClassName, "setCompleted___void_boolean", GetObjectId(), flag);
		}
	}
}
