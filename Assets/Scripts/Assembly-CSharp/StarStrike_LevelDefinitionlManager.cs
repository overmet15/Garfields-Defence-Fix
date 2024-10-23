using System.Collections;
using System.Xml;
using Outblaze;
using UnityEngine;

internal class StarStrike_LevelDefinitionlManager
{
	private IList levelList;

	private int levelCursor;

	private KillEnemiesManager _KillEnemiesManager;

	private UserProfileManager _UserProfileManager;

	public StarStrike_LevelDefinitionlManager(string levelXmlTextConfig)
	{
		GameObject gameObject = GameObject.Find("KillEnemiesManager");
		if (gameObject != null)
		{
			_KillEnemiesManager = GameObject.Find("KillEnemiesManager").GetComponent<KillEnemiesManager>();
		}
		gameObject = GameObject.Find("UserProfile");
		if (gameObject != null)
		{
			_UserProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		}
		levelList = new ArrayList();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(levelXmlTextConfig);
		XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("level");
		int num = 0;
		foreach (XmlNode item in elementsByTagName)
		{
			levelList.Add(LoadLevel(item, num));
			num++;
		}
		if (_KillEnemiesManager != null)
		{
			_KillEnemiesManager.SetCurrentEnemiesNum();
			_KillEnemiesManager.SetCurrentLevelAward();
		}
	}

	private StarStrike_LevelDefinition LoadLevel(XmlNode levelNode, int levelNum)
	{
		float waveTimeInterval = float.Parse(levelNode.Attributes.GetNamedItem("waveTimeInterval").Value);
		int totalEnemies = int.Parse(levelNode.Attributes.GetNamedItem("count").Value);
		int award = int.Parse(levelNode.Attributes.GetNamedItem("award").Value);
		int maxWater = int.Parse(levelNode.Attributes.GetNamedItem("MaxWater").Value);
		int maxSun = int.Parse(levelNode.Attributes.GetNamedItem("MaxSun").Value);
		StarStrike_LevelDefinition starStrike_LevelDefinition = new StarStrike_LevelDefinition(waveTimeInterval);
		if (_KillEnemiesManager != null)
		{
			_KillEnemiesManager.GetTotalEnemies(totalEnemies, levelNum);
			_KillEnemiesManager.GetLevelAwards(award, levelNum);
			_KillEnemiesManager.GetMaxSunRewards(maxSun, levelNum);
			_KillEnemiesManager.GetMaxWaterRewards(maxWater, levelNum);
		}
		foreach (XmlNode item in levelNode)
		{
			if ("wave".Equals(item.Name))
			{
				starStrike_LevelDefinition.AddWave(LoadWave(item));
			}
			if ("enable".Equals(item.Name))
			{
				string value = item.Attributes.GetNamedItem("name").Value;
				starStrike_LevelDefinition.AddEnabledItem(value);
			}
		}
		return starStrike_LevelDefinition;
	}

	private StarStrike_WaveDefinition LoadWave(XmlNode waveNode)
	{
		StarStrike_WaveDefinition starStrike_WaveDefinition = new StarStrike_WaveDefinition();
		foreach (XmlNode item in waveNode)
		{
			if ("army".Equals(item.Name))
			{
				starStrike_WaveDefinition.AddArmy(LoadArmy(item));
			}
		}
		StarStrike_Assertion.Assert(starStrike_WaveDefinition.ArmyCount() > 0, "Wave should have at least one army entry.");
		return starStrike_WaveDefinition;
	}

	private StarStrike_ArmyDefinition LoadArmy(XmlNode armyNode)
	{
		XmlAttributeCollection attributes = armyNode.Attributes;
		int model = int.Parse(attributes.GetNamedItem("model").Value);
		int count = int.Parse(attributes.GetNamedItem("count").Value);
		return new StarStrike_ArmyDefinition(model, count);
	}

	public bool HasNextLevel()
	{
		return levelCursor < levelList.Count;
	}

	public StarStrike_LevelDefinition MoveToNextLevel()
	{
		_UserProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			levelCursor = _UserProfileManager.getGameLevel_NMode() - 1;
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			levelCursor = _UserProfileManager.getGameLevel_HalloweenMode() - 1;
		}
		else
		{
			levelCursor = _UserProfileManager.getGameLevel() - 1;
		}
		StarStrike_LevelDefinition result = (StarStrike_LevelDefinition)levelList[levelCursor];
		levelCursor++;
		return result;
	}

	public void ResetIterator()
	{
		levelCursor = 0;
	}
}
