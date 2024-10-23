using UnityEngine;

public class StarStrike_RocketCannon : MonoBehaviour
{
	public GameObject rocketGlovePrefab;

	private Animation selfAnimation;

	private Animation cannonAnimation;

	private static string FIRE_ANIMATION = "RocketCannonFire";

	private void Start()
	{
		cannonAnimation = base.transform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(cannonAnimation != null, "cannonANimation should not be null");
		cannonAnimation.Play("InitialState");
		selfAnimation = GetComponent<Animation>();
		StarStrike_Assertion.Assert(selfAnimation != null, "selfAnimation should not be null");
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.ROCKET_CANNON_USED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.ROCKET_CANNON_OBJECT, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	private void Update()
	{
		if (!selfAnimation.IsPlaying(FIRE_ANIMATION))
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void PlayOpenAnimation()
	{
		cannonAnimation.Play("Open");
	}

	public void InstantiateGlove()
	{
		Transform transform = base.transform.Find("RocketGlovePosition");
		StarStrike_Assertion.Assert(transform != null, "rocketGlovePositionTransform should not be null");
		Object.Instantiate(rocketGlovePrefab, transform.position, rocketGlovePrefab.transform.rotation);
	}
}
