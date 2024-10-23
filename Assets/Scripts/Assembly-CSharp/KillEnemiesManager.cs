using Outblaze;
using UnityEngine;

public class KillEnemiesManager : MonoBehaviour
{
	private int[] _TotalEnemiesNum;

	private int[] _LevelAward;

	private int[] _MaxSunNum;

	private int[] _MaxWaterNum;

	private UserProfileManager _UserProfileManager;

	private int _CurrentTotalEnemiesNum;

	private int _TotalEnemies;

	private int _CurrentLevelAward;

	private int _GiveWaterReward;

	private int _GiveSunReward;

	public GameObject EZGUI;

	private InGameUIDemo _InGameUIDemo;

	private void Start()
	{
		_UserProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		_InGameUIDemo = EZGUI.transform.GetComponent<InGameUIDemo>();
		int num = ((_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE) ? _UserProfileManager.getMaxLevel_NMode() : ((_UserProfileManager.getCurrentPlayMode() != InGamePlayMode.HALLOWEEN) ? _UserProfileManager.getMaxLevel() : _UserProfileManager.getMaxLevel_HalloweenMode()));
		_TotalEnemiesNum = new int[num];
		_LevelAward = new int[num];
		_MaxSunNum = new int[num];
		_MaxWaterNum = new int[num];
	}

	public void GetTotalEnemies(int totalEnemies, int level)
	{
		_TotalEnemiesNum[level] = totalEnemies;
	}

	public void GetLevelAwards(int Award, int level)
	{
		_LevelAward[level] = Award;
	}

	public void GetMaxSunRewards(int MaxSun, int level)
	{
		_MaxSunNum[level] = MaxSun;
	}

	public void GetMaxWaterRewards(int MaxWater, int level)
	{
		_MaxWaterNum[level] = MaxWater;
	}

	public int ReturnWaterNum(int level)
	{
		return _MaxWaterNum[level];
	}

	public int ReturnSunNum(int level)
	{
		return _MaxSunNum[level];
	}

	public bool CanDropSun(int sunReward)
	{
		int num;
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			num = _UserProfileManager.getGameLevel_NMode();
			if (num <= _UserProfileManager.getFinishedLevel_NMode())
			{
				return false;
			}
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			num = _UserProfileManager.getGameLevel_HalloweenMode();
			if (num <= _UserProfileManager.getFinishedLevel_HalloweenMode())
			{
				return false;
			}
		}
		else
		{
			num = _UserProfileManager.getGameLevel();
		}
		int num2 = _MaxSunNum[num - 1];
		_GiveSunReward += sunReward;
		if (_GiveSunReward > num2)
		{
			_GiveSunReward -= sunReward;
			return false;
		}
		return true;
	}

	public bool CanDropWater(int waterReward)
	{
		int num = ((_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE) ? _UserProfileManager.getGameLevel_NMode() : ((_UserProfileManager.getCurrentPlayMode() != InGamePlayMode.HALLOWEEN) ? _UserProfileManager.getGameLevel() : _UserProfileManager.getGameLevel_HalloweenMode()));
		int num2 = _MaxWaterNum[num - 1];
		_GiveWaterReward += waterReward;
		if (_GiveWaterReward > num2)
		{
			_GiveWaterReward -= waterReward;
			return false;
		}
		return true;
	}

	public void SetCurrentEnemiesNum()
	{
		int num = ((_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE) ? _UserProfileManager.getGameLevel_NMode() : ((_UserProfileManager.getCurrentPlayMode() != InGamePlayMode.HALLOWEEN) ? _UserProfileManager.getGameLevel() : _UserProfileManager.getGameLevel_HalloweenMode()));
		_CurrentTotalEnemiesNum = _TotalEnemiesNum[num - 1];
		_TotalEnemies = _CurrentTotalEnemiesNum;
	}

	public void SetCurrentLevelAward()
	{
		int num;
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			num = _UserProfileManager.getGameLevel_NMode();
			if (num <= _UserProfileManager.getFinishedLevel_NMode())
			{
				return;
			}
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			num = _UserProfileManager.getGameLevel_HalloweenMode();
			if (num <= _UserProfileManager.getFinishedLevel_HalloweenMode())
			{
				return;
			}
		}
		else
		{
			num = _UserProfileManager.getGameLevel();
		}
		_CurrentLevelAward = _LevelAward[num - 1];
		Debug.Log("********_CurrentLevelAward: " + num);
	}

	public int ReturnAward()
	{
		return _CurrentLevelAward;
	}

	public void KillEnemy()
	{
		_CurrentTotalEnemiesNum--;
		float progress = float.Parse(_CurrentTotalEnemiesNum.ToString()) / float.Parse(_TotalEnemies.ToString());
		_InGameUIDemo.UpdateProgressBar(progress);
		Debug.Log("_CurrentTotalEnemiesNum " + _CurrentTotalEnemiesNum);
		if (_CurrentTotalEnemiesNum <= 0)
		{
			PostOnDestroyEvent();
		}
	}

	private void PostOnDestroyEvent()
	{
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = null;
		starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.TOM_BASE_DESTROYED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}
}
