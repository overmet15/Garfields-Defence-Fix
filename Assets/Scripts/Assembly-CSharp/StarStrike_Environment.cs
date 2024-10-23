using UnityEngine;

public class StarStrike_Environment : MonoBehaviour
{
	public int showOnLevel;

	private int previousLevel;

	private StarStrike_LevelManager levelManager;

	private void Start()
	{
		previousLevel = -1;
		GameObject gameObject = GameObject.Find("LevelManager");
		levelManager = null;
		if (gameObject != null)
		{
			levelManager = gameObject.GetComponent<StarStrike_LevelManager>();
		}
	}

	private void Update()
	{
		if (!(levelManager == null) && previousLevel != levelManager.GetLevelNumber())
		{
			if (showOnLevel == levelManager.GetLevelNumber())
			{
				StarStrike_Utils.ShowObject(base.gameObject);
			}
			else
			{
				StarStrike_Utils.HideObject(base.gameObject);
			}
			previousLevel = levelManager.GetLevelNumber();
		}
	}
}
