using System;
using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.App")]
	public class IAppProxy : BridgeObject, IApp
	{
		private static string _bridgeClassName = "muneris.bridge.AppBridge";

		protected IAppProxy(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getAppId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getAppId___String", new object[1] { GetObjectId() });
		}

		public string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[1] { GetObjectId() });
		}

		public string getPackageName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getPackageName___String", new object[1] { GetObjectId() });
		}

		public Uri getAppUrl()
		{
			string value = JniHelper.CallStatic<string>(_bridgeClassName, "getAppUrl___Uri", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Uri>(value);
		}

		public Uri getImageUrl()
		{
			string value = JniHelper.CallStatic<string>(_bridgeClassName, "getImageUrl___Uri", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Uri>(value);
		}

		public string getAppStoreId()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getAppStoreId___String", new object[1] { GetObjectId() });
		}

		public void launch()
		{
			JniHelper.CallStatic(_bridgeClassName, "launch___void", GetObjectId());
		}
	}
}
