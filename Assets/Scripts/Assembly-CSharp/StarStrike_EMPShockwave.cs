using UnityEngine;

internal class StarStrike_EMPShockwave : MonoBehaviour
{
	private void Start()
	{
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.EMP_EXPLODED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}
}
