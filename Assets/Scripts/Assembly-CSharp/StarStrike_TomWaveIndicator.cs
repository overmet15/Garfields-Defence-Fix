using UnityEngine;

public class StarStrike_TomWaveIndicator : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const int BLINK_COUNT = 6;

	private const float BLINK_TIME = 3f;

	public Texture icon;

	public Rect iconRect;

	private bool displayed;

	private StarStrike_CountdownTimer displayTimer;

	private StarStrike_BlinkingAlphaGenerator alphaGenerator;

	private void Start()
	{
		displayed = false;
		displayTimer = new StarStrike_CountdownTimer(3f);
		alphaGenerator = new StarStrike_BlinkingAlphaGenerator(6, 3f);
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		if (displayed)
		{
			if (displayTimer.HasElapsed())
			{
				displayed = false;
				return;
			}
			displayTimer.Update();
			alphaGenerator.Update();
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_EventType eventType = gameEvent.GetEventType();
		if (eventType == StarStrike_EventType.SPAWN_WAVE)
		{
			displayed = true;
			displayTimer.Reset();
			alphaGenerator.Start();
		}
	}
}
