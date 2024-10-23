using UnityEngine;

internal class StarStrike_MeleeAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private static string ATTACK_ANIMATION = "Attack";

	private StarStrike_ArmyUnit armyUnit;

	private Animation viewAnimation;

	private StarStrike_CountdownTimer dealDamageTimer;

	private StarStrike_EventType eventOnDealDamage;

	public StarStrike_MeleeAttackBehaviour(StarStrike_ArmyUnit armyUnit, float dealDamageTime, StarStrike_EventType eventOnDealDamage)
	{
		this.armyUnit = armyUnit;
		viewAnimation = armyUnit.transform.Find("View").GetComponentInChildren<Animation>();
		dealDamageTimer = new StarStrike_CountdownTimer(dealDamageTime);
		this.eventOnDealDamage = eventOnDealDamage;
	}

	public override void Start()
	{
		base.Start();
		PlayAnimation();
		dealDamageTimer.Reset();
	}

	private void PlayAnimation()
	{
		if (viewAnimation != null && (bool)viewAnimation.GetClip(ATTACK_ANIMATION))
		{
			viewAnimation.CrossFade(ATTACK_ANIMATION);
		}
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		if (!IsTimeToDealDamage() && dealDamageTimer.HasElapsed())
		{
			MarkTimeToDealDamage();
			if (targetable != null)
			{
				StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(eventOnDealDamage);
				starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, armyUnit.gameObject);
				StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
			}
		}
		else
		{
			dealDamageTimer.Update();
		}
		if (!viewAnimation.IsPlaying(ATTACK_ANIMATION))
		{
			MarkAsDone();
		}
	}
}
