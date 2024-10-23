using System.Runtime.CompilerServices;

namespace Muneris.Bridge
{
	public sealed class BridgeObjectImpl : IBridgeObject
	{
		private bool mIsDummyObject;

		private long mObjectId;

		public BridgeObjectImpl(ObjectId objectId)
		{
			Init(objectId);
		}

		private void Init(ObjectId objectId)
		{
			DebugUtil.Assert(mObjectId == 0, string.Empty);
			mObjectId = objectId;
		}

		public void SetAsDummyObject()
		{
			mIsDummyObject = true;
		}

		~BridgeObjectImpl()
		{
			if (mObjectId != 0L && !mIsDummyObject)
			{
				DebugUtil.Log("~BridgeObject(): Releasing native object with id: " + mObjectId);
				TaskDispatcher.RunOnMainThread(_003CFinalize_003Em__6);
			}
		}

		public long GetObjectId()
		{
			return mObjectId;
		}

		public string GetNativeClassName()
		{
			return TypeRegistry.GetNativeClassName(GetType());
		}

		public override string ToString()
		{
			if (mObjectId != 0L && !mIsDummyObject)
			{
				return JniHelper.CallStatic<string>("muneris.bridgehelper.Bridge", "toString", new object[1] { mObjectId });
			}
			return base.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BridgeObjectImpl bridgeObjectImpl = obj as BridgeObjectImpl;
			if (bridgeObjectImpl == null)
			{
				return false;
			}
			if (mObjectId == bridgeObjectImpl.mObjectId)
			{
				return true;
			}
			if (mObjectId != 0L && !mIsDummyObject)
			{
				return JniHelper.CallStatic<bool>("muneris.bridgehelper.Bridge", "isEqual", new object[2] { mObjectId, bridgeObjectImpl.mObjectId });
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (mObjectId != 0L && !mIsDummyObject)
			{
				return JniHelper.CallStatic<int>("muneris.bridgehelper.Bridge", "hashCode", new object[1] { mObjectId });
			}
			return mObjectId.GetHashCode();
		}

		[CompilerGenerated]
		private void _003CFinalize_003Em__6()
		{
			if (JniHelper.CallStatic<bool>("muneris.bridgehelper.ObjectManager", "releaseObject", new object[1] { mObjectId }))
			{
				DebugUtil.Log("Released java object with id " + mObjectId);
			}
			else
			{
				DebugUtil.LogError("Failed to release java id: " + mObjectId);
			}
		}
	}
}
