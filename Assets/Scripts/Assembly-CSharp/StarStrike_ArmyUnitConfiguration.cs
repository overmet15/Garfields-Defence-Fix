using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StarStrike_ArmyUnitConfiguration : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CLoadUnitDefinitions_003Ec__AnonStorey1D
	{
		internal Dictionary<string, int> sList;

		internal int _003C_003Em__1(string k)
		{
			return sList[k];
		}
	}

	public TextAsset unitDefinitionXmlConfig;

	private bool definitionsLoaded;

	private StarStrike_ObjectDefinition rootDefinition;

	private ArrayList goodGuy;

	private void LoadUnitDefinitions()
	{
		_003CLoadUnitDefinitions_003Ec__AnonStorey1D _003CLoadUnitDefinitions_003Ec__AnonStorey1D = new _003CLoadUnitDefinitions_003Ec__AnonStorey1D();
		rootDefinition = StarStrike_ObjectDefinition.ParseObject(unitDefinitionXmlConfig.text);
		definitionsLoaded = true;
		Debug.Log("LoadUnitDefinition: " + rootDefinition.GetTotalChildren());
		goodGuy = new ArrayList();
		_003CLoadUnitDefinitions_003Ec__AnonStorey1D.sList = new Dictionary<string, int>();
		int num = 0;
		for (int i = 0; i < rootDefinition.GetTotalChildren(); i++)
		{
			StarStrike_ObjectDefinition child = rootDefinition.GetChild(rootDefinition.GetChildById(i));
			if (child.GetAttributeValue("team").Equals("GOOD"))
			{
				_003CLoadUnitDefinitions_003Ec__AnonStorey1D.sList.Add(rootDefinition.GetChildById(i), rootDefinition.GetIdByIndex(i));
				num++;
			}
		}
		IOrderedEnumerable<string> orderedEnumerable = _003CLoadUnitDefinitions_003Ec__AnonStorey1D.sList.Keys.OrderBy(_003CLoadUnitDefinitions_003Ec__AnonStorey1D._003C_003Em__1);
		foreach (string item in orderedEnumerable)
		{
			goodGuy.Add(item);
		}
	}

	public StarStrike_ObjectDefinition GetUnitDefinition(string unitName)
	{
		if (!definitionsLoaded)
		{
			LoadUnitDefinitions();
		}
		return rootDefinition.GetChild(unitName);
	}

	public int GetTotalUnitDefinition()
	{
		if (!definitionsLoaded)
		{
			LoadUnitDefinitions();
		}
		return goodGuy.Count;
	}

	public ArrayList GetGoodGuy()
	{
		if (!definitionsLoaded)
		{
			LoadUnitDefinitions();
		}
		return goodGuy;
	}

	public StarStrike_ObjectDefinition GetUnitById(int i)
	{
		return rootDefinition.GetChild(goodGuy[i] as string);
	}

	public FD_ObjectLevelDefinition GetCurrentLevel(string unitName)
	{
		if (!definitionsLoaded)
		{
			LoadUnitDefinitions();
		}
		StarStrike_ObjectDefinition unitDefinition = GetUnitDefinition(unitName);
		ArrayList levelArray = unitDefinition.GetLevelArray("level");
		int num = (unitDefinition.HasAttribute("key") ? (PlayerPrefs.HasKey(unitDefinition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(unitDefinition.GetAttributeValue("key")) : 0) : 0);
		if (num >= levelArray.Count)
		{
			num = levelArray.Count - 1;
		}
		return levelArray[num] as FD_ObjectLevelDefinition;
	}
}
