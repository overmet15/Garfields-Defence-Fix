using System;
using Muneris.Bridge;

namespace Muneris.Virtualitem.Util
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.util.ImageValue")]
	public class ImageValue : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualitem.util.ImageValueBridge";

		public ImageValue(string rawValue)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "ImageValue____String", new object[1] { rawValue });
			Init(num);
		}

		protected ImageValue(ObjectId objectId)
			: base(objectId)
		{
		}

		public Uri toUri()
		{
			string value = JniHelper.CallStatic<string>(_bridgeClassName, "toUri___Uri", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Uri>(value);
		}
	}
}
