using UnityEngine;

namespace Outblaze
{
	public class SingletonMonoBehaviour<ChildType> : MonoBehaviour where ChildType : SingletonMonoBehaviour<ChildType>
	{
		private static ChildType instance_;

		public static ChildType Instance
		{
			get
			{
				if ((Object)instance_ == (Object)null)
				{
					instance_ = (ChildType)Object.FindObjectOfType(typeof(ChildType));
				}
				return instance_;
			}
			protected set
			{
				instance_ = value;
			}
		}

		protected virtual void Awake()
		{
			if (Instance != this)
			{
				Object.DestroyImmediate(this);
				return;
			}
			Instance = this as ChildType;
			Object.DontDestroyOnLoad(this);
		}

		protected virtual void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = (ChildType)null;
			}
		}
	}
}
