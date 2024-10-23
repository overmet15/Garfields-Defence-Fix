internal class StarStrike_RangedLaserBehaviour : StarStrike_ArmyBehaviourAdapter
{
	public StarStrike_RangedLaserBehaviour(StarStrike_ArmyUnit ownerUnit)
		: base(ownerUnit)
	{
		AddAction(new StarStrike_AttackUnitAction(this, new StarStrike_RangedLaserAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_UNIT_ACTION));
		AddAction(new StarStrike_AttackBaseAction(this, new StarStrike_RangedLaserAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_BASE_ACTION));
	}
}
