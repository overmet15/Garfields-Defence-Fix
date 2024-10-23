using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StarStrike_SpecialAttackConfiguration : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CParseDefinition_003Ec__AnonStorey1E
	{
		internal Dictionary<string, int> sList;

		internal int _003C_003Em__2(string k)
		{
			return sList[k];
		}
	}

	public TextAsset specialAttackConfigFile;

	private StarStrike_ObjectDefinition specialAttackDefinition;

	private ArrayList specialAttackArray;

	private bool definitionsLoaded;

	private void Start()
	{
		ParseDefinition();
	}

	private void ParseDefinition()
	{
		_003CParseDefinition_003Ec__AnonStorey1E _003CParseDefinition_003Ec__AnonStorey1E = new _003CParseDefinition_003Ec__AnonStorey1E();
		if (specialAttackDefinition != null)
		{
			return;
		}
		specialAttackDefinition = StarStrike_ObjectDefinition.ParseObject(specialAttackConfigFile.text);
		specialAttackArray = new ArrayList();
		_003CParseDefinition_003Ec__AnonStorey1E.sList = new Dictionary<string, int>();
		int num = 0;
		for (int i = 0; i < specialAttackDefinition.GetTotalChildren(); i++)
		{
			_003CParseDefinition_003Ec__AnonStorey1E.sList.Add(specialAttackDefinition.GetChildById(i), specialAttackDefinition.GetIdByIndex(i));
			num++;
		}
		IOrderedEnumerable<string> orderedEnumerable = _003CParseDefinition_003Ec__AnonStorey1E.sList.Keys.OrderBy(_003CParseDefinition_003Ec__AnonStorey1E._003C_003Em__2);
		foreach (string item in orderedEnumerable)
		{
			specialAttackArray.Add(item);
		}
		definitionsLoaded = true;
	}

	public StarStrike_ObjectDefinition GetDefinition(string specialAttackName)
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return specialAttackDefinition.GetChild(specialAttackName);
	}

	public int GetTotalSpecialAttackDefinition()
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return specialAttackArray.Count;
	}

	public ArrayList GetSpecialAttackArray()
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return specialAttackArray;
	}

	public StarStrike_ObjectDefinition GetSpecialAttackById(int i)
	{
		return specialAttackDefinition.GetChild(specialAttackArray[i] as string);
	}

	public FD_ObjectLevelDefinition GetCurrentLevel(string unitName)
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		StarStrike_ObjectDefinition definition = GetDefinition(unitName);
		ArrayList levelArray = definition.GetLevelArray("level");
		int num = (definition.HasAttribute("key") ? (PlayerPrefs.HasKey(definition.GetAttributeValue("key")) ? PlayerPrefs.GetInt(definition.GetAttributeValue("key")) : 0) : 0);
		if (num >= levelArray.Count)
		{
			num = levelArray.Count - 1;
		}
		return levelArray[num] as FD_ObjectLevelDefinition;
	}
}
