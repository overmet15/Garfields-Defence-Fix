using UnityEngine;

public class StarStrike_ScoreHud : MonoBehaviour, StarStrike_UiRenderer
{
	public Rect scoreTextRect;

	public GUIStyle scoreTextStyle;

	private StarStrike_ScoringManager scoringManager;

	private void Start()
	{
		scoringManager = StarStrike_ScoringManager.GetInstance();
	}

	public void RenderUI()
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, StarStrike_Constants.HUD_SCALE);
		GUI.Label(scoreTextRect, scoringManager.GetScore().ToString(), scoreTextStyle);
	}
}
