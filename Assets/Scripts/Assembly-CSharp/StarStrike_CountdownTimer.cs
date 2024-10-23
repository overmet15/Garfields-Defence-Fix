using UnityEngine;

internal class StarStrike_CountdownTimer
{
	private float polledTime;

	private float countdownTime;

	public StarStrike_CountdownTimer(float countdownTime)
	{
		StarStrike_Assertion.Assert(countdownTime > 0f, "The specified time must be greater than zero.");
		polledTime = 0f;
		this.countdownTime = countdownTime;
	}

	public void Update()
	{
		polledTime += Time.deltaTime;
	}

	public void Reset()
	{
		polledTime = 0f;
	}

	public void ResetContinue()
	{
		while (polledTime > countdownTime)
		{
			polledTime -= countdownTime;
		}
	}

	public bool HasElapsed()
	{
		return StarStrike_Comparison.TolerantGreaterThanOrEquals(polledTime, countdownTime);
	}

	public float GetRatio()
	{
		float value = polledTime / countdownTime;
		return StarStrike_Utils.Clamp(value, 0f, 1f);
	}
}
