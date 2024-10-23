using UnityEngine;

internal class StarStrike_HeavyBangAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private const float DEAL_DAMAGE_TIME = 0.16667f;

	private static string ATTACK_ANIMATION = "RoboSpikeAttack";

	private Animation headAnimation;

	private StarStrike_CountdownTimer dealDamageTimer;

	public StarStrike_HeavyBangAttackBehaviour(StarStrike_ArmyBehaviour behaviour)
	{
		headAnimation = behaviour.GetOwnerUnit().transform.Find("Mesh").Find("Head").GetComponent<Animation>();
		StarStrike_Assertion.Assert(headAnimation != null, "Head animation was not found when it is required.");
		dealDamageTimer = new StarStrike_CountdownTimer(0.16667f);
	}

	public override void Start()
	{
		base.Start();
		dealDamageTimer.Reset();
		headAnimation.Play(ATTACK_ANIMATION);
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		if (!headAnimation.IsPlaying(ATTACK_ANIMATION))
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
