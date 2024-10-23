internal class StarStrike_HeavyRotateBehaviour : StarStrike_ArmyBehaviourAdapter
{
	public StarStrike_HeavyRotateBehaviour(StarStrike_ArmyUnit ownerUnit)
		: base(ownerUnit)
	{
		AddAction(new StarStrike_AttackUnitAction(this, new StarStrike_HeavyRotateAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_UNIT_ACTION));
		AddAction(new StarStrike_AttackBaseAction(this, new StarStrike_HeavyRotateAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_BASE_ACTION));
	}
}
