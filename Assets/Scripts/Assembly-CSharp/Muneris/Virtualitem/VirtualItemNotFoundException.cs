using Muneris.Bridge;

namespace Muneris.Virtualitem
{
	[BridgeObjectInfo(NativeClass = "muneris.android.virtualitem.VirtualItemNotFoundException")]
	public class VirtualItemNotFoundException : VirtualItemException
	{
		public VirtualItemNotFoundException(string msg)
			: base(msg)
		{
		}

		protected VirtualItemNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
