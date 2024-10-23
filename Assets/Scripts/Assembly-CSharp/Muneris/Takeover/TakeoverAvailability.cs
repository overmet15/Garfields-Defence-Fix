using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverAvailability")]
	public class TakeoverAvailability : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.takeover.TakeoverAvailabilityBridge";

		protected TakeoverAvailability(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getEvent()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getEvent___String", new object[1] { GetObjectId() });
		}

		public int getAvailableCount()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getAvailableCount___int", new object[1] { GetObjectId() });
		}

		public bool isPrecise()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isPrecise___boolean", new object[1] { GetObjectId() });
		}
	}
}
