using UnityEngine;

internal class StarStrike_HeavyRotateAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private const float DEAL_DAMAGE_TIME = 0.16667f;

	private static string ATTACK_ANIMATION = "ACMEModel3Attack";

	private Animation unitAnimation;

	private StarStrike_CountdownTimer dealDamageTimer;

	public StarStrike_HeavyRotateAttackBehaviour(StarStrike_ArmyBehaviour behaviour)
	{
		unitAnimation = behaviour.GetOwnerUnit().transform.Find("Mesh").GetComponent<Animation>();
		StarStrike_Assertion.Assert(unitAnimation != null, "Unit animation was not found when it is required.");
		dealDamageTimer = new StarStrike_CountdownTimer(0.16667f);
	}

	public override void Start()
	{
		base.Start();
		dealDamageTimer.Reset();
		unitAnimation.Play(ATTACK_ANIMATION);
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		if (!unitAnimation.IsPlaying(ATTACK_ANIMATION))
		{
			MarkAsDone();
		}
		else if (dealDamageTimer.HasElapsed())
		{
			MarkTimeToDealDamage();
		}
		else
		{
			dealDamageTimer.Update();
		}
	}
}
