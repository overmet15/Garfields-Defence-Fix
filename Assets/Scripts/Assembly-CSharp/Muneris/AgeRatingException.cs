using Muneris.Bridge;

namespace Muneris
{
	[BridgeObjectInfo(NativeClass = "muneris.android.AgeRatingException")]
	public class AgeRatingException : MunerisException
	{
		public AgeRatingException(string msg)
			: base(msg)
		{
		}

		protected AgeRatingException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
