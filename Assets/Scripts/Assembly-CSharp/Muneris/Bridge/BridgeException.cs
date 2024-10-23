using System;

namespace Muneris.Bridge
{
	public abstract class BridgeException : Exception, IBridgeObject
	{
		private BridgeObjectImpl mImpl;

		public override string Message
		{
			get
			{
				if (mImpl.GetObjectId() != 0L)
				{
					return JniHelper.CallStatic<string>("muneris.bridgehelper.Bridge", "getExceptionMessage", new object[1] { mImpl.GetObjectId() });
				}
				return base.Message;
			}
		}

		protected BridgeException(ObjectId objectId)
		{
			Init(objectId);
		}

		public BridgeException(string message)
			: base(message)
		{
			Init(0L);
		}

		protected void SetAsDummyObject()
		{
			mImpl.SetAsDummyObject();
		}

		protected void Init(ObjectId objectId)
		{
			mImpl = new BridgeObjectImpl(objectId);
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
			if (mImpl.GetObjectId() != 0L)
			{
				return mImpl.ToString();
			}
			return base.ToString();
		}

		public override bool Equals(object obj)
		{
			BridgeException ex = obj as BridgeException;
			if (ex == null)
			{
				return false;
			}
			if (mImpl.GetObjectId() != 0L)
			{
				return mImpl.Equals(ex.mImpl);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (mImpl.GetObjectId() != 0L)
			{
				return mImpl.GetHashCode();
			}
			return mImpl.GetHashCode();
		}
	}
}
