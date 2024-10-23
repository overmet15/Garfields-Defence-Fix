using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.Takeovers")]
	public class Takeovers : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.takeover.TakeoversBridge";

		protected Takeovers(ObjectId objectId)
			: base(objectId)
		{
		}

		public static TakeoverAvailability checkAvailability(string @event)
		{
			string json = JniHelper.CallStatic<string>(_bridgeClassName, "checkAvailability___TakeoverAvailability_String", new object[1] { @event });
			return JsonHelper.Deserialize<TakeoverAvailability>(json);
		}
	}
}
