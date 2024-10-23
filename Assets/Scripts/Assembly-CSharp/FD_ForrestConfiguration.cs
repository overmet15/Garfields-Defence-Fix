using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FD_ForrestConfiguration : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CParseDefinition_003Ec__AnonStorey1C
	{
		internal Dictionary<string, int> sList;

		internal int _003C_003Em__0(string k)
		{
			return sList[k];
		}
	}

	public TextAsset forrestConfigFile;

	private StarStrike_ObjectDefinition forrestDefinition;

	private ArrayList forrestArray;

	private bool definitionsLoaded;

	private void Start()
	{
		ParseDefinition();
	}

	private void ParseDefinition()
	{
		_003CParseDefinition_003Ec__AnonStorey1C _003CParseDefinition_003Ec__AnonStorey1C = new _003CParseDefinition_003Ec__AnonStorey1C();
		if (forrestDefinition != null)
		{
			return;
		}
		forrestDefinition = StarStrike_ObjectDefinition.ParseObject(forrestConfigFile.text);
		forrestArray = new ArrayList();
		_003CParseDefinition_003Ec__AnonStorey1C.sList = new Dictionary<string, int>();
		int num = 0;
		for (int i = 0; i < forrestDefinition.GetTotalChildren(); i++)
		{
			_003CParseDefinition_003Ec__AnonStorey1C.sList.Add(forrestDefinition.GetChildById(i), forrestDefinition.GetIdByIndex(i));
			num++;
		}
		IOrderedEnumerable<string> orderedEnumerable = _003CParseDefinition_003Ec__AnonStorey1C.sList.Keys.OrderBy(_003CParseDefinition_003Ec__AnonStorey1C._003C_003Em__0);
		foreach (string item in orderedEnumerable)
		{
			forrestArray.Add(item);
		}
		definitionsLoaded = true;
	}

	public StarStrike_ObjectDefinition GetDefinition(string forrestName)
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return forrestDefinition.GetChild(forrestName);
	}

	public int GetTotalForrestDefinition()
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return forrestArray.Count;
	}

	public ArrayList GetForrestArray()
	{
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		return forrestArray;
	}

	public StarStrike_ObjectDefinition GetForrestById(int i)
	{
		return forrestDefinition.GetChild(forrestArray[i] as string);
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
		bool flag = int.Parse(definition.GetAttributeValue("needToBuy")) == 1;
		if (flag && num == 0)
		{
			return null;
		}
		if (num >= levelArray.Count)
		{
			num = levelArray.Count - 1;
		}
		return levelArray[num] as FD_ObjectLevelDefinition;
	}

	public int GetSmithCurrentLevel()
	{
		return PlayerPrefs.GetInt("Forrest5_lvl");
	}

	public FD_ObjectLevelDefinition GetSmithPower(int level)
	{
		string forrestName = "05LittleTreePlants";
		if (!definitionsLoaded)
		{
			ParseDefinition();
		}
		StarStrike_ObjectDefinition definition = GetDefinition(forrestName);
		ArrayList levelArray = definition.GetLevelArray("level");
		if (level >= levelArray.Count)
		{
			level = levelArray.Count - 1;
		}
		if (level < 0)
		{
			level = 0;
		}
		return levelArray[level] as FD_ObjectLevelDefinition;
	}
}
