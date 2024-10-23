using UnityEngine;

internal class StarStrike_DeadAction : StarStrike_AbstractAction
{
	private Animation viewAnimation;

	public StarStrike_DeadAction(Transform unitTransform, string actionId)
		: base(actionId)
	{
		viewAnimation = unitTransform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(viewAnimation != null, "viewAnimation must not be null.");
	}

	public override void OnPush()
	{
		base.OnPush();
		UnmarkAsDone();
		viewAnimation.Stop();
	}

	public override bool IsDone()
	{
		return false;
	}
}
