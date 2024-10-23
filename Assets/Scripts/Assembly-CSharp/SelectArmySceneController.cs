using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectArmySceneController : MonoBehaviour
{
	public UIButton continueButton;

	public UIButton backButton;

	private void Start()
	{
		continueButton.SetValueChangedDelegate(MyDelegate);
		backButton.SetValueChangedDelegate(MyDelegate);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape) && PopupManager.Instance.isPopping())
		{
			PopupManager.Instance.HidePopup();
		}
	}

	private void MyDelegate(IUIObject obj)
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		Debug.Log("lvl_obj: " + loadingManagerInstance.GetSceneId());
		if (obj == continueButton)
		{
			loadingManagerInstance.setSceneId("Mini_Game");
			SceneManager.LoadScene("Loading");
		}
		else if (obj == backButton)
		{
			loadingManagerInstance.setSceneId("FD_Upgrade");
			SceneManager.LoadScene("Loading");
		}
	}
}
