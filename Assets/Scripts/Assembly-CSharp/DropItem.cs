using Outblaze;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	private StarStrike_MineralProducer mineralProducer;

	private DropItemsManager _DropItemManager;

	private bool picked;

	public int DropType;

	public AudioClip pickSFX;

	public Animation animation;

	private void Start()
	{
		mineralProducer = GameObject.Find("MineralProducer").GetComponent<StarStrike_MineralProducer>();
		_DropItemManager = GameObject.Find("DropItemsManager").GetComponent<DropItemsManager>();
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (picked)
		{
			return;
		}
		StarStrike_HeroRangeCollider starStrike_HeroRangeCollider = StarStrike_Utils.FindComponentThroughParent<StarStrike_HeroRangeCollider>(otherCollider.transform);
		if (starStrike_HeroRangeCollider != null)
		{
			return;
		}
		StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
		if (starStrike_ArmyUnit != null && starStrike_ArmyUnit.GetUnitType() == UnitType.HERO)
		{
			if (DropType == 1 || DropType == 3)
			{
				Debug.Log("*****Get Sun Power*******");
				_DropItemManager.AddSunPower();
			}
			else
			{
				Debug.Log("*****Get Water Power*******");
				mineralProducer.AddMinerals(1);
			}
			if (pickSFX != null)
			{
				SingletonMonoBehaviour<AudioManager>.Instance.Play(pickSFX);
			}
			checkTutorial();
			Pick();
		}
	}

	private void Pick()
	{
		picked = true;
		if (animation.IsPlaying("Drop"))
		{
			animation.PlayQueued("Get");
		}
		else
		{
			animation.Play("Get");
		}
	}

	private void SelfDestroy()
	{
		checkTutorial();
		Object.Destroy(base.transform.parent.gameObject);
	}

	private void checkTutorial()
	{
		int num = _DropItemManager.returnDropTutorial();
		if (num != 0)
		{
			switch (num)
			{
			case 1:
				_DropItemManager.TriggerTutorial2();
				break;
			case 2:
				_DropItemManager.FinishDropTutorial();
				break;
			default:
				_DropItemManager.FinishDropTutorial();
				break;
			}
		}
	}

	public void SetDropType(int t)
	{
		DropType = t;
		if (DropType < 2)
		{
			Debug.Log("Invoke SelfDestroy");
			Invoke("SelfDestroy", 5f);
		}
	}

	private void Update()
	{
		if (animation.IsPlaying("Drop"))
		{
			return;
		}
		if (picked)
		{
			if (!animation.IsPlaying("Get"))
			{
				SelfDestroy();
			}
			else if (iTween.Count(base.transform.parent.gameObject) <= 0)
			{
				iTween.FadeTo(base.transform.parent.gameObject, 0f, animation.GetClip("Get").length);
			}
		}
		else if (!animation.IsPlaying("Idle"))
		{
			animation.Play("Idle");
		}
	}
}
