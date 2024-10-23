using UnityEngine;

internal class StarStrike_DestroyedAction : StarStrike_AbstractAction
{
	private const float FADE_TIME = 1f;

	private static string DESTROYED_ANIMATION = "Destroyed";

	private Transform unitTransform;

	private Animation viewAnimation;

	public StarStrike_DestroyedAction(Transform unitTransform, string actionId)
		: base(actionId)
	{
		this.unitTransform = unitTransform;
		viewAnimation = unitTransform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(viewAnimation != null, "viewAnimation must not be null.");
	}

	public override void OnPush()
	{
		base.OnPush();
		UnmarkAsDone();
		PlayAnimation();
	}

	public override void Update()
	{
		base.Update();
		if (!IsAnimationPlaying())
		{
			MarkAsDone();
			StarStrike_MeshFader starStrike_MeshFader = unitTransform.gameObject.AddComponent<StarStrike_MeshFader>();
			starStrike_MeshFader.SetFadeTime(1f);
		}
	}

	private void PlayAnimation()
	{
		if (!(viewAnimation == null))
		{
			viewAnimation.Play(DESTROYED_ANIMATION);
		}
	}

	private bool IsAnimationPlaying()
	{
		return viewAnimation != null && viewAnimation.IsPlaying(DESTROYED_ANIMATION);
	}
}
