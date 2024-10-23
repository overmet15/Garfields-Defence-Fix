using UnityEngine;
using UnityEngine.SceneManagement;

public class StarStrike_JerryLoseUi : MonoBehaviour
{
	public GUIStyle skipButtonStyle;

	public Rect skipButtonRect;

	private StarStrike_JerryLose jerryLoseComponent;

	private bool skipped;

	private void Start()
	{
		jerryLoseComponent = GetComponent<StarStrike_JerryLose>();
		StarStrike_Assertion.AssertNotNull(jerryLoseComponent, "jerryLoseComponent");
		skipped = false;
	}

	private void Update()
	{
		if (!skipped && Input.GetKeyUp(KeyCode.Space))
		{
			SkipScene();
		}
	}

	private void OnGUI()
	{
		if (!skipped && GUI.Button(skipButtonRect, string.Empty, skipButtonStyle))
		{
			SkipScene();
		}
	}

	private void SkipScene()
	{
		Time.timeScale = 0f;
		jerryLoseComponent.PushGameOverState();
		skipped = true;
		SceneManager.LoadScene("Mini_Game");
	}
}
