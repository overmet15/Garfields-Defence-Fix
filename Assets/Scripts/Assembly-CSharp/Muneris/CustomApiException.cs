using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.CustomApiException")]
	public class CustomApiException : MunerisException
	{
		private static string _bridgeClassName = "muneris.bridge.CustomApiExceptionBridge";

		public CustomApiException(string msg)
			: base(msg)
		{
		}

		protected CustomApiException(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getType()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getType___String", new object[1] { GetObjectId() });
		}

		public void setType(string type)
		{
			JniHelper.CallStatic(_bridgeClassName, "setType___void_String", GetObjectId(), type);
		}

		public string getSubtype()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getSubtype___String", new object[1] { GetObjectId() });
		}

		public void setSubtype(string subtype)
		{
			JniHelper.CallStatic(_bridgeClassName, "setSubtype___void_String", GetObjectId(), subtype);
		}
	}
}
