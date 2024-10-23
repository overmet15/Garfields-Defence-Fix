internal class StarStrike_AttackBaseAction : StarStrike_AbstractAction
{
	private StarStrike_ArmyBehaviour behaviour;

	private StarStrike_AttackBehaviour attackBehaviour;

	private bool damageDealt;

	public StarStrike_AttackBaseAction(StarStrike_ArmyBehaviour behaviour, StarStrike_AttackBehaviour attackBehaviour, string actionId)
		: base(actionId)
	{
		this.behaviour = behaviour;
		this.attackBehaviour = attackBehaviour;
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		damageDealt = false;
		attackBehaviour.Start();
	}

	public override void OnReveal()
	{
		base.OnReveal();
		damageDealt = false;
		attackBehaviour.Start();
	}

	public override void Update()
	{
		attackBehaviour.Update(behaviour.GetEnemyBase());
		if (!damageDealt && behaviour.GetOwnerUnit().IsAlive() && attackBehaviour.IsTimeToDealDamage())
		{
			DealDamage();
			damageDealt = true;
		}
		if (attackBehaviour.IsDone())
		{
			MarkAsDone();
		}
	}

	protected StarStrike_ArmyBehaviour GetBehaviour()
	{
		return behaviour;
	}

	protected void DealDamage()
	{
		int attackDamageToBase = behaviour.GetOwnerUnit().GetAttackDamageToBase();
		behaviour.GetEnemyBase().ReceiveDamage(attackDamageToBase);
	}
}
