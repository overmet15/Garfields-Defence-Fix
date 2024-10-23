using UnityEngine;

internal class StarStrike_TimedWaitAction : StarStrike_AbstractAction
{
	private StarStrike_CountdownTimer waitTimer;

	private Animation viewAnimation;

	private static string IDLE_ANIMATION = "Idle";

	public StarStrike_TimedWaitAction(Transform unitTransform, float waitTime, string actionId)
		: base(actionId)
	{
		waitTimer = new StarStrike_CountdownTimer(waitTime);
		viewAnimation = unitTransform.Find("View").GetComponentInChildren<Animation>();
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		StartWait();
	}

	public override void OnReveal()
	{
		base.OnReveal();
		StartWait();
	}

	private void StartWait()
	{
		waitTimer.Reset();
		if (viewAnimation != null)
		{
			viewAnimation.Play(IDLE_ANIMATION);
		}
	}

	public override void Update()
	{
		waitTimer.Update();
		if (waitTimer.HasElapsed())
		{
			MarkAsDone();
		}
	}
}
