using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.MunerisClient")]
	public class MunerisClient : BridgeObject
	{
		public const string SDK_VERSION = "5.7.2";

		private static string _bridgeClassName = "muneris.bridge.MunerisClientBridge";

		protected MunerisClient(ObjectId objectId)
			: base(objectId)
		{
		}

		public static bool isReady()
		{
			return JniHelper.CallStatic<bool>(_bridgeClassName, "isReady___boolean", new object[0]);
		}

		public static AgeRating getAgeRating()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getAgeRating___AgeRating", new object[0]);
			return SerializationHelper.Deserialize<AgeRating>(num);
		}

		public static void setAgeRating(AgeRating ageRating)
		{
			int num = SerializationHelper.Serialize(ageRating);
			JniHelper.CallStatic(_bridgeClassName, "setAgeRating___void_AgeRating", num);
		}
	}
}
