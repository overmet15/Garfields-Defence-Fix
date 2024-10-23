using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.VirtualGoodAnnotation")]
	public class VirtualGoodAnnotation : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.virtualgood.VirtualGoodAnnotationBridge";

		protected VirtualGoodAnnotation(ObjectId objectId)
			: base(objectId)
		{
		}

		public string getName()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getName___String", new object[1] { GetObjectId() });
		}

		public string getText()
		{
			return JniHelper.CallStatic<string>(_bridgeClassName, "getText___String", new object[1] { GetObjectId() });
		}
	}
}
