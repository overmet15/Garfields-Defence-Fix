using UnityEngine;

public class StarStrike_Meteor : MonoBehaviour
{
	private const float STARTING_Y = 11f;

	private const float RIGHT_MOST_X = 16f;

	private const float LEFT_MOST_X = -20f;

	private const float START_MAX_X = -15f;

	private const float MIN_END_X_OFFSET = 1f;

	public GameObject meteorExplosionPrefab;

	private Transform thisTransform;

	private Transform viewTransform;

	private int damage;

	private Vector3 direction;

	private float velocity;

	private float rotationVelocity;

	private StarStrike_GameStateManager gameStateManager;

	private static string GROUND_NAME = "Ground";

	private void Start()
	{
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		StarStrike_ObjectDefinition definition = component.GetDefinition("Meteor");
		damage = int.Parse(definition.GetAttributeValue("damage"));
		thisTransform = base.transform;
		viewTransform = thisTransform.Find("View").transform;
		StarStrike_Assertion.Assert(viewTransform != null, "viewTransform should not be null");
		gameStateManager = StarStrike_GameStateManager.GetInstance();
	}

	public void Init(float horizontalDisplacement)
	{
		float num = Random.Range(-20f, 16f - horizontalDisplacement);
		Vector3 vector = new Vector3(num, 11f, 0f);
		base.transform.position = vector;
		float x = num + horizontalDisplacement;
		Vector3 vector2 = new Vector3(x, 0f, 0f);
		velocity = Random.Range(15, 25);
		rotationVelocity = Random.Range(-720, -360);
		direction = (vector2 - vector).normalized;
	}

	private void Update()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME))
		{
			Vector3 translation = direction * (velocity * Time.deltaTime);
			thisTransform.Translate(translation);
			viewTransform.Rotate(0f, 0f, rotationVelocity * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (StarStrike_Utils.ContainsObjectWithName(other.transform, GROUND_NAME))
		{
			Object.Instantiate(meteorExplosionPrefab, thisTransform.position, Quaternion.identity);
			StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.METEOR_HIT_GROUND);
			StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
			Object.Destroy(base.gameObject);
		}
		StarStrike_ArmyUnit component = other.GetComponent<StarStrike_ArmyUnit>();
		if (component != null && component.GetOwner() == Owner.TOM)
		{
			component.ReceiveDamage(damage);
		}
	}
}
