namespace Muneris.Bridge
{
	public struct ObjectId
	{
		private long value;

		public ObjectId(long value)
		{
			this.value = value;
		}

		public static implicit operator ObjectId(long value)
		{
			return new ObjectId(value);
		}

		public static implicit operator long(ObjectId objectId)
		{
			return objectId.value;
		}
	}
}
