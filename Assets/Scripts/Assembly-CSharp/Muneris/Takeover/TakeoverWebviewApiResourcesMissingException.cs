using Muneris.Bridge;

namespace Muneris.Takeover
{
	[BridgeObjectInfo(NativeClass = "muneris.android.takeover.TakeoverWebviewApiResourcesMissingException")]
	public class TakeoverWebviewApiResourcesMissingException : TakeoverWebviewException
	{
		public TakeoverWebviewApiResourcesMissingException(string msg)
			: base(msg)
		{
		}

		protected TakeoverWebviewApiResourcesMissingException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
