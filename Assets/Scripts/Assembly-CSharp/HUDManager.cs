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
#if !UNITY_STANDALONE
				ButtonCollectionAction(num);
				if (!inGameUI.IsPause && movingSFX != null && currentTouch == null)
				{
					movingAudioSource = SingletonMonoBehaviour<AudioManager>.Instance.Play(movingSFX, true);
				}
#endif
                HideTutorial();
				HideStage3Help();
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

#if !UNITY_STANDALONE
			StarStrike_ArmyUnit.Stand();
			if (movingAudioSource != null)
			{
				Object.Destroy(movingAudioSource.gameObject);
			}
#endif
            currentTouch = null;
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
#if UNITY_STANDALONE
	// Code written here is made by @overmet15 (on discord)

	int isDown = -1; // To prevent player from being stuck
	bool isBackUp; // Control fix i guess
    private bool isKeyAPressed = false;
    private bool isKeyDPressed = false;

    private void Update()
    {
        if (inGameUI.IsPause)
        {
			StopMove();
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            isKeyAPressed = true;
            Move(0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isKeyDPressed = true;
            Move(1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isKeyAPressed = false;
            StopMove(); // Stop moving left if A is released.
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isKeyDPressed = false;
            StopMove(); // Stop moving right if D is released.
        }
    }

    private void Move(int i)
    {
        if (isDown != i && isDown != -1)
        {
            StarStrike_ArmyUnit.Stand();
            isBackUp = true;
        }

        if (i == 0 || i == 1)
        {
            ButtonCollectionAction(i);
        }
        else
        {
            Debug.LogError("Value i is wrong, supported are 0 and 1");
            return;
        }

        isDown = i;

        if (!inGameUI.IsPause && movingSFX != null && movingAudioSource == null)
        {
            movingAudioSource = SingletonMonoBehaviour<AudioManager>.Instance.Play(movingSFX, true);
        }
    }

    private void StopMove()
    {
        // Stop movement only if both keys are up.
        if (!isKeyAPressed && !isKeyDPressed)
        {
            StarStrike_ArmyUnit.Stand();

            isDown = -1; // Reset movement state.

            if (movingAudioSource != null)
            {
                Object.Destroy(movingAudioSource.gameObject);
                movingAudioSource = null;
            }
        }
    }
#endif
}
