using UnityEngine;

public class StarStrike_GameConfiguration : MonoBehaviour
{
	public TextAsset gameConfigXml;

	private StarStrike_ObjectDefinition gameConfigDefinition;

	public string GetAttribute(string name)
	{
		LoadConfig();
		return gameConfigDefinition.GetAttributeValue(name);
	}

	private void LoadConfig()
	{
		if (gameConfigDefinition == null)
		{
			gameConfigDefinition = StarStrike_ObjectDefinition.ParseObject(gameConfigXml.text);
		}
	}
}
