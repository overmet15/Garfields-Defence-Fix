using System;
using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AppLaunchInfo")]
	public class AppLaunchInfo : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.AppLaunchInfoBridge";

		protected AppLaunchInfo(ObjectId objectId)
			: base(objectId)
		{
		}

		public DateTime getPreviousLaunchDate()
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "getPreviousLaunchDate___Date", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<DateTime>(num);
		}

		public bool isPreviousSessionCrashed()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isPreviousSessionCrashed___boolean", new object[1] { GetObjectId() });
		}

		public long getTotalLaunches()
		{
			return JniHelper.CallStatic<long>(_bridgeClassName, "getTotalLaunches___long", new object[1] { GetObjectId() });
		}
	}
}
