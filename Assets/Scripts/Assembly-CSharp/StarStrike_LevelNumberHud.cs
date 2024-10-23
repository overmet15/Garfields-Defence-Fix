using UnityEngine;

public class StarStrike_LevelNumberHud : MonoBehaviour, StarStrike_UiRenderer
{
	public Rect levelTextRect;

	public GUIStyle levelTextStyle;

	public Rect levelNumberRect;

	public GUIStyle levelNumberStyle;

	private StarStrike_LevelManager levelManager;

	private void Start()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.AssertNotNull(levelManager, "levelManager");
	}

	public void RenderUI()
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, StarStrike_Constants.HUD_SCALE);
		GUI.Label(levelTextRect, "Level", levelTextStyle);
		GUI.Label(levelNumberRect, levelManager.GetLevelNumber().ToString(), levelNumberStyle);
	}
}
