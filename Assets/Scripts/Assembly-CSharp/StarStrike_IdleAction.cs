using UnityEngine;

internal class StarStrike_IdleAction : StarStrike_AbstractAction
{
	private static string IDLE_ANIMATION = "Idle";

	private static string WALK_ANIMATION = "Walk";

	private Animation viewAnimation;

	public StarStrike_IdleAction(StarStrike_ArmyUnit armyUnit, string actionId)
		: base(actionId)
	{
		viewAnimation = armyUnit.transform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(viewAnimation != null, "viewAnimation should not be null");
	}

	public override void OnPush()
	{
		base.OnPush();
		if (viewAnimation.IsPlaying(WALK_ANIMATION))
		{
			viewAnimation.Play(IDLE_ANIMATION);
		}
		else
		{
			viewAnimation.PlayQueued(IDLE_ANIMATION);
		}
	}

	public override bool IsDone()
	{
		return false;
	}
}
