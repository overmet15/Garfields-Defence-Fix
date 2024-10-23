using System.Collections;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrade1SceneController : MonoBehaviour
{
	public UIButton menuButton;

	public UIButton continueButton;

	private UserProfileManager up;

	private void Awake()
	{
		up = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		up.setCurrentScene("FD_Upgrade");
	}

	private void Start()
	{
		menuButton.SetValueChangedDelegate(MyDelegate);
		continueButton.SetValueChangedDelegate(MyDelegate);
		Debug.Log("++++++++ UPGRADE 2 ++++++++++");
		//MunerisController.Instance.ReportEvent("menu");
	}

	public IEnumerator WaitAndPlay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		continueButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		menuButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}

	private void MyDelegate(IUIObject obj)
	{
		Debug.Log("++++ Button Clicked! ++++");
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (obj == menuButton)
		{
			menuButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			loadingManagerInstance.setSceneId("FD_TitleScene");
			SceneManager.LoadScene("Loading");
		}
		else if (obj == continueButton)
		{
			Debug.Log("++++ Button Clicked! --> COntinue ++++");
			continueButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			PopupManager.Instance.HidePopup();
			userProfileManagerInstance.setCurrentPlayMode(InGamePlayMode.NORMAL);
			if (userProfileManagerInstance.getGameLevel() == 1)
			{
				loadingManagerInstance.setSceneId("Mini_Game");
			}
			else
			{
				loadingManagerInstance.setSceneId("FD_Preparation");
			}
			SceneManager.LoadScene("Loading");
		}
	}
}
