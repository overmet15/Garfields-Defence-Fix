using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.BadCredentialsException")]
	public class BadCredentialsException : AccessDeniedException
	{
		public BadCredentialsException(string msg)
			: base(msg)
		{
		}

		protected BadCredentialsException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
