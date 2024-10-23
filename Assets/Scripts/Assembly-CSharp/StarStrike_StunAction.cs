using UnityEngine;

internal class StarStrike_StunAction : StarStrike_AbstractAction
{
	private StarStrike_CountdownTimer stunTimer;

	private Animation viewAnimation;

	private GameObject stunIndicator;

	//private ParticleEmitter birdsEmitter;

	//private ParticleEmitter starsEmitter;

	public StarStrike_StunAction(StarStrike_ArmyUnit unit, float stunTime, string actionId)
		: base(actionId)
	{
		viewAnimation = unit.transform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(viewAnimation != null, "viewAnimation must not be null");
		stunIndicator = unit.transform.Find("StunIndicator").gameObject;
		stunTimer = new StarStrike_CountdownTimer(stunTime);
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		viewAnimation.Play("Idle");
		StarStrike_Utils.ShowObject(stunIndicator);
	}

	public override void Update()
	{
		stunTimer.Update();
		if (stunTimer.HasElapsed())
		{
			StarStrike_Utils.HideObject(stunIndicator);
			MarkAsDone();
		}
	}
}
