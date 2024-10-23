using Muneris.Bridge;

namespace Muneris.Appevent
{
	[BridgeObjectInfo(NativeClass = "muneris.android.appevent.AppEventCircularReferenceException")]
	public class AppEventCircularReferenceException : MunerisException
	{
		public const string CIRCULAR_REFERENCE = "Circular reference detected: %s <-> %s";

		public AppEventCircularReferenceException(string msg)
			: base(msg)
		{
		}

		protected AppEventCircularReferenceException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
