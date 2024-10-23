using UnityEngine;

internal class StarStrike_RangedPunchAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private static float PUNCH_AT_ENEMY_TIME = 0.25f;

	private Animation glovesAnimation;

	private StarStrike_CountdownTimer punchEnemyTimer;

	public StarStrike_RangedPunchAttackBehaviour(StarStrike_ArmyBehaviour behaviour)
	{
		glovesAnimation = behaviour.GetOwnerUnit().transform.Find("Gloves").GetComponent<Animation>();
		StarStrike_Assertion.Assert(glovesAnimation != null, "Animation for gloves was not found.");
		punchEnemyTimer = new StarStrike_CountdownTimer(PUNCH_AT_ENEMY_TIME);
	}

	public override void Start()
	{
		base.Start();
		punchEnemyTimer.Reset();
		glovesAnimation.Play("GloveAttack");
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		if (!glovesAnimation.IsPlaying("GloveAttack"))
		{
			MarkAsDone();
		}
		else if (punchEnemyTimer.HasElapsed())
		{
			MarkTimeToDealDamage();
		}
		else
		{
			punchEnemyTimer.Update();
		}
	}
}
