using System;
using System.Collections;
using System.Collections.Generic;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreparationSceneController : MonoBehaviour
{
	private ImageManager imageManager;

	public UIScrollList skillScrollList;

	public GameObject preparationListItem;

	public GameObject armySelectedButton;

	public GameObject armySelectedBackground;

	public GameObject goldenArmySelectedBackground01;

	public GameObject goldenArmySelectedBackground02;

	public UIButton continueButton;

	public UIButton backButton;

	public SpriteText SeasonNum;

	public SpriteText TitleLbl;

	private List<UIButton> addSkillButtons;

	private List<GameObject> removeSkillButtons;

	private ArrayList iconArray;

	private ArrayList iconArray2;

	private ArrayList selectedArmyArray;

	private StarStrike_ArmyUnitConfiguration armyUnitConfiguration;

	private StarStrike_SpecialAttackConfiguration spAttackConfiguration;

	private LanguageManager langMan;

	private UserProfileManager up;

	private bool armySelected;

	private int currentGameLevel;

	public static float expectedScreenRatio = 2f / 3f;

	public GameObject[] enemyUnitPrefabs;

	public Material[] enemyUnitMaterials;

	public TextAsset levelXmlConfig;

	public Material buttonMaterial;

	public Material backgroundMaterial;

	public AudioClip backgroundMusic;

	private void Awake()
	{
		continueButton.SetValueChangedDelegate(onButtonClick);
		backButton.SetValueChangedDelegate(onButtonClick);
		armyUnitConfiguration = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		spAttackConfiguration = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		up = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		selectedArmyArray = up.getSelectedArmy();
		up.setCurrentScene("FD_Preparation");
		if (up.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			levelXmlConfig = Resources.Load("Data/level_config_night") as TextAsset;
			backgroundMaterial.color = new Color(14f / 51f, 13f / 51f, 16f / 51f);
			for (int i = 0; i < 26; i++)
			{
				switch (i)
				{
				case 2:
					enemyUnitMaterials[i].mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
					break;
				case 3:
					enemyUnitMaterials[i].mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
					break;
				default:
					enemyUnitMaterials[i].mainTexture = Resources.Load("Textures/Normal/E" + (i + 1).ToString("00"), typeof(Texture2D)) as Texture2D;
					break;
				}
			}
		}
		else if (up.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			levelXmlConfig = Resources.Load("Data/level_config_night") as TextAsset;
			backgroundMaterial.color = new Color(14f / 51f, 13f / 51f, 16f / 51f);
			for (int j = 0; j < 26; j++)
			{
				switch (j)
				{
				case 2:
					enemyUnitMaterials[j].mainTexture = Resources.Load("Textures/Halloween/E01", typeof(Texture2D)) as Texture2D;
					break;
				case 3:
					enemyUnitMaterials[j].mainTexture = Resources.Load("Textures/Halloween/E01", typeof(Texture2D)) as Texture2D;
					break;
				default:
					enemyUnitMaterials[j].mainTexture = Resources.Load("Textures/Halloween/E" + (j + 1).ToString("00"), typeof(Texture2D)) as Texture2D;
					break;
				}
			}
		}
		else
		{
			backgroundMaterial.color = Color.white;
			for (int k = 0; k < 26; k++)
			{
				switch (k)
				{
				case 2:
					enemyUnitMaterials[k].mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
					break;
				case 3:
					enemyUnitMaterials[k].mainTexture = Resources.Load("Textures/Normal/E01", typeof(Texture2D)) as Texture2D;
					break;
				default:
					enemyUnitMaterials[k].mainTexture = Resources.Load("Textures/Normal/E" + (k + 1).ToString("00"), typeof(Texture2D)) as Texture2D;
					break;
				}
			}
		}
		iconArray = new ArrayList();
		iconArray2 = new ArrayList();
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(SeasonNum);
		string langData = langMan.getLangData("UpgradeScene_Season_Txt");
		if (up.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			SeasonNum.Text = langData.Replace("#", up.getGameLevel_NMode().ToString());
		}
		else if (up.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			SeasonNum.Text = langData.Replace("#", up.getGameLevel_HalloweenMode().ToString());
		}
		else
		{
			SeasonNum.Text = langData.Replace("#", up.getGameLevel().ToString());
		}
		currentGameLevel = up.getGameLevel();
		imageManager = ImageManager.Instance;
		addSkillButtons = new List<UIButton>();
		removeSkillButtons = new List<GameObject>();
		armySelected = false;
		LoadTextures();
	}

	private void LoadTextures()
	{
		Texture2D texture2D = Resources.Load("ButtonsMaterial-" + up.getLangCode()) as Texture2D;
		if (texture2D == null)
		{
			buttonMaterial.mainTexture = Resources.Load("ButtonsMaterial") as Texture2D;
		}
		else
		{
			buttonMaterial.mainTexture = texture2D;
		}
	}

	private void Start()
	{
		BuildSkillScrollList();
		ShowPreviousArmySelection();
		AdjustSize();
		ShowEnemy();
		//MunerisController.Instance.ReportEvent("menu");
		continueButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		backButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		StartCoroutine(WaitAndPlay(0.5f));
		SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic);
	}

	public IEnumerator WaitAndPlay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		continueButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
		backButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}

	private void AdjustSize()
	{
		float num = (float)Screen.height / (float)Screen.width / expectedScreenRatio;
		if (num > 1f)
		{
			Camera[] allCameras = Camera.allCameras;
			foreach (Camera camera in allCameras)
			{
				Debug.Log("c.orthographicSize = " + camera.orthographicSize);
				camera.orthographic = true;
				camera.orthographicSize = num * 320f;
			}
		}
	}

	private void ShowEnemy()
	{
		List<int> list = new List<int>();
		StarStrike_LevelDefinitionlManager starStrike_LevelDefinitionlManager = new StarStrike_LevelDefinitionlManager(levelXmlConfig.text);
		StarStrike_LevelDefinition starStrike_LevelDefinition = starStrike_LevelDefinitionlManager.MoveToNextLevel();
		while (starStrike_LevelDefinition.HasNextWave())
		{
			StarStrike_WaveDefinition starStrike_WaveDefinition = starStrike_LevelDefinition.MoveToNextWave();
			while (starStrike_WaveDefinition.HasNext())
			{
				StarStrike_ArmyDefinition starStrike_ArmyDefinition = starStrike_WaveDefinition.MoveToNext();
				int model = starStrike_ArmyDefinition.GetModel();
				if (!list.Contains(model))
				{
					list.Add(starStrike_ArmyDefinition.GetModel());
				}
			}
		}
		list.Sort();
		float num = 0f;
		GameObject gameObject = new GameObject("Enemies");
		gameObject.transform.position = new Vector3(-180f, -200f, 0f);
		for (int num2 = list.Count - 1; num2 >= 0; num2--)
		{
			int num3 = list[num2];
			GameObject original = enemyUnitPrefabs[num3 - 1];
			GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity);
			gameObject2.transform.parent = gameObject.transform;
			num += gameObject2.transform.localScale.x * 100f;
			gameObject2.transform.localPosition = new Vector3(num, 0f, 50f + (float)num2);
			num += gameObject2.transform.localScale.x * 100f;
			gameObject2.transform.localScale = new Vector3(200f * gameObject2.transform.localScale.x, 200f * gameObject2.transform.localScale.y, 0.01f);
		}
		float num4 = num;
		TweenContainerTo(gameObject, -600f - num4);
	}

	private void TweenContainerTo(GameObject container, float x)
	{
		iTween.MoveTo(container, iTween.Hash("x", x, "speed", 70, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncompleteparams", container, "oncomplete", "HandleContainerTweeningComplete"));
	}

	private void HandleContainerTweeningComplete(GameObject container)
	{
		iTween.MoveFrom(container, iTween.Hash("x", -100, "speed", 70, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncompleteparams", container, "oncomplete", "HandleContainerTweeningComplete"));
	}

	private void BuildSpecialAttackScrollList()
	{
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(TitleLbl);
		TitleLbl.Text = langMan.getLangData("SelectArmyScene_WarriorTitle");
		skillScrollList.ClearList(true);
		iconArray.Clear();
		iconArray2.Clear();
		Debug.Log("+++ icon Array: " + iconArray.Count + "++++");
		int i;
		for (i = 0; i < 5; i++)
		{
			UnityEngine.Object.Destroy(GameObject.Find("armyBg" + i));
		}
		for (i = 0; i < selectedArmyArray.Count; i++)
		{
			UnityEngine.Object.Destroy(GameObject.Find("armyIcon" + i));
		}
		addSkillButtons.Clear();
		removeSkillButtons.Clear();
		selectedArmyArray.Clear();
		selectedArmyArray = up.getSelectedSpecialAttack();
		Debug.Log("array: " + addSkillButtons.Count + ", " + removeSkillButtons.Count);
		i = 0;
		UIListItemContainer uIListItemContainer = null;
		ArrayList specialAttackArray = spAttackConfiguration.GetSpecialAttackArray();
		foreach (string item in specialAttackArray)
		{
			StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(item);
			bool flag = ((currentGameLevel < int.Parse(definition.GetAttributeValue("UnlockLevel"))) ? true : false);
			FD_ObjectLevelDefinition currentLevel = spAttackConfiguration.GetCurrentLevel(item);
			string attributeValue = currentLevel.GetAttributeValue("attack");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("type", "SpecialAttack");
			dictionary.Add("UnitName", item);
			dictionary.Add("id", definition.GetId().ToString());
			dictionary.Add("cooldown", definition.GetAttributeValue("cooldown").ToString());
			bool flag2 = checkAlreadyAdded(dictionary);
			string path = ((!definition.HasAttribute("icon")) ? "Images/small_heart" : ((flag || flag2) ? ("Images/" + definition.GetAttributeValue("icon") + "_g") : ("Images/" + definition.GetAttributeValue("icon"))));
			UIButton uIButton;
			UIButton uIButton2;
			if (i % 2 == 0)
			{
				uIListItemContainer = (UIListItemContainer)skillScrollList.CreateItem(preparationListItem);
				Texture2D image = imageManager.GetImage(path);
				uIButton = (UIButton)uIListItemContainer.GetElement("left_icon");
				uIButton.Data = dictionary;
				uIButton.name = "button_" + item;
				uIButton2 = (UIButton)uIListItemContainer.GetElement("BlackBg_Left");
				uIButton2.Data = dictionary;
				uIButton2.name = "button2_" + item;
				UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("left_SpawnPrice");
				uIButton3.Text = string.Empty;
				uIButton3.SetSize(0f, 0f);
				UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("left_Heal");
				uIButton4.Text = string.Empty;
				uIButton4.SetSize(0f, 0f);
				UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("left_Demage");
				if (!flag)
				{
					float num = Mathf.Floor((float)int.Parse(attributeValue) * 1.2f);
					if (item == "Rein")
					{
						uIButton5.Text = attributeValue + "%";
					}
					else
					{
						uIButton5.Text = num.ToString();
					}
					if (image != null)
					{
						uIButton.SetTexture(image);
					}
				}
				else
				{
					uIButton.SetTexture(imageManager.GetImage("Images/lock_2"));
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
				}
				UIButton uIButton6 = (UIButton)uIListItemContainer.GetElement("left_HP");
				uIButton6.Text = string.Empty;
				uIButton6.SetSize(0f, 0f);
			}
			else
			{
				Texture2D image2 = imageManager.GetImage(path);
				uIButton = (UIButton)uIListItemContainer.GetElement("right_icon");
				uIButton.Data = dictionary;
				uIButton.name = "button_" + item;
				uIButton2 = (UIButton)uIListItemContainer.GetElement("BlackBg_Right");
				uIButton2.Data = dictionary;
				uIButton2.name = "button2_" + item;
				UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("right_SpawnPrice");
				uIButton3.Text = string.Empty;
				uIButton3.SetSize(0f, 0f);
				UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("right_Heal");
				uIButton4.Text = string.Empty;
				uIButton4.SetSize(0f, 0f);
				UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("right_Damage");
				if (!flag)
				{
					float num = Mathf.Floor((float)int.Parse(attributeValue) * 1.2f);
					if (item == "Rein")
					{
						uIButton5.Text = attributeValue + "%";
					}
					else
					{
						uIButton5.Text = num.ToString();
					}
					if (image2 != null)
					{
						uIButton.SetTexture(image2);
					}
				}
				else
				{
					uIButton.SetTexture(imageManager.GetImage("Images/lock_2"));
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
				}
				UIButton uIButton6 = (UIButton)uIListItemContainer.GetElement("right_HP");
				uIButton6.Text = string.Empty;
				uIButton6.SetSize(0f, 0f);
			}
			uIButton.SetValueChangedDelegate(AddSpecialAttack);
			uIButton2.SetValueChangedDelegate(AddSpecialAttack);
			if (flag || flag2)
			{
				uIButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton2.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			addSkillButtons.Add(uIButton);
			iconArray.Add(uIButton);
			iconArray2.Add(uIButton2);
			i++;
		}
		if (i % 2 != 0)
		{
			UIButton uIButton7 = (UIButton)uIListItemContainer.GetElement("right_icon");
			uIButton7.SetSize(0f, 0f);
			UIButton uIButton8 = (UIButton)uIListItemContainer.GetElement("right_iconBackground");
			uIButton8.SetSize(0f, 0f);
			UIButton uIButton9 = (UIButton)uIListItemContainer.GetElement("right_Damage");
			uIButton9.Text = string.Empty;
			uIButton9.SetSize(0f, 0f);
			UIButton uIButton10 = (UIButton)uIListItemContainer.GetElement("right_HP");
			uIButton10.Text = string.Empty;
			uIButton10.SetSize(0f, 0f);
			UIButton uIButton11 = (UIButton)uIListItemContainer.GetElement("right_SpawnPrice");
			uIButton11.Text = string.Empty;
			uIButton11.SetSize(0f, 0f);
			UIButton uIButton12 = (UIButton)uIListItemContainer.GetElement("right_Heal");
			uIButton12.Text = string.Empty;
			uIButton12.SetSize(0f, 0f);
		}
		int goldenSlotCount = up.getGoldenSlotCount("goldenSlot02_lvl");
		for (i = 0; i < up.getTotalSpAttackSlot(); i++)
		{
			GameObject gameObject = ((i >= goldenSlotCount) ? UnityEngine.Object.Instantiate(armySelectedBackground) : UnityEngine.Object.Instantiate(goldenArmySelectedBackground02));
			gameObject.SetActiveRecursivelyLegacy(true);
			gameObject.transform.position = new Vector3(130 * i - 403, -275f, -1f);
			gameObject.name = "armyBg" + i;
		}
	}

	private void BuildSkillScrollList()
	{
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(TitleLbl);
		TitleLbl.Text = langMan.getLangData("SelectArmyScene_NumenTitle");
		UIListItemContainer uIListItemContainer = null;
		ArrayList goodGuy = armyUnitConfiguration.GetGoodGuy();
		int num = 0;
		foreach (string item in goodGuy)
		{
			if (item == "Hero")
			{
				continue;
			}
			StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(item);
			bool flag = ((currentGameLevel < int.Parse(unitDefinition.GetAttributeValue("UnlockLevel"))) ? true : false);
			string text2 = ((!unitDefinition.HasAttribute("icon")) ? "Images/small_heart" : (flag ? ("Images/" + unitDefinition.GetAttributeValue("icon") + "_g") : ("Images/" + unitDefinition.GetAttributeValue("icon"))));
			FD_ObjectLevelDefinition currentLevel = armyUnitConfiguration.GetCurrentLevel(item);
			string attributeValue = currentLevel.GetAttributeValue("attack");
			string attributeValue2 = currentLevel.GetAttributeValue("health");
			string attributeValue3 = unitDefinition.GetAttributeValue("price");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("UnitName", item);
			dictionary.Add("id", unitDefinition.GetId().ToString());
			dictionary.Add("price", unitDefinition.GetAttributeValue("price"));
			dictionary.Add("cooldown", unitDefinition.GetAttributeValue("cooldown").ToString());
			dictionary.Add("type", "Army");
			bool flag2 = checkAlreadyAdded(dictionary);
			UIButton uIButton;
			UIButton uIButton2;
			if (num % 2 == 0)
			{
				uIListItemContainer = (UIListItemContainer)skillScrollList.CreateItem(preparationListItem);
				uIButton = (UIButton)uIListItemContainer.GetElement("left_icon");
				uIButton2 = (UIButton)uIListItemContainer.GetElement("BlackBg_Left");
				uIButton.Data = dictionary;
				uIButton.name = "button_" + item;
				uIButton2.Data = dictionary;
				uIButton2.name = "button2_" + item;
				UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("left_Heal");
				UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("left_Demage");
				if (!flag)
				{
					float num2 = Mathf.Floor((float)int.Parse(attributeValue) * 1.2f);
					if (item == "Army_Owl")
					{
						uIButton4.Text = string.Empty;
						uIButton4.SetSize(0f, 0f);
						uIButton3.Text = num2.ToString();
						uIButton3.SetSize(50f, 50f);
					}
					else
					{
						uIButton4.Text = num2.ToString();
						uIButton4.SetSize(55f, 57f);
						uIButton3.Text = string.Empty;
						uIButton3.SetSize(0f, 0f);
					}
					if (flag2)
					{
						uIButton.SetTexture(imageManager.GetImage(text2 + "_g"));
					}
					else
					{
						uIButton.SetTexture(imageManager.GetImage(text2));
					}
				}
				else
				{
					uIButton.SetTexture(imageManager.GetImage("Images/lock_2"));
					uIButton4.Text = string.Empty;
					uIButton4.SetSize(0f, 0f);
					uIButton3.Text = string.Empty;
					uIButton3.SetSize(0f, 0f);
				}
				UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("left_HP");
				float num3 = Mathf.Floor((float)int.Parse(attributeValue2) * 1.2f);
				if (!flag)
				{
					uIButton5.Text = num3.ToString();
				}
				else
				{
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
				}
				UIButton uIButton6 = (UIButton)uIListItemContainer.GetElement("left_SpawnPrice");
				uIButton6.Text = attributeValue3;
			}
			else
			{
				uIButton = (UIButton)uIListItemContainer.GetElement("right_icon");
				uIButton2 = (UIButton)uIListItemContainer.GetElement("BlackBg_Right");
				uIButton.Data = dictionary;
				uIButton.name = "button_" + item;
				uIButton2.Data = dictionary;
				uIButton2.name = "button2_" + item;
				UIButton uIButton3 = (UIButton)uIListItemContainer.GetElement("right_Heal");
				UIButton uIButton4 = (UIButton)uIListItemContainer.GetElement("right_Damage");
				if (!flag)
				{
					float num2 = Mathf.Floor((float)int.Parse(attributeValue) * 1.2f);
					if (item == "Army_Owl")
					{
						uIButton4.Text = string.Empty;
						uIButton4.SetSize(0f, 0f);
						uIButton3.Text = num2.ToString();
						uIButton3.SetSize(50f, 50f);
					}
					else
					{
						uIButton4.Text = num2.ToString();
						uIButton4.SetSize(55f, 57f);
						uIButton3.Text = string.Empty;
						uIButton3.SetSize(0f, 0f);
					}
					if (flag2)
					{
						uIButton.SetTexture(imageManager.GetImage(text2 + "_g"));
					}
					else
					{
						uIButton.SetTexture(imageManager.GetImage(text2));
					}
				}
				else
				{
					uIButton.SetTexture(imageManager.GetImage("Images/lock_2"));
					uIButton4.Text = string.Empty;
					uIButton4.SetSize(0f, 0f);
					uIButton3.Text = string.Empty;
					uIButton3.SetSize(0f, 0f);
				}
				UIButton uIButton5 = (UIButton)uIListItemContainer.GetElement("right_HP");
				float num3 = Mathf.Floor((float)int.Parse(attributeValue2) * 1.2f);
				if (!flag)
				{
					uIButton5.Text = num3.ToString();
				}
				else
				{
					uIButton5.Text = string.Empty;
					uIButton5.SetSize(0f, 0f);
				}
				UIButton uIButton6 = (UIButton)uIListItemContainer.GetElement("right_SpawnPrice");
				uIButton6.Text = attributeValue3;
			}
			uIButton.SetValueChangedDelegate(AddSkill);
			uIButton2.SetValueChangedDelegate(AddSkill);
			if (flag || flag2)
			{
				uIButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				uIButton2.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			addSkillButtons.Add(uIButton);
			iconArray.Add(uIButton);
			iconArray2.Add(uIButton2);
			num++;
		}
		if (num % 2 != 0)
		{
			UIButton uIButton7 = (UIButton)uIListItemContainer.GetElement("right_icon");
			uIButton7.SetSize(0f, 0f);
			UIButton uIButton8 = (UIButton)uIListItemContainer.GetElement("right_iconBackground");
			uIButton8.SetSize(0f, 0f);
			UIButton uIButton9 = (UIButton)uIListItemContainer.GetElement("right_Damage");
			uIButton9.Text = string.Empty;
			uIButton9.SetSize(0f, 0f);
			UIButton uIButton10 = (UIButton)uIListItemContainer.GetElement("right_HP");
			uIButton10.Text = string.Empty;
			uIButton10.SetSize(0f, 0f);
			UIButton uIButton11 = (UIButton)uIListItemContainer.GetElement("right_SpawnPrice");
			uIButton11.Text = string.Empty;
			uIButton11.SetSize(0f, 0f);
			UIButton uIButton12 = (UIButton)uIListItemContainer.GetElement("right_Heal");
			uIButton12.Text = string.Empty;
			uIButton12.SetSize(0f, 0f);
		}
		int goldenSlotCount = up.getGoldenSlotCount("goldenSlot01_lvl");
		for (num = 0; num < 5; num++)
		{
			GameObject gameObject = ((num >= goldenSlotCount) ? UnityEngine.Object.Instantiate(armySelectedBackground) : UnityEngine.Object.Instantiate(goldenArmySelectedBackground01));
			gameObject.SetActive(true);
			gameObject.transform.position = new Vector3(130 * num - 403, -275f, -1f);
			gameObject.name = "armyBg" + num;
		}
	}

	private void ShowPreviousSpecialAttackSelection()
	{
		Debug.Log("previous special attack: " + selectedArmyArray.Count);
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			Dictionary<string, string> dictionary = (Dictionary<string, string>)selectedArmyArray[i];
			dictionary.Add("type", "SpecialAttack");
			if (dictionary["UnitName"] != string.Empty)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(armySelectedButton);
				gameObject.SetActive(true);
				gameObject.transform.position = new Vector3(130 * i - 361, -374f, -1f);
				gameObject.name = "armyIcon" + i;
				StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(dictionary["UnitName"]);
				string path = ((!definition.HasAttribute("icon")) ? "Images/small_heart" : ("Images/" + definition.GetAttributeValue("icon")));
				UIButton component = gameObject.transform.Find("icon").GetComponent<UIButton>();
				component.SetTexture(imageManager.GetImage(path));
				component.gameObject.SetActive(true);
				component.Data = dictionary;
				component.SetValueChangedDelegate(RemoveSkill);
				UIButton component2 = gameObject.transform.Find("delButton").GetComponent<UIButton>();
				component2.Data = dictionary;
				component2.SetValueChangedDelegate(RemoveSkill);
				component2.gameObject.SetActive(true);
				removeSkillButtons.Add(gameObject);
			}
			else
			{
				selectedArmyArray.RemoveAt(i);
				i--;
			}
		}
	}

	private void ShowPreviousArmySelection()
	{
		Debug.Log("previous army: " + selectedArmyArray.Count);
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			Dictionary<string, string> dictionary = (Dictionary<string, string>)selectedArmyArray[i];
			dictionary.Add("type", "Army");
			Debug.Log("objName:" + dictionary["UnitName"]);
			if (dictionary["UnitName"] != string.Empty && dictionary["UnitName"] != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(armySelectedButton);
				gameObject.SetActive(true);
				gameObject.transform.position = new Vector3(130 * i - 361, -374f, -1f);
				gameObject.name = "armyIcon" + i;
				StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(dictionary["UnitName"]);
				string path = ((!unitDefinition.HasAttribute("icon")) ? "Images/small_heart" : ("Images/" + unitDefinition.GetAttributeValue("icon")));
				UIButton component = gameObject.transform.Find("icon").GetComponent<UIButton>();
				component.SetTexture(imageManager.GetImage(path));
				component.gameObject.SetActive(true);
				component.Data = dictionary;
				component.SetValueChangedDelegate(RemoveSkill);
				UIButton component2 = gameObject.transform.Find("delButton").GetComponent<UIButton>();
				component2.Data = dictionary;
				component2.SetValueChangedDelegate(RemoveSkill);
				component2.gameObject.SetActive(true);
				removeSkillButtons.Add(gameObject);
				Debug.Log("UnitName:" + dictionary["UnitName"]);
			}
			else
			{
				selectedArmyArray.RemoveAt(i);
				i--;
			}
		}
	}

	private void AddSkill(IUIObject button)
	{
		Dictionary<string, string> dictionary = (Dictionary<string, string>)button.Data;
		Debug.Log("UnitName: " + dictionary["UnitName"] + ", " + dictionary["id"]);
		if (selectedArmyArray.Count < 5 && !checkAlreadyAdded(dictionary))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(armySelectedButton);
			gameObject.SetActive(true);
			gameObject.transform.position = new Vector3(130 * selectedArmyArray.Count - 361, -374f, -1f);
			gameObject.name = "armyIcon" + selectedArmyArray.Count;
			StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(dictionary["UnitName"]);
			string path = ((!unitDefinition.HasAttribute("icon")) ? "Images/small_heart" : ("Images/" + unitDefinition.GetAttributeValue("icon")));
			UIButton component = gameObject.transform.Find("icon").GetComponent<UIButton>();
			component.SetTexture(imageManager.GetImage(path));
			component.gameObject.SetActive(true);
			component.Data = dictionary;
			component.SetValueChangedDelegate(RemoveSkill);
			UIButton component2 = gameObject.transform.Find("delButton").GetComponent<UIButton>();
			component2.Data = dictionary;
			component2.SetValueChangedDelegate(RemoveSkill);
			component2.gameObject.SetActive(true);
			removeSkillButtons.Add(gameObject);
			selectedArmyArray.Add(dictionary);
			UIButton component3 = GameObject.Find("button_" + dictionary["UnitName"]).GetComponent<UIButton>();
			UIButton component4 = GameObject.Find("button2_" + dictionary["UnitName"]).GetComponent<UIButton>();
			if (component3 != null)
			{
				component3.SetTexture(imageManager.GetImage("Images/" + unitDefinition.GetAttributeValue("icon") + "_g"));
				component3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
				component4.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
	}

	private bool checkAlreadyAdded(Dictionary<string, string> objDict)
	{
		bool result = false;
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			Dictionary<string, string> dictionary = (Dictionary<string, string>)selectedArmyArray[i];
			if (objDict["UnitName"] == dictionary["UnitName"])
			{
				result = true;
			}
		}
		return result;
	}

	private void RemoveSkill(IUIObject button)
	{
		Dictionary<string, string> dictionary = (Dictionary<string, string>)button.Data;
		Debug.Log("Remove Skill: " + dictionary["UnitName"] + ", " + dictionary["id"]);
		bool flag = false;
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			if (flag)
			{
				Debug.Log("change position: " + i);
				iTween.MoveTo(removeSkillButtons[i], new Vector3(130 * i - 361, -374f, -1f), 1f);
				removeSkillButtons[i].name = "armyIcon" + i;
			}
			if (dictionary != selectedArmyArray[i])
			{
				continue;
			}
			for (int j = 0; j < iconArray.Count; j++)
			{
				UIButton uIButton = (UIButton)iconArray[j];
				UIButton uIButton2 = (UIButton)iconArray2[j];
				string text = "button_" + dictionary["UnitName"];
				if (uIButton.name == text)
				{
					StarStrike_ObjectDefinition starStrike_ObjectDefinition = ((!(dictionary["type"].ToString() == "Army")) ? spAttackConfiguration.GetDefinition(dictionary["UnitName"]) : armyUnitConfiguration.GetUnitDefinition(dictionary["UnitName"]));
					uIButton.SetTexture(imageManager.GetImage("Images/" + starStrike_ObjectDefinition.GetAttributeValue("icon")));
					uIButton.SetControlState(UIButton.CONTROL_STATE.ACTIVE);
					uIButton2.SetControlState(UIButton.CONTROL_STATE.ACTIVE);
				}
			}
			UnityEngine.Object.Destroy(removeSkillButtons[i]);
			selectedArmyArray.RemoveAt(i);
			removeSkillButtons.RemoveAt(i);
			i--;
			flag = true;
		}
	}

	private void AddSpecialAttack(IUIObject button)
	{
		Dictionary<string, string> dictionary = (Dictionary<string, string>)button.Data;
		Debug.Log("Adding Special Attack -----> UnitName: " + dictionary["UnitName"] + ", " + dictionary["id"]);
		if (selectedArmyArray.Count < up.getTotalSpAttackSlot() && !checkAlreadyAdded(dictionary))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(armySelectedButton);
			gameObject.SetActive(true);
			gameObject.transform.position = new Vector3(130 * selectedArmyArray.Count - 361, -374f, -1f);
			gameObject.name = "armyIcon" + selectedArmyArray.Count;
			StarStrike_ObjectDefinition definition = spAttackConfiguration.GetDefinition(dictionary["UnitName"]);
			string path = ((!definition.HasAttribute("icon")) ? "Images/small_heart" : ("Images/" + definition.GetAttributeValue("icon")));
			UIButton component = gameObject.transform.Find("icon").GetComponent<UIButton>();
			component.SetTexture(imageManager.GetImage(path));
			component.gameObject.SetActive(true);
			component.Data = dictionary;
			component.SetValueChangedDelegate(RemoveSkill);
			UIButton component2 = gameObject.transform.Find("delButton").GetComponent<UIButton>();
			component2.Data = dictionary;
			component2.SetValueChangedDelegate(RemoveSkill);
			component2.gameObject.SetActive(true);
			removeSkillButtons.Add(gameObject);
			selectedArmyArray.Add(dictionary);
			UIButton component3 = GameObject.Find("button_" + dictionary["UnitName"]).GetComponent<UIButton>();
			if (component3 != null)
			{
				component3.SetTexture(imageManager.GetImage("Images/" + definition.GetAttributeValue("icon") + "_g"));
				component3.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
	}

	private int getSelectedArmy()
	{
		int num = 0;
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			Dictionary<string, string> dictionary = (Dictionary<string, string>)selectedArmyArray[i];
			Debug.Log("objName:" + dictionary["UnitName"]);
			if (dictionary["UnitName"] != string.Empty && dictionary["UnitName"] != null)
			{
				num++;
			}
		}
		return num;
	}

	private void cleanSelectedSpecialSkillData()
	{
		skillScrollList.ClearList(true);
		iconArray.Clear();
		iconArray2.Clear();
		Debug.Log("+++ icon Array: " + iconArray.Count + "++++");
		for (int i = 0; i < 2; i++)
		{
			UnityEngine.Object.Destroy(GameObject.Find("armyBg" + i));
		}
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			UnityEngine.Object.Destroy(GameObject.Find("armyIcon" + i));
		}
		addSkillButtons.Clear();
		removeSkillButtons.Clear();
		selectedArmyArray.Clear();
		selectedArmyArray = up.getSelectedArmy();
		armySelected = false;
	}

	private void onButtonClick(IUIObject obj)
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		if (!armySelected)
		{
			up.setSelectedArmy(selectedArmyArray);
		}
		else
		{
			up.setSelectedSpecialAttack(selectedArmyArray);
		}
		Debug.Log("lvl_obj: " + loadingManagerInstance.GetSceneId());
		if (obj == continueButton)
		{
			if (!armySelected)
			{
				if (getSelectedArmy() == 0)
				{
					PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, langMan.getLangData("SelectArmyScene_NoArmySelectedWarning"));
					return;
				}
				armySelected = true;
				BuildSpecialAttackScrollList();
				ShowPreviousSpecialAttackSelection();
				return;
			}
			Debug.Log("Game Level: " + up.getGameLevel());
			if (up.getGameLevel() == 1)
			{
				up.resetTutorial(1);
				loadingManagerInstance.setSceneId("FD_Opening");
			}
			else
			{
				loadingManagerInstance.setSceneId("Mini_Game");
			}
			SceneManager.LoadScene("Loading");
		}
		else if (obj == backButton)
		{
			if (!armySelected)
			{
				loadingManagerInstance.setSceneId("FD_Upgrade");
				SceneManager.LoadScene("Loading");
			}
			else
			{
				cleanSelectedSpecialSkillData();
				BuildSkillScrollList();
				ShowPreviousArmySelection();
			}
		}
	}

	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (PopupManager.Instance.isPopping())
			{
				PopupManager.Instance.HidePopup();
			}
			else if (!armySelected)
			{
				LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
				loadingManagerInstance.setSceneId("FD_Upgrade");
				SceneManager.LoadScene("Loading");
			}
			else
			{
				cleanSelectedSpecialSkillData();
				BuildSkillScrollList();
				ShowPreviousArmySelection();
			}
		}
	}
}
