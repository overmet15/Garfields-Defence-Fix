using UnityEngine;

internal class StarStrike_MeleeBehaviour : StarStrike_ArmyBehaviourAdapter
{
	public StarStrike_MeleeBehaviour(StarStrike_ArmyUnit ownerUnit, float dealDamageTime, StarStrike_EventType onDealDamageEvent)
		: base(ownerUnit)
	{
		AddAction(new StarStrike_AttackUnitAction(this, new StarStrike_MeleeAttackBehaviour(ownerUnit, dealDamageTime, onDealDamageEvent), StarStrike_ArmyUnit.ATTACK_UNIT_ACTION));
		AddAction(new StarStrike_AttackBaseAction(this, new StarStrike_MeleeAttackBehaviour(ownerUnit, dealDamageTime, onDealDamageEvent), StarStrike_ArmyUnit.ATTACK_BASE_ACTION));
	}

	public override void OnCollideWithBody(Collider otherCollider)
	{
		CheckCollisionWithArmyUnit(otherCollider);
		CheckCollisionWithBase(otherCollider);
	}

	private void CheckCollisionWithArmyUnit(Collider otherCollider)
	{
		StarStrike_RangeCollider starStrike_RangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_RangeCollider>(otherCollider.transform);
		if (starStrike_RangeCollider != null)
		{
			return;
		}
		StarStrike_HeroRangeCollider starStrike_HeroRangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_HeroRangeCollider>(otherCollider.transform);
		if (!(starStrike_HeroRangeCollider != null))
		{
			StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
			if (IsEnemyUnit(starStrike_ArmyUnit) && starStrike_ArmyUnit.IsAlive())
			{
				AddEnemyInCombat(starStrike_ArmyUnit);
			}
		}
	}

	private bool IsEnemyUnit(StarStrike_ArmyUnit otherArmyUnit)
	{
		if (otherArmyUnit == null)
		{
			return false;
		}
		if (GetOwner() == otherArmyUnit.GetOwner())
		{
			return false;
		}
		return true;
	}

	private void CheckCollisionWithBase(Collider otherCollider)
	{
		StarStrike_Base starStrike_Base = StarStrike_Utils.FindComponentThroughParent<StarStrike_Base>(otherCollider.transform);
		if (!(starStrike_Base == null) && GetOwner() != starStrike_Base.GetOwner())
		{
			SetEnemyBase(starStrike_Base);
			MarkEnemyBaseInRange();
		}
	}
}
