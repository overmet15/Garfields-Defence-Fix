using UnityEngine;

public class StarStrike_EMP : MonoBehaviour
{
	private const float SHOCKWAVE_INTERVAL_TIME = 0.2f;

	private const int NUM_SHOCKWAVES = 3;

	public const float STUN_TIME = 0.6f;

	private StarStrike_CountdownTimer stunTimer;

	private GameObject unitsParent;

	private float stunTime;

	public GameObject empShockwavePrefab;

	private StarStrike_CountdownTimer shockwaveIntervalTimer;

	private int numShockwaveDeployed;

	private static Vector3 SHOCKWAVE_POSITION = new Vector3(0f, 0f, -7.5f);

	private void Start()
	{
		stunTimer = new StarStrike_CountdownTimer(0.6f);
		unitsParent = GameObject.Find("InstantiatedObjectsParent");
		numShockwaveDeployed = 0;
		shockwaveIntervalTimer = new StarStrike_CountdownTimer(0.2f);
		DeployShockwave();
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		StarStrike_ObjectDefinition definition = component.GetDefinition("EMP");
		stunTime = float.Parse(definition.GetAttributeValue("stunTime"));
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.EMP_USED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.EMP_OBJECT, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	private void Update()
	{
		stunTimer.Update();
		if (stunTimer.HasElapsed())
		{
			StunEnemies();
			Object.Destroy(base.gameObject);
		}
		else
		{
			CheckShockwaveDeployment();
		}
	}

	private void CheckShockwaveDeployment()
	{
		if (numShockwaveDeployed < 3)
		{
			if (shockwaveIntervalTimer.HasElapsed())
			{
				DeployShockwave();
			}
			shockwaveIntervalTimer.Update();
		}
	}

	private void DeployShockwave()
	{
		if (numShockwaveDeployed < 3)
		{
			numShockwaveDeployed++;
			shockwaveIntervalTimer.Reset();
			Object.Instantiate(empShockwavePrefab, SHOCKWAVE_POSITION, Quaternion.identity);
		}
	}

	private void StunEnemies()
	{
		StarStrike_ArmyUnit[] componentsInChildren = unitsParent.GetComponentsInChildren<StarStrike_ArmyUnit>();
		StarStrike_ArmyUnit[] array = componentsInChildren;
		foreach (StarStrike_ArmyUnit starStrike_ArmyUnit in array)
		{
			if (starStrike_ArmyUnit.GetOwner() == Owner.TOM)
			{
				starStrike_ArmyUnit.PerformAction(new StarStrike_StunAction(starStrike_ArmyUnit, stunTime, "Stun " + starStrike_ArmyUnit.gameObject.GetInstanceID()));
			}
		}
	}
}
