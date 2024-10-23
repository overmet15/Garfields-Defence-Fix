using UnityEngine;

public class StarStrike_LaserEmitter : MonoBehaviour
{
	public GameObject laserBallPrefab;

	private Transform emitterTransform;

	private void Start()
	{
		emitterTransform = StarStrike_Utils.FindTransformByName(base.transform, "emitter");
		StarStrike_Assertion.Assert(emitterTransform != null, "emitterTransform must not be null.");
	}

	public void EmitLaser(Transform target, int damage, int OwnerID)
	{
		EmitLaser(target, damage, OwnerID, false);
	}

	public void EmitLaser(Transform target, int damage, int OwnerID, bool hidden)
	{
		Vector3 position = emitterTransform.position;
		position.z = 0f;
		GameObject gameObject = (GameObject)Object.Instantiate(laserBallPrefab, position, Quaternion.identity);
		StarStrike_LaserBallProjectileMotion component = gameObject.GetComponent<StarStrike_LaserBallProjectileMotion>();
		component.SetTarget(target);
		if (hidden)
		{
			gameObject.transform.Find("View").gameObject.SetActiveRecursivelyLegacy(false);
		}
		StarStrike_LaserBall component2 = gameObject.GetComponent<StarStrike_LaserBall>();
		component2.SetDamage(damage);
		component2.SetOwnerID(OwnerID);
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.LASER_BALL_FIRED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, emitterTransform.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}
}
