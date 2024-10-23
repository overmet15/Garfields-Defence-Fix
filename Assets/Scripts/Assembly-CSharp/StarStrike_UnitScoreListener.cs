using UnityEngine;

internal class StarStrike_UnitScoreListener : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private void Start()
	{
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		if (gameEvent.GetEventType() == StarStrike_EventType.UNIT_DESTROYED)
		{
			StarStrike_ArmyUnit attachment = gameEvent.GetAttachment<StarStrike_ArmyUnit>(StarStrike_AttachmentKey.ARMY_UNIT);
			if (attachment.GetOwner() == Owner.TOM)
			{
				Vector3 position = attachment.transform.position;
				position.y += 2f;
			}
		}
	}
}
