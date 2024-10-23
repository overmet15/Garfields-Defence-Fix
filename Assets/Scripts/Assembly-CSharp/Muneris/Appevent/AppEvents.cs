using Muneris.Bridge;

namespace Muneris.Appevent
{
	[BridgeObjectInfo(NativeClass = "muneris.android.appevent.AppEvents")]
	public class AppEvents : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.appevent.AppEventsBridge";

		protected AppEvents(ObjectId objectId)
			: base(objectId)
		{
		}

		public static ReportAppEventCommand report(string name)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "report___ReportAppEventCommand_String", new object[1] { name });
			return JsonHelper.Deserialize<ReportAppEventCommand>(json);
		}
	}
}
