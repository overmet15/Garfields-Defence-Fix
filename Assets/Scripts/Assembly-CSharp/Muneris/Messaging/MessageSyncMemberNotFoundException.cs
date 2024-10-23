using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.MessageSyncMemberNotFoundException")]
	public class MessageSyncMemberNotFoundException : MessageSyncException
	{
		public MessageSyncMemberNotFoundException(string msg)
			: base(msg)
		{
		}

		protected MessageSyncMemberNotFoundException(ObjectId objectId)
			: base(objectId)
		{
		}
	}
}
