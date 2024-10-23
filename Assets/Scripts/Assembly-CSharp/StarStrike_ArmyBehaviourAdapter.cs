using System.Collections;
using UnityEngine;

internal class StarStrike_ArmyBehaviourAdapter : StarStrike_ArmyBehaviour
{
	private bool enemyBaseInRange;

	private StarStrike_Base enemyBase;

	private StarStrike_ArmyUnit ownerUnit;

	private StarStrike_TargetQueue targetQueue;

	private Hashtable actionMap;

	public StarStrike_ArmyBehaviourAdapter(StarStrike_ArmyUnit ownerUnit)
	{
		this.ownerUnit = ownerUnit;
		enemyBaseInRange = false;
		enemyBase = null;
		targetQueue = new StarStrike_TargetQueue(ownerUnit.GetOwner());
		actionMap = new Hashtable();
	}

	public StarStrike_ArmyUnit GetOwnerUnit()
	{
		return ownerUnit;
	}

	public virtual void CleanHeroTargets()
	{
		if (ownerUnit.IsEnemy() && !ownerUnit.IsHeroOff())
		{
		}
	}

	public virtual void CleanDeadTargets()
	{
		if (targetQueue.IsEmpty())
		{
			return;
		}
		StarStrike_ArmyUnit starStrike_ArmyUnit = null;
		do
		{
			starStrike_ArmyUnit = targetQueue.Front();
			if ((bool)starStrike_ArmyUnit)
			{
				if (starStrike_ArmyUnit.unitName != "Hero")
				{
				}
				if (ownerUnit.IsHeroOff())
				{
					if (!targetQueue.IsEmpty())
					{
						targetQueue.Dequeue();
					}
					starStrike_ArmyUnit.SetHeroOff(false);
				}
				else
				{
					if (starStrike_ArmyUnit.IsAlive())
					{
						break;
					}
					targetQueue.Dequeue();
				}
			}
			else
			{
				targetQueue.Dequeue();
			}
		}
		while (!targetQueue.IsEmpty());
	}

	public virtual bool IsEnemyBaseInRange()
	{
		return enemyBaseInRange;
	}

	public virtual void MarkEnemyBaseInRange()
	{
		enemyBaseInRange = true;
	}

	public virtual void UnmarkEnemyBaseInRange()
	{
		enemyBaseInRange = false;
	}

	public virtual void OnCollideWithBody(Collider otherCollider)
	{
	}

	public virtual void SetEnemyBase(StarStrike_Base enemyBase)
	{
		this.enemyBase = enemyBase;
	}

	public StarStrike_Base GetEnemyBase()
	{
		StarStrike_Assertion.Assert(enemyBase != null, "Enemy base should not be null when it is requested.");
		return enemyBase;
	}

	public virtual void AddEnemyInCombat(StarStrike_ArmyUnit unit)
	{
		targetQueue.Enqueue(unit);
	}

	public virtual void RemoveEnemyInCombat()
	{
		targetQueue.Clear();
	}

	public virtual void RemoveUnit(StarStrike_ArmyUnit unit)
	{
		targetQueue.Remove(unit);
	}

	public virtual bool HasEnemyInCombat()
	{
		return !targetQueue.IsEmpty();
	}

	public virtual StarStrike_ArmyUnit GetNextTarget()
	{
		return targetQueue.Front();
	}

	public virtual StarStrike_Action GetAction(string actionId)
	{
		StarStrike_Action starStrike_Action = (StarStrike_Action)actionMap[actionId];
		StarStrike_Assertion.Assert(starStrike_Action != null, "The requested action should not be null (" + actionId + ").");
		return starStrike_Action;
	}

	protected void AddAction(StarStrike_Action action)
	{
		StarStrike_Assertion.Assert(!actionMap.ContainsKey(action.GetId()), "The action map already contains the specified action. Can't add the action.");
		actionMap.Add(action.GetId(), action);
	}

	protected Owner GetOwner()
	{
		return ownerUnit.GetOwner();
	}

	protected StarStrike_ArmyUnit GetFrontTarget()
	{
		Debug.Log("GetFrontTarget");
		return targetQueue.Front();
	}

	public virtual void RemoveTempTargetArr(int id)
	{
	}

	public virtual void AttackNearestEnemy(Transform thisTransform)
	{
	}

	public virtual void FindTempEnemy(Collider otherCollider)
	{
	}
}
