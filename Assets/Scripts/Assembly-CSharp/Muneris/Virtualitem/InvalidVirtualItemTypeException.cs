using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.InvalidVirtualItemTypeException")]
	public class InvalidVirtualItemTypeException : MunerisException
	{
		public InvalidVirtualItemTypeException(string msg)
			: base(msg)
		{
		}

		protected InvalidVirtualItemTypeException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
