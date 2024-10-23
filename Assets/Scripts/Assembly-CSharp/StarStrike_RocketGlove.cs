using UnityEngine;

public class StarStrike_RocketGlove : MonoBehaviour
{
	private const float X_POS_DESTROY = 20f;

	public float velocity;

	public int damage = 20;

	private Transform thisTransform;

	private void Start()
	{
		thisTransform = base.transform;
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.ROCKET_GLOVE_FIRED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.ROCKET_GLOVE_OBJECT, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	private void Update()
	{
		thisTransform.Translate(thisTransform.forward * (velocity * Time.deltaTime), Space.World);
		if (StarStrike_Comparison.TolerantGreaterThanOrEquals(thisTransform.position.x, 20f))
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		StarStrike_ArmyUnit component = otherCollider.GetComponent<StarStrike_ArmyUnit>();
		if (!(component == null) && component.GetOwner() == Owner.TOM)
		{
			component.ReceiveDamage(damage);
			StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.ROCKET_GLOVE_HIT);
			starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
			StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
		}
	}
}
