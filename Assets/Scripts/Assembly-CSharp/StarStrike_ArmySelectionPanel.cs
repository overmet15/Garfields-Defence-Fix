using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarStrike_ArmySelectionPanel : MonoBehaviour
{
	private const float SPRING_FACTOR = 2.5f;

	private const float NEXT_PAGE_OFFSET = 35f;

	private const float SPACING = 10f;

	private const float PANEL_Y = 86f;

	private const float BUTTON_Y = 300f;

	private const float SLIDING_TIME = 3f;

	public Texture[] panelImages;

	private ArrayList panels;

	private Vector2 refPoint;

	private Vector2 targetPoint;

	public GUIStyle nextButton;

	public GUIStyle backButton;

	public GUIStyle goButton;

	private Rect nextRect;

	private Rect backRect;

	private Rect goRect;

	private StarStrike_CountdownTimer animTimer;

	private bool isSliding;

	private bool isGoingLeft;

	public AudioClip buttonClickSound;

	private void Awake()
	{
		panels = new ArrayList();
		for (int i = 0; i < panelImages.Length; i++)
		{
			Texture texture = panelImages[i];
			float num = (float)(texture.width * i) + 10f * (float)(i + 1) + 35f * (float)(i / 3);
			num = num / 800f * (float)Screen.width;
			Rect rect = new Rect(num, 86f, texture.width, texture.height);
			panels.Add(new StarStrike_InstructionPanel(texture, rect));
		}
		float num2 = nextButton.normal.background.width;
		float height = nextButton.normal.background.height;
		nextRect = new Rect((float)Screen.width - num2, 300f, num2, height);
		float width = backButton.normal.background.width;
		float height2 = backButton.normal.background.height;
		backRect = new Rect(10f, 300f, width, height2);
		float num3 = goButton.normal.background.width;
		float height3 = goButton.normal.background.height;
		goRect = new Rect((float)(Screen.width / 2) - num3 / 2f, 600f, num3, height3);
		refPoint = new Vector2(0f, 0f);
		targetPoint = new Vector2(0f, 0f);
		animTimer = new StarStrike_CountdownTimer(3f);
		isSliding = false;
	}

	private void Start()
	{
		StarStrike_ScoringManager.DeleteInstance();
		StarStrike_EventManagerInstance.DeleteInstance();
		StarStrike_ArmySingletons.DeleteInstance();
		StarStrike_GameStateManager.DeleteInstance();
	}

	private void OnGUI()
	{
		updateRefpoint();
		foreach (StarStrike_InstructionPanel panel in panels)
		{
			panel.setRefPoint(refPoint);
			GUI.DrawTexture(panel.getDisplayRect(), panel.getImage());
		}
		if (GUI.Button(goRect, string.Empty, goButton))
		{
			SceneManager.LoadScene("Mini_Game");
			return;
		}
		if (targetPoint.x >= 0f && GUI.Button(nextRect, string.Empty, nextButton))
		{
			targetPoint.x -= Screen.width;
			animTimer.Reset();
			isSliding = true;
			isGoingLeft = false;
			MinigameAudioManager.PlayClipAtPoint(buttonClickSound, base.transform.position);
		}
		if (targetPoint.x < 0f && GUI.Button(backRect, string.Empty, backButton))
		{
			targetPoint.x += Screen.width;
			animTimer.Reset();
			isSliding = true;
			isGoingLeft = true;
			MinigameAudioManager.PlayClipAtPoint(buttonClickSound, base.transform.position);
		}
	}

	private void updateRefpoint()
	{
		if (isSliding)
		{
			animTimer.Update();
			if (animTimer.HasElapsed())
			{
				refPoint.x = targetPoint.x;
				isSliding = false;
			}
			else
			{
				float ratio = animTimer.GetRatio();
				refPoint.x = StarStrike_InterpolationUtils.GetParabolicPos(ratio, refPoint.x, targetPoint.x, 2.5f * (float)((!isGoingLeft) ? 1 : (-1)));
			}
		}
	}
}
