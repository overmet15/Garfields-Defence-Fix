using UnityEngine;

internal class StarStrike_Utils
{
	private StarStrike_Utils()
	{
	}

	public static void SetAsParent(Transform parentTransform, Transform childTransform)
	{
		childTransform.parent = parentTransform;
	}

	public static void SeedRandomizer()
	{
		Random.InitState((int)(Time.time * 10000f));
	}

	public static bool RandomBoolean()
	{
		int num = Random.Range(0, 2);
		return num > 0;
	}

	public static void HideObject(GameObject gameObject)
	{
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			renderer.enabled = false;
		}
	}

	public static void ShowObject(GameObject gameObject)
	{
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			renderer.enabled = true;
		}
	}

	public static float Clamp(float value, float min, float max)
	{
		if (value < min)
		{
			return min;
		}
		if (value > max)
		{
			return max;
		}
		return value;
	}

	public static int Clamp(int value, int min, int max)
	{
		if (value < min)
		{
			return min;
		}
		if (value > max)
		{
			return max;
		}
		return value;
	}

	public static TComponentType FindComponentThroughParent<TComponentType>(Transform transform) where TComponentType : Component
	{
		TComponentType component = transform.GetComponent<TComponentType>();
		if ((Object)component == (Object)null)
		{
			if (transform.parent == null)
			{
				return (TComponentType)null;
			}
			return FindComponentThroughParent<TComponentType>(transform.parent);
		}
		return component;
	}

	public static bool IsEmpty(string str)
	{
		return str == null || str.Length == 0;
	}

	public static bool ContainsObjectWithName(Transform transform, string name)
	{
		if (name.Equals(transform.gameObject.name))
		{
			return true;
		}
		Transform transform2 = transform.Find(name);
		if (transform2 != null)
		{
			return true;
		}
		Transform parent = transform.parent;
		do
		{
			if (parent != null)
			{
				if (name.Equals(parent.gameObject.name))
				{
					return true;
				}
				parent = parent.parent;
			}
		}
		while (parent != null);
		return false;
	}

	public static Transform FindTransformByName(Transform transformRoot, string name)
	{
		if (name.Equals(transformRoot.name))
		{
			return transformRoot;
		}
		Transform transform = null;
		foreach (Transform item in transformRoot)
		{
			transform = FindTransformByName(item, name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}
}
