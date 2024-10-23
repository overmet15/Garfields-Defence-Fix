using UnityEngine;

public class StarStrike_RangeCollider : MonoBehaviour
{
	public enum MissleType
	{
		HEAL = 0,
		ATTACK = 1
	}

	private StarStrike_ArmyUnit thisArmyUnit;

	public MissleType missleType;

	private float range;

	private void Start()
	{
		thisArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(base.transform);
		StarStrike_Assertion.Assert(thisArmyUnit != null, "Army unit was not found when it is required.");
		range = base.transform.localScale.z;
		StarStrike_Assertion.Assert(range > 0f, "Range must be greater than zero.");
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (!CheckCollisionWithEnemyUnit(otherCollider))
		{
			CheckCollisionWithEnemyBase(otherCollider);
		}
	}

	private void OnTriggerExit(Collider otherCollider)
	{
		StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
		if (starStrike_ArmyUnit == null)
		{
			return;
		}
		if (missleType == MissleType.ATTACK)
		{
			if (starStrike_ArmyUnit.owner != thisArmyUnit.owner)
			{
				Debug.Log("**********ATTACK Range RemoveEnemyInCombat*********");
				thisArmyUnit.GetBehaviour().RemoveUnit(starStrike_ArmyUnit);
			}
		}
		else if (missleType == MissleType.HEAL)
		{
			if (starStrike_ArmyUnit.owner == thisArmyUnit.owner)
			{
				Debug.Log("thisArmyUnit.GetBehaviour().RemoveUnit");
				thisArmyUnit.GetBehaviour().RemoveUnit(starStrike_ArmyUnit);
			}
			if (thisArmyUnit.owner == Owner.TOM && !thisArmyUnit.GetBehaviour().HasEnemyInCombat() && starStrike_ArmyUnit.GetUnitType() == UnitType.HERO && thisArmyUnit.IsAlive())
			{
				thisArmyUnit.DoctorMove();
			}
		}
	}

	private bool CheckCollisionWithEnemyUnit(Collider otherCollider)
	{
		StarStrike_RangeCollider starStrike_RangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_RangeCollider>(otherCollider.transform);
		if (starStrike_RangeCollider != null)
		{
			return false;
		}
		StarStrike_HeroRangeCollider starStrike_HeroRangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_HeroRangeCollider>(otherCollider.transform);
		if (starStrike_HeroRangeCollider != null)
		{
			return false;
		}
		StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
		if (starStrike_ArmyUnit == null)
		{
			return false;
		}
		if (!starStrike_ArmyUnit.IsAlive())
		{
			return false;
		}
		if (!IsEnemyUnit(starStrike_ArmyUnit))
		{
			if (thisArmyUnit.GetUnitType() == UnitType.HEAL)
			{
				if (thisArmyUnit.IsAlive())
				{
					thisArmyUnit.DoctorMove();
				}
				Debug.Log("*******HEAL otherArmyUnit: " + starStrike_ArmyUnit.GetUnitType());
				if (starStrike_ArmyUnit.NeedHeal())
				{
					thisArmyUnit.GetBehaviour().AddEnemyInCombat(starStrike_ArmyUnit);
				}
			}
			return false;
		}
		if (thisArmyUnit.GetUnitType() == UnitType.HEAL)
		{
			thisArmyUnit.Stand();
			return false;
		}
		thisArmyUnit.GetBehaviour().AddEnemyInCombat(starStrike_ArmyUnit);
		if (ShouldTargetAttackMe(starStrike_ArmyUnit))
		{
			starStrike_ArmyUnit.GetBehaviour().AddEnemyInCombat(thisArmyUnit);
		}
		return true;
	}

	private bool ShouldTargetAttackMe(StarStrike_ArmyUnit target)
	{
		StarStrike_RangeCollider componentInChildren = target.GetComponentInChildren<StarStrike_RangeCollider>();
		StarStrike_HeroRangeCollider componentInChildren2 = target.GetComponentInChildren<StarStrike_HeroRangeCollider>();
		if (componentInChildren == null)
		{
			return false;
		}
		if (componentInChildren2 == null)
		{
			return false;
		}
		return StarStrike_Comparison.TolerantGreaterThanOrEquals(componentInChildren.GetRange(), GetRange());
	}

	public float GetRange()
	{
		return range;
	}

	private void CheckCollisionWithEnemyBase(Collider otherCollider)
	{
		StarStrike_Base starStrike_Base = StarStrike_Utils.FindComponentThroughParent<StarStrike_Base>(otherCollider.transform);
		if (!(starStrike_Base == null) && starStrike_Base.GetOwner() != thisArmyUnit.GetOwner())
		{
			if (thisArmyUnit.GetUnitType() == UnitType.HEAL)
			{
				thisArmyUnit.Stand();
				return;
			}
			thisArmyUnit.GetBehaviour().MarkEnemyBaseInRange();
			thisArmyUnit.GetBehaviour().SetEnemyBase(starStrike_Base);
		}
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
}
