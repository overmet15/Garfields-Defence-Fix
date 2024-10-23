internal class StarStrike_HeavyBangBehaviour : StarStrike_ArmyBehaviourAdapter
{
	public StarStrike_HeavyBangBehaviour(StarStrike_ArmyUnit ownerUnit)
		: base(ownerUnit)
	{
		AddAction(new StarStrike_AttackUnitAction(this, new StarStrike_HeavyBangAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_UNIT_ACTION));
		AddAction(new StarStrike_AttackBaseAction(this, new StarStrike_HeavyBangAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_BASE_ACTION));
	}
}
