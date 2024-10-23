internal class StarStrike_BlinkingAlphaGenerator
{
	private int blinkCount;

	private StarStrike_CountdownTimer blinkTimer;

	private int currentBlinkCount;

	private bool blinking;

	public StarStrike_BlinkingAlphaGenerator(int blinkCount, float blinkTime)
	{
		this.blinkCount = blinkCount;
		blinkTimer = new StarStrike_CountdownTimer(blinkTime / (float)blinkCount);
		currentBlinkCount = 0;
		blinking = false;
	}

	public void Start()
	{
		currentBlinkCount = 1;
		blinking = true;
		blinkTimer.Reset();
	}

	public void Update()
	{
		if (!blinking)
		{
			return;
		}
		if (blinkTimer.HasElapsed())
		{
			currentBlinkCount++;
			if (currentBlinkCount >= blinkCount)
			{
				blinking = false;
			}
			else
			{
				blinkTimer.ResetContinue();
			}
		}
		blinkTimer.Update();
	}

	public float GetAlpha()
	{
		return 1f - blinkTimer.GetRatio();
	}
}
