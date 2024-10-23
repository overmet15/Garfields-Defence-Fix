internal class StarStrike_AttackUnitAction : StarStrike_AbstractAction
{
	private StarStrike_ArmyBehaviour behaviour;

	private StarStrike_AttackBehaviour attackBehaviour;

	private bool damageDealt;

	public StarStrike_AttackUnitAction(StarStrike_ArmyBehaviour behaviour, StarStrike_AttackBehaviour attackBehaviour, string actionId)
		: base(actionId)
	{
		this.behaviour = behaviour;
		this.attackBehaviour = attackBehaviour;
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		StartAttack();
	}

	public override void OnReveal()
	{
		base.OnReveal();
		StartAttack();
	}

	private void StartAttack()
	{
		damageDealt = false;
		if (GetBehaviour().HasEnemyInCombat())
		{
			attackBehaviour.Start();
		}
		else
		{
			MarkAsDone();
		}
	}

	public override void Update()
	{
		behaviour.CleanDeadTargets();
		if (!behaviour.HasEnemyInCombat())
		{
			attackBehaviour.Update(null);
			if (attackBehaviour.IsDone())
			{
				MarkAsDone();
			}
			return;
		}
		attackBehaviour.Update(behaviour.GetNextTarget());
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
		StarStrike_ArmyUnit ownerUnit = behaviour.GetOwnerUnit();
		StarStrike_ArmyUnit nextTarget = behaviour.GetNextTarget();
		int attackDamage = ownerUnit.GetAttackDamage();
		attackDamage = ((attackDamage >= 0) ? attackDamage : 0);
		nextTarget.ReceiveDamage(attackDamage);
	}
}
