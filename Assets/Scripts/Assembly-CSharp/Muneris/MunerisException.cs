using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.MunerisException")]
	public class MunerisException : BridgeException
	{
		public MunerisException(string msg)
			: base(msg)
		{
		}

		protected MunerisException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
