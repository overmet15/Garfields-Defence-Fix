using UnityEngine;

public class StarStrike_MeteorShower : MonoBehaviour
{
	public GameObject meteorPrefab;

	private float MIN_HORIZONTAL_DISPLACEMENT = 5f;

	private float MAX_HORIZONTAL_DISPLACEMENT = 12f;

	private int batchCount;

	private int meteorsPerBatch;

	private StarStrike_CountdownTimer timeBetweenBatchTimer;

	private int batchDeployed;

	private void Start()
	{
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		StarStrike_ObjectDefinition definition = component.GetDefinition("Meteor");
		batchCount = int.Parse(definition.GetAttributeValue("batchCount"));
		meteorsPerBatch = int.Parse(definition.GetAttributeValue("meteorsPerBatch"));
		float countdownTime = float.Parse(definition.GetAttributeValue("timeBetweenBatch"));
		timeBetweenBatchTimer = new StarStrike_CountdownTimer(countdownTime);
		batchDeployed = 0;
		DeployMeteorBatch();
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.METEOR_SHOWER_USED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.METEOR_SHOWER_OBJECT, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	private void Update()
	{
		if (batchDeployed >= batchCount)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		timeBetweenBatchTimer.Update();
		if (timeBetweenBatchTimer.HasElapsed())
		{
			DeployMeteorBatch();
			timeBetweenBatchTimer.ResetContinue();
		}
	}

	private void DeployMeteorBatch()
	{
		float horizontalDisplacement = Random.Range(MIN_HORIZONTAL_DISPLACEMENT, MAX_HORIZONTAL_DISPLACEMENT);
		StarStrike_EventManager<StarStrike_EventType, StarStrike_AttachmentKey> instance = StarStrike_EventManagerInstance.GetInstance();
		for (int i = 0; i < meteorsPerBatch; i++)
		{
			GameObject gameObject = Object.Instantiate(meteorPrefab);
			StarStrike_Meteor component = gameObject.GetComponent<StarStrike_Meteor>();
			component.Init(horizontalDisplacement);
			StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.METEOR_CREATED);
			starStrike_Event.Attach(StarStrike_AttachmentKey.METEOR_OBJECT, gameObject);
			instance.PostEvent(starStrike_Event);
		}
		batchDeployed++;
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event2 = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.METEOR_SHOWER_BATCH_DEPLOYED);
		starStrike_Event2.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event2);
	}
}
