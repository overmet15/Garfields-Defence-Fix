namespace Muneris.Bridge
{
	public abstract class BridgeObject : IBridgeObject
	{
		private BridgeObjectImpl mImpl;

		protected BridgeObject(ObjectId objectId)
		{
			Init(objectId);
		}

		protected void Init(ObjectId objectId)
		{
			mImpl = new BridgeObjectImpl(objectId);
		}

		protected void SetAsDummyObject()
		{
			mImpl.SetAsDummyObject();
		}

		public long GetObjectId()
		{
			return mImpl.GetObjectId();
		}

		public string GetNativeClassName()
		{
			return TypeRegistry.GetNativeClassName(GetType());
		}

		public override string ToString()
		{
			return mImpl.ToString();
		}

		public override bool Equals(object obj)
		{
			BridgeObject bridgeObject = obj as BridgeObject;
			if (bridgeObject == null)
			{
				return false;
			}
			return mImpl.Equals(bridgeObject.mImpl);
		}

		public override int GetHashCode()
		{
			return mImpl.GetHashCode();
		}
	}
}
