using UnityEngine;

internal class StarStrike_HeroIdleAction : StarStrike_AbstractAction
{
	private static string MOVE_ANIMATION = "Idle";

	private Animation animation;

	public StarStrike_HeroIdleAction(Transform unitTransform, string actionId)
		: base(actionId)
	{
		animation = unitTransform.Find("View").GetComponentInChildren<Animation>();
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		PlayMoveAnimation();
	}

	public override void OnReveal()
	{
		PlayMoveAnimation();
	}

	private void PlayMoveAnimation()
	{
		if (animation != null)
		{
			animation.Play(MOVE_ANIMATION);
		}
	}

	public override bool IsDone()
	{
		return false;
	}
}
