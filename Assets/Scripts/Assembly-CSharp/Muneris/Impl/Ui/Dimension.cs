using Muneris.Bridge;

namespace Muneris.Impl.Ui
{
	[BridgeObjectInfo(NativeClass = "muneris.android.impl.ui.Dimension")]
	public class Dimension : BridgeObject
	{
		private static string _bridgeClassName = "muneris.bridge.impl.ui.DimensionBridge";

		public Dimension(int width, int height)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "Dimension____int_int", new object[2] { width, height });
			Init(num);
		}

		public Dimension(string sizeDescription)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "Dimension____String", new object[1] { sizeDescription });
			Init(num);
		}

		public Dimension(string percentage, int parentWidth, int parentHeight)
			: base(0L)
		{
			long num = JniHelper.CallStatic<long>(_bridgeClassName, "Dimension____String_int_int", new object[3] { percentage, parentWidth, parentHeight });
			Init(num);
		}

		protected Dimension(ObjectId objectId)
			: base(objectId)
		{
		}

		public int getWidth()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getWidth___int", new object[1] { GetObjectId() });
		}

		public int getHeight()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getHeight___int", new object[1] { GetObjectId() });
		}

		public int getArea()
		{
			return JniHelper.CallStatic<int>(_bridgeClassName, "getArea___int", new object[1] { GetObjectId() });
		}

		public double getAspectRatio()
		{
			return JniHelper.CallStatic<double>(_bridgeClassName, "getAspectRatio___double", new object[1] { GetObjectId() });
		}
	}
}
