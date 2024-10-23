using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.RestoreNotSupportedException")]
	public class RestoreNotSupportedException : RestoreFailedException
	{
		public RestoreNotSupportedException(string msg)
			: base(msg)
		{
		}

		protected RestoreNotSupportedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
