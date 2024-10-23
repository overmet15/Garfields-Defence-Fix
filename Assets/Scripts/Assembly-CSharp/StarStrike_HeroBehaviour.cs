internal class StarStrike_HeroBehaviour : StarStrike_ArmyBehaviourAdapter
{
	public StarStrike_HeroBehaviour(StarStrike_ArmyUnit ownerUnit)
		: base(ownerUnit)
	{
		AddAction(new StarStrike_AttackUnitAction(this, new StarStrike_HeroAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_UNIT_ACTION));
		AddAction(new StarStrike_AttackBaseAction(this, new StarStrike_HeroAttackBehaviour(this), StarStrike_ArmyUnit.ATTACK_BASE_ACTION));
	}
}
