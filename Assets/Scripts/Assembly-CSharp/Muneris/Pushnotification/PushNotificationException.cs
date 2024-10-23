using Muneris.Bridge;

namespace Muneris.Pushnotification
{
	[BridgeObjectInfo(NativeClass = "muneris.android.pushnotification.PushNotificationException")]
	public class PushNotificationException : MunerisException
	{
		public PushNotificationException(string msg)
			: base(msg)
		{
		}

		protected PushNotificationException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
