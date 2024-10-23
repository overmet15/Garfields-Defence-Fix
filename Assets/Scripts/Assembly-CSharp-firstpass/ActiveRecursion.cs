using UnityEngine;

public static class ActiveRecursion
{
	public static void SetActiveRecursivelyLegacy(this GameObject go, bool active)
	{
		if (!go)
		{
			return;
		}
		foreach (Transform item in go.transform)
		{
			item.gameObject.SetActiveRecursivelyLegacy(active);
		}
		go.SetActive(active);
	}
}
