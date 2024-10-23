using Muneris.Bridge;

namespace Muneris.Virtualgood
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualgood.RestoreFailedException")]
	public class RestoreFailedException : VirtualGoodsException
	{
		public RestoreFailedException(string msg)
			: base(msg)
		{
		}

		protected RestoreFailedException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
