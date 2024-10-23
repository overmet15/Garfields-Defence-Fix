using UnityEngine;

public class StarStrike_BaseDestroyedListener : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	public Texture damagedTexture;

	private Texture undamagedTexture;

	public Renderer baseRenderer;

	public GameObject explosionPrefab;

	public StarStrike_EventType triggerEvent;

	private void Start()
	{
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		if (gameEvent.GetEventType() != triggerEvent && gameEvent.GetEventType() != StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED)
		{
		}
	}
}
