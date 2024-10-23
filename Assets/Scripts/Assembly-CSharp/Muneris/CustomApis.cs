using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.CustomApis")]
	public class CustomApis : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.CustomApisBridge";

		protected CustomApis(ObjectId objectId)
			: base(objectId)
		{
		}

		public static InvokeCustomApiCommand invokeApi(string apiMethod, JsonObject apiParams)
		{
			string text = JsonHelper.Serialize(apiParams);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "invokeApi___InvokeCustomApiCommand_String_JSONObject", new object[2] { apiMethod, text });
			return JsonHelper.Deserialize<InvokeCustomApiCommand>(json);
		}
	}
}
