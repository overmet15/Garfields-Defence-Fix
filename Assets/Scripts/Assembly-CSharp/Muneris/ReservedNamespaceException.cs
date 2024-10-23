using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.ReservedNamespaceException")]
	public class ReservedNamespaceException : MunerisException
	{
		public ReservedNamespaceException(string msg)
			: base(msg)
		{
		}

		protected ReservedNamespaceException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
