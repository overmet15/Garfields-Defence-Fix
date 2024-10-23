using System;
using System.Collections.Generic;

public static class RandomUtils
{
	private static Random random = new Random();

	public static List<T> RandomListItems<T>(ICollection<T> collection, int elementsCount)
	{
		List<T> list = new List<T>();
		int num = elementsCount;
		int num2 = collection.Count;
		foreach (T item in collection)
		{
			double num3 = (double)num / (double)num2;
			if (random.NextDouble() < num3)
			{
				list.Add(item);
				num--;
			}
			num2--;
		}
		return list;
	}

	public static bool RandomAction(float probability, Action successCallback, Action failCallback)
	{
		bool flag = (float)random.Next(0, 100) / 100f < probability;
		Action action = ((!flag) ? failCallback : successCallback);
		if (action != null)
		{
			action();
		}
		return flag;
	}
}
