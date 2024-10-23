using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.GroupDescriptor")]
	public class GroupDescriptor : BridgeObject
	{
		public enum Field
		{
			Conversation = 0,
			SourceType = 1,
			TargetType = 2
		}

		private static string _bridgeClassName = "muneris.bridge.messaging.GroupDescriptorBridge";

		public GroupDescriptor(Field field)
			: base(0L)
		{
			int num = SerializationHelper.Serialize(field);
			long num2 = JniHelper.CallStatic<long>(_bridgeClassName, "GroupDescriptor____GroupDescriptor_Field", new object[1] { num });
			Init(num2);
		}

		protected GroupDescriptor(ObjectId objectId)
			: base(objectId)
		{
		}

		public Field getField()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getField___GroupDescriptor_Field", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Field>(num);
		}
	}
}
