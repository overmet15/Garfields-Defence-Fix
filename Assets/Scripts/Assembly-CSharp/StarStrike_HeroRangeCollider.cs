using UnityEngine;

public class StarStrike_HeroRangeCollider : MonoBehaviour
{
	public enum MissleType
	{
		HEAL = 0,
		ATTACK = 1
	}

	private StarStrike_ArmyUnit thisArmyUnit;

	private StarStrike_ArmyBehaviour behaviour;

	public MissleType missleType;

	private float range;

	private int _PotentialTargetsLength = 200;

	private StarStrike_ArmyUnit[] _PotentialTargets = new StarStrike_ArmyUnit[200];

	private void Start()
	{
		thisArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(base.transform);
		StarStrike_Assertion.Assert(thisArmyUnit != null, "Army unit was not found when it is required.");
		range = base.transform.localScale.z;
		StarStrike_Assertion.Assert(range > 0f, "Range must be greater than zero.");
		ResetPotentialArray();
	}

	private void Update()
	{
		if (thisArmyUnit.checkNearestEnemy())
		{
			AttackNearestEnemy();
			thisArmyUnit.NearestEnemyChecked();
		}
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (thisArmyUnit.isHeroMoving())
		{
			FindTempEnemy(otherCollider);
		}
		else
		{
			CheckCollisionWithEnemyUnit(otherCollider);
		}
	}

	private void OnTriggerExit(Collider otherCollider)
	{
		StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
		if (!(starStrike_ArmyUnit == null) && starStrike_ArmyUnit.owner == thisArmyUnit.owner)
		{
		}
	}

	private void CheckCollisionWithEnemyUnit(Collider otherCollider)
	{
		StarStrike_RangeCollider starStrike_RangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_RangeCollider>(otherCollider.transform);
		if (starStrike_RangeCollider != null)
		{
			return;
		}
		StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
		if (!(starStrike_ArmyUnit == null) && starStrike_ArmyUnit.IsAlive() && IsEnemyUnit(starStrike_ArmyUnit))
		{
			thisArmyUnit.GetBehaviour().AddEnemyInCombat(starStrike_ArmyUnit);
			ResetPotentialArray();
			if (ShouldTargetAttackMe(starStrike_ArmyUnit))
			{
				starStrike_ArmyUnit.GetBehaviour().AddEnemyInCombat(thisArmyUnit);
			}
		}
	}

	private bool ShouldTargetAttackMe(StarStrike_ArmyUnit target)
	{
		StarStrike_RangeCollider componentInChildren = target.GetComponentInChildren<StarStrike_RangeCollider>();
		if (componentInChildren == null)
		{
			return false;
		}
		return StarStrike_Comparison.TolerantGreaterThanOrEquals(componentInChildren.GetRange(), GetRange());
	}

	public float GetRange()
	{
		return range;
	}

	private bool IsEnemyUnit(StarStrike_ArmyUnit otherArmyUnit)
	{
		if (otherArmyUnit == null)
		{
			return false;
		}
		if (thisArmyUnit.GetOwner() == otherArmyUnit.GetOwner())
		{
			return false;
		}
		return true;
	}

	private void ResetPotentialArray()
	{
		for (int i = 0; i < _PotentialTargetsLength; i++)
		{
			_PotentialTargets[i] = null;
		}
	}

	private int ReturnPotentialTargetID()
	{
		int num = 0;
		for (int i = 0; i < _PotentialTargetsLength; i++)
		{
			if (_PotentialTargets[i] == null)
			{
				return num;
			}
			num++;
		}
		return 0;
	}

	public void FindTempEnemy(Collider otherCollider)
	{
		UpdateTempTargets(otherCollider);
	}

	private void UpdateTempTargets(Collider otherCollider)
	{
		StarStrike_RangeCollider starStrike_RangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_RangeCollider>(otherCollider.transform);
		if (!(starStrike_RangeCollider != null))
		{
			StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
			if (IsEnemyUnit(starStrike_ArmyUnit) && starStrike_ArmyUnit.IsAlive())
			{
				int num = ReturnPotentialTargetID();
				_PotentialTargets[num] = starStrike_ArmyUnit;
				starStrike_ArmyUnit.SetHeroTempTarget(num);
			}
		}
	}

	public void RemoveTempTargetArr(int id)
	{
		_PotentialTargets[id] = null;
	}

	public void AttackNearestEnemy()
	{
		float num = thisArmyUnit.ReturnTransformX();
		behaviour = thisArmyUnit.ReturnBehaviour();
		int num2 = 0;
		float num3 = 99f;
		bool flag = false;
		float f = 999f;
		StarStrike_ArmyUnit[] potentialTargets = _PotentialTargets;
		foreach (StarStrike_ArmyUnit starStrike_ArmyUnit in potentialTargets)
		{
			if (starStrike_ArmyUnit != null)
			{
				float num4 = starStrike_ArmyUnit.ReturnTransformX();
				float num5 = num - num4;
				if (num5 < num3)
				{
					num3 = num5;
					num2 = starStrike_ArmyUnit.ReturnTempTargetTicket();
					flag = true;
					f = num5;
				}
			}
		}
		if (!behaviour.HasEnemyInCombat() && flag && _PotentialTargets[num2] != null)
		{
			behaviour.AddEnemyInCombat(_PotentialTargets[num2]);
			if (Mathf.Abs(Mathf.Ceil(f)) < 10f)
			{
				thisArmyUnit.HeroMeleeAttacking();
			}
			else
			{
				thisArmyUnit.HeroRangeAttacking();
			}
			ResetPotentialArray();
		}
	}
}
