using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.PrivacyPreferences")]
	public class PrivacyPreferences : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.PrivacyPreferencesBridge";

		protected PrivacyPreferences(ObjectId objectId)
			: base(objectId)
		{
		}

		public static bool isLimitDataUsage()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isLimitDataUsage___boolean", new object[0]);
		}

		public static void setLimitDataUsage(bool limitDataUsage)
		{
			bool flag = limitDataUsage;
			JniHelper.CallStatic(_bridgeClassName, "setLimitDataUsage___void_boolean", flag);
		}

		public static bool isOptOutTargetedAds()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isOptOutTargetedAds___boolean", new object[0]);
		}

		public static void setOptOutTargetedAds(bool optOutTargetedAds)
		{
			bool flag = optOutTargetedAds;
			JniHelper.CallStatic(_bridgeClassName, "setOptOutTargetedAds___void_boolean", flag);
		}

		public static bool isOptOutLocationTracking()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isOptOutLocationTracking___boolean", new object[0]);
		}

		public static void setOptOutLocationTracking(bool optOutLocationTracking)
		{
			bool flag = optOutLocationTracking;
			JniHelper.CallStatic(_bridgeClassName, "setOptOutLocationTracking___void_boolean", flag);
		}

		public static bool isOptOutPersonalIdentification()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isOptOutPersonalIdentification___boolean", new object[0]);
		}

		public static void setOptOutPersonalIdentification(bool optOutPersonalIdentification)
		{
			bool flag = optOutPersonalIdentification;
			JniHelper.CallStatic(_bridgeClassName, "setOptOutPersonalIdentification___void_boolean", flag);
		}
	}
}
