using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneController : MonoBehaviour
{
	public Material normalEndingMaterial;

	public Material nightModeEndingMaterial;

	public GameObject normalEnding;

	public GameObject nightModeEnding;

	public SpriteText endGameTitleLbl;

	public SpriteText endGameMsgLbl;

	private void Awake()
	{
		LoadTextures();
	}

	private void LoadTextures()
	{
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		if (userProfileManagerInstance.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			normalEnding.SetActiveRecursivelyLegacy(false);
			Texture2D texture2D = Resources.Load("EndingSceneMaterial_NightMode-" + userProfileManagerInstance.getLangCode()) as Texture2D;
			if (texture2D == null)
			{
				Debug.Log("Texture is Null");
				nightModeEndingMaterial.mainTexture = Resources.Load("EndingSceneMaterial_NightMode") as Texture2D;
			}
			else
			{
				Debug.Log("Texture is NOT Null");
				nightModeEndingMaterial.mainTexture = texture2D;
			}
		}
		else
		{
			nightModeEnding.SetActiveRecursivelyLegacy(false);
			Texture2D texture2D = Resources.Load("EndingSceneMaterial2-" + userProfileManagerInstance.getLangCode()) as Texture2D;
			if (texture2D == null)
			{
				Debug.Log("Texture is Null");
				normalEndingMaterial.mainTexture = Resources.Load("EndingSceneMaterial2") as Texture2D;
			}
			else
			{
				Debug.Log("Texture is NOT Null");
				normalEndingMaterial.mainTexture = texture2D;
			}
		}
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(endGameTitleLbl);
		fontManagerInstance.SetSmallFontMat(endGameMsgLbl);
		fontManagerInstance.SetFontColor(endGameMsgLbl, Color.black, endGameMsgLbl.color);
		endGameTitleLbl.Text = languageManagerInstance.getLangData("endGameTitle");
		if (userProfileManagerInstance.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			endGameMsgLbl.Text = languageManagerInstance.getLangData("endGameMsg_ChallengeMode");
		}
		else
		{
			endGameMsgLbl.Text = languageManagerInstance.getLangData("endGameMsg");
		}
	}

	private void Exit()
	{
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		if (userProfileManagerInstance.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE || userProfileManagerInstance.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
			loadingManagerInstance.setSceneId("FD_Upgrade");
			SceneManager.LoadScene("Loading");
		}
		else
		{
			LoadingManager loadingManagerInstance2 = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
			loadingManagerInstance2.setSceneId("FD_TitleScene");
			SceneManager.LoadScene("Loading");
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Exit();
		}
	}
}
