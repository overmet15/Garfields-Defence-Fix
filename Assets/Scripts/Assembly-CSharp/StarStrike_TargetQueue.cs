using System.Collections;
using UnityEngine;

internal class StarStrike_TargetQueue
{
	private Owner owner;

	private ArrayList queue;

	public StarStrike_TargetQueue(Owner owner)
	{
		this.owner = owner;
		queue = new ArrayList();
	}

	public void Enqueue(StarStrike_ArmyUnit unit)
	{
		if (!queue.Contains(unit))
		{
			queue.Add(unit);
		}
	}

	public void Remove(StarStrike_ArmyUnit unit)
	{
		if (queue.Contains(unit))
		{
			queue.Remove(unit);
		}
	}

	public StarStrike_ArmyUnit Front()
	{
		StarStrike_Assertion.Assert(queue.Count > 0, "There are no enqueued target units.");
		return (StarStrike_ArmyUnit)queue[0];
	}

	public void Dequeue()
	{
		StarStrike_Assertion.Assert(queue.Count > 0, "There are no enqueued target units.");
		queue.RemoveAt(0);
	}

	public void Clear()
	{
		queue.Clear();
	}

	public bool IsEmpty()
	{
		return queue.Count == 0;
	}

	public int Count()
	{
		Debug.Log("=========owner===========" + owner);
		Debug.Log("=========Count===========" + queue.Count);
		return queue.Count;
	}
}
