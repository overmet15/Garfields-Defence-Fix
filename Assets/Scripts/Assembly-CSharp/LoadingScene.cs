using System;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
	public UIPanel[] randomIcons;

	public UIButton gameTips;

	public AudioClip backgroundMusic;

	public UIButton badGuyDescription;

	public SpriteText badGuyTxt;

	public SpriteText loadingLbl;

	public SpriteText didUknowLbl;

	public SpriteText gameTipsLbl;

	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
	}

	private void Start()
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		UserProfileManager userProfileManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(didUknowLbl);
		fontManagerInstance.SetSmallFontMat(gameTipsLbl);
		fontManagerInstance.SetBigFontMat(loadingLbl);
		fontManagerInstance.SetSmallFontMat(badGuyTxt);
		if (userProfileManagerInstance.getLangCode() == "zh-Hant")
		{
			didUknowLbl.SetCharacterSize(30f);
			Vector3 position = didUknowLbl.transform.position;
			didUknowLbl.transform.position = new Vector3(position.x, position.y - 5f, position.z);
		}
		didUknowLbl.Text = languageManagerInstance.getLangData("DidUKnow");
		loadingLbl.Text = languageManagerInstance.getLangData("LoadingTxt");
		int num = UnityEngine.Random.Range(0, randomIcons.Length);
		for (int i = 0; i < randomIcons.Length; i++)
		{
			if (i == num)
			{
				randomIcons[i].BringIn();
			}
			else
			{
				randomIcons[i].Dismiss();
			}
		}
		if (num >= 4)
		{
			badGuyDescription.Text = languageManagerInstance.getLangData("BadGuyDescription" + num);
		}
		else
		{
			badGuyDescription.Text = string.Empty;
			badGuyDescription.transform.position = new Vector3(-1000f, -1000f, 10f);
			badGuyDescription.SetSize(0f, 0f);
		}
		int num2 = UnityEngine.Random.Range(0, 10);
		if (num2 >= 5 && loadingManagerInstance.GetSceneId() == "Mini_Game" && num < 4)
		{
			num = UnityEngine.Random.Range(0, 5);
			gameTips.Text = languageManagerInstance.getLangData("GameTips" + num);
		}
		else
		{
			gameTips.Text = string.Empty;
			gameTips.transform.position = new Vector3(-1000f, -1000f, 10f);
			gameTips.SetSize(0f, 0f);
		}
		if (loadingManagerInstance.GetSceneId() == "FD_Upgrade")
		{
			SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic);
		}
		else if (loadingManagerInstance.GetSceneId() == "Mini_Game")
		{
			SingletonMonoBehaviour<AudioManager>.Instance.StopBackgroundMusic();
		}
		SceneManager.LoadScene(loadingManagerInstance.GetSceneId());
	}
}
