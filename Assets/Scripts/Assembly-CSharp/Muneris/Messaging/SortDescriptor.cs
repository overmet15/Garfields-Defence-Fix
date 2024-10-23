using Muneris.Bridge;

namespace Muneris.Messaging
{
	[BridgeObjectInfo(NativeClass = "muneris.android.messaging.SortDescriptor")]
	public class SortDescriptor : BridgeObject
	{
		public enum Field
		{
			CreationDate = 0
		}

		public enum Order
		{
			Ascending = 0,
			Descending = 1
		}

		private static string _bridgeClassName = "muneris.bridge.messaging.SortDescriptorBridge";

		public SortDescriptor(Field field, Order order)
			: base(0L)
		{
			int num = SerializationHelper.Serialize(field);
			int num2 = SerializationHelper.Serialize(order);
			long num3 = JniHelper.CallStatic<long>(_bridgeClassName, "SortDescriptor____SortDescriptor_Field_SortDescriptor_Order", new object[2] { num, num2 });
			Init(num3);
		}

		protected SortDescriptor(ObjectId objectId)
			: base(objectId)
		{
		}

		public Field getField()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getField___SortDescriptor_Field", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Field>(num);
		}

		public Order getOrder()
		{
			int num = JniHelper.CallStatic<int>(_bridgeClassName, "getOrder___SortDescriptor_Order", new object[1] { GetObjectId() });
			return SerializationHelper.Deserialize<Order>(num);
		}
	}
}
