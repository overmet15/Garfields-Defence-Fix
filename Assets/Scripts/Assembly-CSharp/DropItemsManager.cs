using Outblaze;
using UnityEngine;

public class DropItemsManager : MonoBehaviour
{
	public int SunPowerValue = 1;

	private int SunPower;

	private int PickedScore;

	private int CurrentLevel;

	private int Tutorial;

	public GameObject _DropResource;

	public GameObject _SunResource;

	public GameObject _WaterResource;

	public AudioClip SunPickSFX;

	public AudioClip WaterPickSFX;

	private DropItem _DropItem;

	private StarStrike_ScoringManager scoringManager;

	public TutorialManager tutorialManager;

	private void Start()
	{
		scoringManager = StarStrike_ScoringManager.GetInstance();
	}

	public void DropItem(Vector3 dropPosition, int dropNum, bool sunPower)
	{
		if (sunPower)
		{
			Debug.Log("+++++Drop SunPower====>" + dropNum);
			_DropResource = _SunResource;
		}
		else
		{
			Debug.Log("+++++Drop WaterPower====>" + dropNum);
			_DropResource = _WaterResource;
		}
		for (int i = 0; i < dropNum; i++)
		{
			dropPosition.z = -15f;
			GameObject gameObject = (GameObject)Object.Instantiate(_DropResource, dropPosition, Quaternion.identity);
			DropItem componentInChildren = gameObject.GetComponentInChildren<DropItem>();
			Debug.Log("DropItem ==> " + componentInChildren);
			if (sunPower)
			{
				componentInChildren.SetDropType(1);
				componentInChildren.pickSFX = SunPickSFX;
			}
			else
			{
				componentInChildren.SetDropType(0);
				componentInChildren.pickSFX = WaterPickSFX;
			}
			dropPosition.y += 0.2f;
			dropPosition.x += 1f;
		}
	}

	public void dropTutorialSunPower()
	{
		Vector3 position = base.transform.position;
		position.x += 2f;
		position.y -= 1.5f;
		GameObject gameObject = (GameObject)Object.Instantiate(_SunResource, position, Quaternion.identity);
		DropItem componentInChildren = gameObject.GetComponentInChildren<DropItem>();
		componentInChildren.SetDropType(3);
		componentInChildren.pickSFX = SunPickSFX;
	}

	public void dropTutorialWaterPower()
	{
		Vector3 position = base.transform.position;
		position.x -= 10f;
		position.y -= 1.5f;
		GameObject gameObject = (GameObject)Object.Instantiate(_WaterResource, position, Quaternion.identity);
		DropItem componentInChildren = gameObject.GetComponentInChildren<DropItem>();
		componentInChildren.SetDropType(4);
		componentInChildren.pickSFX = WaterPickSFX;
	}

	public void AddSunPower()
	{
		SunPower++;
	}

	public int ReturnSunPower()
	{
		int num = CurrentLevel - 3;
		if (num < 0)
		{
			num = 0;
		}
		return SunPower * (1 + num / 3);
	}

	public void AddScore()
	{
		PickedScore++;
	}

	public int ReturnScore()
	{
		PickedScore = scoringManager.GetScore();
		return PickedScore;
	}

	public void GetCurrentLevel(int level)
	{
		CurrentLevel = level;
		if (CurrentLevel == 3)
		{
			TriggerTutorial1();
		}
	}

	private void TriggerTutorial1()
	{
		Tutorial = 1;
		dropTutorialWaterPower();
	}

	public void TriggerTutorial2()
	{
		Tutorial = 2;
		tutorialManager.ShowNext();
		dropTutorialSunPower();
	}

	public void FinishDropTutorial()
	{
		Tutorial = 0;
		tutorialManager.ShowNext();
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		userProfileManagerInstance.addItemCount("item03_lvl", 1);
		InGameUIDemo component = GameObject.Find("In Game EZGUI").GetComponent<InGameUIDemo>();
		component.checkPotionButton();
	}

	public bool CanDropItem()
	{
		if (CurrentLevel > 2)
		{
			return true;
		}
		return false;
	}

	public int returnDropTutorial()
	{
		return Tutorial;
	}
}
