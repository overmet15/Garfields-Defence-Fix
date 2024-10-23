using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.InvalidArgumentException")]
	public class InvalidArgumentException : MunerisException
	{
		public InvalidArgumentException(string msg)
			: base(msg)
		{
		}

		protected InvalidArgumentException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
