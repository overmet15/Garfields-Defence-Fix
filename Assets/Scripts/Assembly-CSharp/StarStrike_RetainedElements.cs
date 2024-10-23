using UnityEngine;

internal class StarStrike_RetainedElements : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private bool addedAsListener;

	private void Start()
	{
		addedAsListener = false;
	}

	private void OnLevelWasLoaded(int level)
	{
		addedAsListener = false;
	}

	private void Update()
	{
		if (!addedAsListener)
		{
			if (!StarStrike_EventManagerInstance.GetInstance().ContainsListener(this))
			{
				StarStrike_EventManagerInstance.GetInstance().AddListener(this);
			}
			addedAsListener = true;
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.QUIT_CONFIRMED:
		case StarStrike_EventType.GAME_OVER_CONFIRMED:
		case StarStrike_EventType.TOM_LOSE_ANIMATION_ENDED:
			Object.Destroy(base.gameObject);
			break;
		}
	}
}
