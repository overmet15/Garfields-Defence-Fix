using Outblaze;
using UnityEngine;

public class HUDManager : UIViewController
{
	private const float RIGHTMOST_X = 5.4f;

	private const float LEFTMOST_X = -5.8f;

	private UITouch currentTouch;

	private GameObject currentToken;

	private StarStrike_ArmyUnit StarStrike_ArmyUnit;

	public GameObject[] _ButtonCollection;

	public float adjustTokenThreshold;

	public float dragTokenFactor;

	public float dragTokenThreshold;

	public Camera _HUDCam;

	public Camera _MainCam;

	public GameObject _MainCharacter;

	public TutorialManager tutorialManager;

	public AudioClip movingSFX;

	private AudioSource movingAudioSource;

	public InGameUIDemo inGameUI;

	private Vector3? raycastCollider(Collider c, Vector2 screenPos)
	{
		Vector3? result = null;
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		Ray ray2 = _HUDCam.ScreenPointToRay(screenPos);
		RaycastHit hitInfo;
		RaycastHit hitInfo2;
		if (c.Raycast(ray, out hitInfo, 100f))
		{
			result = hitInfo.point;
		}
		else if (c.Raycast(ray2, out hitInfo2, 100f))
		{
			result = hitInfo.point;
		}
		return result;
	}

	public override void OnTouchBegan(UITouch touch)
	{
		int num = 0;
		GameObject[] buttonCollection = _ButtonCollection;
		foreach (GameObject gameObject in buttonCollection)
		{
			if (raycastCollider(gameObject.GetComponent<Collider>(), touch.position).HasValue)
			{
				ButtonCollectionAction(num);
				HideTutorial();
				HideStage3Help();
				if (!inGameUI.IsPause && movingSFX != null && currentTouch == null)
				{
					movingAudioSource = SingletonMonoBehaviour<AudioManager>.Instance.Play(movingSFX, true);
				}
				currentTouch = touch;
				break;
			}
			num++;
		}
	}

	private void HideTutorial()
	{
		if (!tutorialManager.IsShowing)
		{
			return;
		}
		if (tutorialManager.CurrentPanelIndex == 4)
		{
			tutorialManager.ShowNext();
			tutorialManager.Insert(12);
			if (!tutorialManager.IsShowing)
			{
				tutorialManager.ShowNext();
			}
			UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
			userProfileManagerInstance.addItemCount("item01_lvl", 1);
			inGameUI.checkPotionButton();
		}
		if (tutorialManager.CurrentPanelIndex == 1 || tutorialManager.CurrentPanelIndex == 2 || tutorialManager.CurrentPanelIndex == 4 || tutorialManager.CurrentPanelIndex == 11)
		{
			tutorialManager.ShowNext();
		}
		else
		{
			tutorialManager.Remove(1);
		}
	}

	private void HideStage3Help()
	{
		if (inGameUI._showStage3Help)
		{
			Time.timeScale = 1f;
			inGameUI.hideGenHelpPanel();
		}
	}

	public override void OnTouchMoved(UITouch touch)
	{
		if (currentTouch != null && currentTouch.fingerId == touch.fingerId && !(currentToken != null))
		{
		}
	}

	public override void OnTouchEnded(UITouch touch)
	{
		if (currentTouch != null && currentTouch.fingerId == touch.fingerId)
		{
			StarStrike_ArmyUnit.Stand();
			currentTouch = null;
			if (movingAudioSource != null)
			{
				Object.Destroy(movingAudioSource.gameObject);
			}
		}
	}

	private void ButtonCollectionAction(int id)
	{
		switch (id)
		{
		case 0:
			if (StarStrike_ArmyUnit.IsAlive())
			{
				StarStrike_ArmyUnit.MoveBackward();
			}
			break;
		case 1:
			if (StarStrike_ArmyUnit.IsAlive())
			{
				StarStrike_ArmyUnit.MoveForward();
			}
			break;
		case 2:
			if (StarStrike_ArmyUnit.IsAlive())
			{
				StarStrike_ArmyUnit.MoveForward();
			}
			break;
		case 3:
			if (StarStrike_ArmyUnit.IsAlive())
			{
				StarStrike_ArmyUnit.MoveForward();
			}
			break;
		}
	}

	private void MoveCamera(bool dir)
	{
		float x = _MainCam.transform.position.x;
		if (dir)
		{
			if (!(_MainCam.transform.position.x < 5.4f))
			{
			}
		}
		else
		{
			x = _MainCam.transform.position.x - 0.5f;
			x = StarStrike_Utils.Clamp(x, -5.8f, 5.4f);
		}
		Vector3 position = _MainCam.transform.position;
		position.x = x;
		_MainCam.transform.position = position;
	}

	private void Start()
	{
		StarStrike_ArmyUnit = _MainCharacter.GetComponent<StarStrike_ArmyUnit>();
	}
}
