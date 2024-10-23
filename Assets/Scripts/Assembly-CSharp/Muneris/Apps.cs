using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.Apps")]
	public class Apps : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.AppsBridge";

		protected Apps(ObjectId objectId)
			: base(objectId)
		{
		}

		public static FindAppsCommand find(List<string> appIds)
		{
			string text = JsonHelper.Serialize(appIds);
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "find___FindAppsCommand_String", new object[1] { text });
			return JsonHelper.Deserialize<FindAppsCommand>(json);
		}
	}
}
