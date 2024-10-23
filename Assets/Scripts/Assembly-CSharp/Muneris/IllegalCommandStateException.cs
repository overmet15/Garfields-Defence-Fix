using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.IllegalCommandStateException")]
	public class IllegalCommandStateException : MunerisException
	{
		public IllegalCommandStateException(string msg)
			: base(msg)
		{
		}

		protected IllegalCommandStateException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
