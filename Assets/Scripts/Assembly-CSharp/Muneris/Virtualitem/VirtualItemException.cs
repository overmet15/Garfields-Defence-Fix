using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItemException")]
	public class VirtualItemException : MunerisException
	{
		public VirtualItemException(string msg)
			: base(msg)
		{
		}

		protected VirtualItemException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
