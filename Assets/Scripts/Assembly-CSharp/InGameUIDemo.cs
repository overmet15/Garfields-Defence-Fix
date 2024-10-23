using System;
using System.Collections;
using System.Collections.Generic;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIDemo : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const float BLINK_TIME = 3f;

	private static int backgroundMusicIndex = -1;

	public static float expectedScreenRatio = 2f / 3f;

	public AudioClip[] backgroundMusic;

	public AudioClip testAudioClip;

	public AudioClip victoryAudioClip;

	public AudioClip defeatAudioClip;

	public StarStrike_GameState stateToBecomeActive;

	public StarStrike_GameState stateToLevelComplete;

	public UIButton[] warriorButtons;

	public UIButton[] spellButtons;

	public UIButton[] potionButtons;

	public GameObject CollectedCoinValue;

	public GameObject TotalCoinsValue;

	public GameObject AwardedCoinValue;

	public GameObject DestroyPanelCoin;

	private GameObject[] warriorButtonPrefabs;

	private GameObject[] spellButtonPrefabs;

	public Material[] warriorMaterials;

	public Material[] spellMaterials;

	public GameObject[] warriorPrefabs;

	public GameObject[] spellPrefabs;

	private GameObject[] waterProgressPrefabs;

	public GameObject hpBar;

	public GameObject progressBar;

	public GameObject waterBar;

	public GameObject sword;

	public UIPanel dangerPanel;

	public UIButton waterButton;

	public UIButton waterProgress;

	public UIButton potionButton;

	public UIPanel destroyedPanel;

	public UIPanel gameStartPanel;

	public UIPanel incomingPanel;

	public UIPanel pausePanel;

	public UIPanel victoryPanel;

	public UIPanel genHelpPanel;

	public SpriteText gameLevelText;

	public SpriteText loseCollectedText;

	public SpriteText victoryAwardText;

	public SpriteText victoryCollectedText;

	public SpriteText victoryTotalText;

	public TutorialManager tutorialManager;

	private ArrayList selectedArmyArray;

	private ArrayList selectedSpecialAttackArray;

	private StarStrike_GameStateManager gameStateManager;

	private KillEnemiesManager _KillEnemiesManager;

	private DropItemsManager _DropItemManager;

	private UserProfileManager _UserProfileManager;

	private StarStrike_LevelManager levelManager;

	private StarStrike_CountdownTimer displayTimer;

	private bool LevelCompletePrompt;

	public bool _showStage3Help;

	private bool pause;

	private bool _HealAll;

	private float _HealFactor = 0.1f;

	private float heroHealth;

	private float treeHealth;

	private bool _genHelp;

	private bool _PlayerLost;

	private Dictionary<string, StarStrike_ConditionalAction> actions;

	private bool countEnd;

	private bool showPromptToRate;

	private int invincibleHeroCount;

	public Material victoryScreenMaterial;

	public Material loseScreenMaterial;

	public Material miniGameSceneMaterial;

	public Material background0Material;

	public Material background1Material;

	public Material foregroundMaterial;

	public StarStrike_ArmyUnit hero;

	private LanguageManager langMan;

	public Material garfieldMaterial;

	public Material fridgeMaterial;

	public Material squeakMaterial;

	public Material lanolinMaterial;

	private string[] warriorTextureNames = new string[9] { "G02_Sheldon", "G03_Nermal", "G04_Wade", "G05_Booker", "G07_Odie", "G08_Arlene", "G09_Orson", "G10_Roy", "G11_SuperOdie" };

	private string[] spellTextureNames = new string[6] { "SP01_Box", "SP02_Toycar", "SP03_Toyplane", "SP04_Rein", "SP05_Book", "SP06_Jon" };

	private static Vector3[] StartPostions = new Vector3[3]
	{
		new Vector3(0f, 2f, -2f),
		new Vector3(-19f, 2f, 0f),
		new Vector3(-19f, 2f, 0f)
	};

	private StarStrike_MineralProducer mineralProducer;

	private int _CurrentLevel;

	public bool IsPause
	{
		get
		{
			return pause;
		}
	}

	public int RandomUnitIndex
	{
		get
		{
			int[] array = new int[6] { 0, 1, 2, 3, 4, 7 };
			int levelNumber = levelManager.GetLevelNumber();
			int num = 4;
			if (levelNumber >= 14 && levelNumber < 23)
			{
				num = 5;
			}
			else if (levelNumber >= 23)
			{
				num = 6;
			}
			List<int> list = new List<int>();
			for (int i = 0; i < num; i++)
			{
				list.Add(array[i]);
				Debug.Log("ReinUnitsLib[i]: " + array[i]);
			}
			Debug.Log("CurrentReinUnits.Count: " + list.Count);
			int max = list.Count - 1;
			return UnityEngine.Random.Range(0, max);
		}
	}

	private void Awake()
	{
		_UserProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		_UserProfileManager.setCurrentScene("Mini_Game");
		PopupManager.Instance.HidePopup();
		string langCode = _UserProfileManager.getLangCode();
		levelManager = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			levelManager.levelXmlConfig = Resources.Load("Data/level_config_night") as TextAsset;
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Night Camera")) as GameObject;
			gameObject.transform.parent = Camera.main.transform;
			gameObject.transform.localPosition = new Vector3(-4f, 0f, 1f);
			Camera.main.backgroundColor = new Color(16f / 85f, 0.2784314f, 42f / 85f);
			background0Material.mainTexture = Resources.Load("Background/0/Night/atlas0") as Texture2D;
			background1Material.mainTexture = Resources.Load("Background/1/Night/atlas0") as Texture2D;
			foregroundMaterial.mainTexture = Resources.Load("Foreground/Night/atlas0") as Texture2D;
			garfieldMaterial.mainTexture = Resources.Load("Textures/Normal/G01_Garfield") as Texture2D;
			fridgeMaterial.mainTexture = Resources.Load("Textures/Normal/G13_Fridge") as Texture2D;
			squeakMaterial.mainTexture = Resources.Load("Textures/Normal/D01_Squeak") as Texture2D;
			lanolinMaterial.mainTexture = Resources.Load("Textures/Normal/D02_Bo_Lanolin") as Texture2D;
			for (int i = 0; i < warriorMaterials.Length; i++)
			{
				warriorMaterials[i].mainTexture = Resources.Load("Textures/Normal/" + warriorTextureNames[i]) as Texture2D;
			}
			for (int j = 0; j < spellMaterials.Length; j++)
			{
				spellMaterials[j].mainTexture = Resources.Load("Textures/Normal/" + spellTextureNames[j]) as Texture2D;
			}
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			levelManager.levelXmlConfig = Resources.Load("Data/level_config_night") as TextAsset;
			Camera.main.backgroundColor = new Color(16f / 85f, 0.2784314f, 42f / 85f);
			background0Material.mainTexture = Resources.Load("Background/0/Halloween/atlas0") as Texture2D;
			background1Material.mainTexture = Resources.Load("Background/1/Halloween/atlas0") as Texture2D;
			foregroundMaterial.mainTexture = Resources.Load("Foreground/Halloween/atlas0") as Texture2D;
			garfieldMaterial.mainTexture = Resources.Load("Textures/Halloween/G01_Garfield") as Texture2D;
			fridgeMaterial.mainTexture = Resources.Load("Textures/Halloween/G13_Fridge") as Texture2D;
			squeakMaterial.mainTexture = Resources.Load("Textures/Halloween/D01_Squeak") as Texture2D;
			lanolinMaterial.mainTexture = Resources.Load("Textures/Halloween/D02_Bo_Lanolin") as Texture2D;
			for (int k = 0; k < warriorMaterials.Length; k++)
			{
				warriorMaterials[k].mainTexture = Resources.Load("Textures/Halloween/" + warriorTextureNames[k]) as Texture2D;
			}
			for (int l = 0; l < spellMaterials.Length; l++)
			{
				spellMaterials[l].mainTexture = Resources.Load("Textures/Halloween/" + spellTextureNames[l]) as Texture2D;
			}
		}
		else if (_UserProfileManager.ChineseNewYear)
		{
			Camera.main.backgroundColor = new Color(0.85490197f, 0.16862746f, 0.16862746f);
			background0Material.mainTexture = Resources.Load("Background/0/CNY/atlas0") as Texture2D;
			background1Material.mainTexture = Resources.Load("Background/1/CNY/atlas0") as Texture2D;
			foregroundMaterial.mainTexture = Resources.Load("Foreground/Normal/atlas0") as Texture2D;
			garfieldMaterial.mainTexture = Resources.Load("Textures/CNY/G01_Garfield") as Texture2D;
			fridgeMaterial.mainTexture = Resources.Load("Textures/Normal/G13_Fridge") as Texture2D;
			squeakMaterial.mainTexture = Resources.Load("Textures/Normal/D01_Squeak") as Texture2D;
			lanolinMaterial.mainTexture = Resources.Load("Textures/CNY/D02_Bo_Lanolin") as Texture2D;
			for (int m = 0; m < warriorMaterials.Length; m++)
			{
				warriorMaterials[m].mainTexture = Resources.Load("Textures/Normal/" + warriorTextureNames[m]) as Texture2D;
				if (warriorTextureNames[m] == "G03_Nermal" || warriorTextureNames[m] == "G07_Odie" || warriorTextureNames[m] == "G09_Orson")
				{
					warriorMaterials[m].mainTexture = Resources.Load("Textures/CNY/" + warriorTextureNames[m]) as Texture2D;
				}
			}
			for (int n = 0; n < spellMaterials.Length; n++)
			{
				spellMaterials[n].mainTexture = Resources.Load("Textures/Normal/" + spellTextureNames[n]) as Texture2D;
			}
		}
		else
		{
			background0Material.mainTexture = Resources.Load("Background/0/Normal/atlas0") as Texture2D;
			background1Material.mainTexture = Resources.Load("Background/1/Normal/atlas0") as Texture2D;
			foregroundMaterial.mainTexture = Resources.Load("Foreground/Normal/atlas0") as Texture2D;
			garfieldMaterial.mainTexture = Resources.Load("Textures/Normal/G01_Garfield") as Texture2D;
			fridgeMaterial.mainTexture = Resources.Load("Textures/Normal/G13_Fridge") as Texture2D;
			squeakMaterial.mainTexture = Resources.Load("Textures/Normal/D01_Squeak") as Texture2D;
			lanolinMaterial.mainTexture = Resources.Load("Textures/Normal/D02_Bo_Lanolin") as Texture2D;
			for (int num = 0; num < warriorMaterials.Length; num++)
			{
				warriorMaterials[num].mainTexture = Resources.Load("Textures/Normal/" + warriorTextureNames[num]) as Texture2D;
			}
			for (int num2 = 0; num2 < spellMaterials.Length; num2++)
			{
				spellMaterials[num2].mainTexture = Resources.Load("Textures/Normal/" + spellTextureNames[num2]) as Texture2D;
			}
		}
		destroyedPanel.BringIn();
		victoryPanel.BringIn();
		Texture2D texture2D = Resources.Load("VictoryScreenMaterial-" + langCode) as Texture2D;
		if (texture2D == null)
		{
			victoryScreenMaterial.mainTexture = Resources.Load("VictoryScreenMaterial") as Texture2D;
		}
		else
		{
			victoryScreenMaterial.mainTexture = texture2D;
		}
		texture2D = Resources.Load("LoseScreenMaterial-" + langCode) as Texture2D;
		if (texture2D == null)
		{
			loseScreenMaterial.mainTexture = Resources.Load("LoseScreenMaterial") as Texture2D;
		}
		else
		{
			loseScreenMaterial.mainTexture = texture2D;
		}
		texture2D = Resources.Load("MiniGameSceneMaterial-" + langCode) as Texture2D;
		if (texture2D == null)
		{
			miniGameSceneMaterial.mainTexture = Resources.Load("MiniGameSceneMaterial") as Texture2D;
		}
		else
		{
			miniGameSceneMaterial.mainTexture = texture2D;
		}
		warriorButtonPrefabs = new GameObject[9];
		warriorButtonPrefabs[0] = Resources.Load("Spawn Button A02") as GameObject;
		warriorButtonPrefabs[1] = Resources.Load("Spawn Button A03") as GameObject;
		warriorButtonPrefabs[2] = Resources.Load("Spawn Button A04") as GameObject;
		warriorButtonPrefabs[3] = Resources.Load("Spawn Button A05") as GameObject;
		warriorButtonPrefabs[4] = Resources.Load("Spawn Button A07") as GameObject;
		warriorButtonPrefabs[5] = Resources.Load("Spawn Button A08") as GameObject;
		warriorButtonPrefabs[6] = Resources.Load("Spawn Button A09") as GameObject;
		warriorButtonPrefabs[7] = Resources.Load("Spawn Button A10") as GameObject;
		warriorButtonPrefabs[8] = Resources.Load("Spawn Button A11") as GameObject;
		spellButtonPrefabs = new GameObject[6];
		for (int num3 = 0; num3 < spellButtonPrefabs.Length; num3++)
		{
			spellButtonPrefabs[num3] = Resources.Load("Spell Button sp00" + (num3 + 1)) as GameObject;
		}
		waterProgressPrefabs = new GameObject[4];
		for (int num4 = 0; num4 < waterProgressPrefabs.Length; num4++)
		{
			waterProgressPrefabs[num4] = Resources.Load("Water Progress " + num4) as GameObject;
		}
		heroHealth = 1f;
		treeHealth = 1f;
		_KillEnemiesManager = GameObject.Find("KillEnemiesManager").GetComponent<KillEnemiesManager>();
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		mineralProducer = GameObject.Find("MineralProducer").GetComponent<StarStrike_MineralProducer>();
		_DropItemManager = GameObject.Find("DropItemsManager").GetComponent<DropItemsManager>();
		selectedArmyArray = _UserProfileManager.getSelectedArmy();
		selectedSpecialAttackArray = _UserProfileManager.getSelectedSpecialAttack();
		Debug.Log("selectedArmyArray.Count: " + selectedArmyArray.Count);
		Debug.Log("selectedSpecialAttackArray.Count: " + selectedSpecialAttackArray.Count);
		displayTimer = new StarStrike_CountdownTimer(3f);
		if (_UserProfileManager.getCurrentPlayMode() != 0 || _UserProfileManager.getGameLevel() != 3)
		{
		}
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			backgroundMusicIndex = 2;
		}
		else if (backgroundMusicIndex < 0 || backgroundMusicIndex > 1)
		{
			backgroundMusicIndex = UnityEngine.Random.Range(0, 2);
		}
		else
		{
			backgroundMusicIndex = (backgroundMusicIndex + 1) % 2;
		}
	}

	private void Start()
	{
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			//MunerisController.Instance.ReportEvent("Start Night Mode Level " + _UserProfileManager.getGameLevel_NMode());
		}
		showPromptToRate = false;
		checkPotionButton();
		langMan = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			_CurrentLevel = _UserProfileManager.getGameLevel_NMode();
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			_CurrentLevel = _UserProfileManager.getGameLevel_HalloweenMode();
		}
		else
		{
			_CurrentLevel = _UserProfileManager.getGameLevel();
		}
		showDangerSign();
		if (backgroundMusicIndex < backgroundMusic.Length && backgroundMusic[backgroundMusicIndex] != null)
		{
			SingletonMonoBehaviour<AudioManager>.Instance.PlayBackgroundMusic(backgroundMusic[backgroundMusicIndex]);
		}
		if (testAudioClip != null)
		{
			SingletonMonoBehaviour<AudioManager>.Instance.Play(testAudioClip);
		}
		StarStrike_UnitCreatorAction.Reset();
		actions = new Dictionary<string, StarStrike_ConditionalAction>();
		int goldenSlotCount = _UserProfileManager.getGoldenSlotCount("goldenSlot01_lvl");
		Debug.Log(">>>> GOLDEN ARMY SLOT: " + goldenSlotCount);
		for (int i = 0; i < selectedArmyArray.Count; i++)
		{
			Dictionary<string, string> dictionary = (Dictionary<string, string>)selectedArmyArray[i];
			Debug.Log("selectedArmyArray objDict[id]: " + dictionary["id"]);
			Debug.Log("selectedArmyArray objDict[UnitName]: " + dictionary["UnitName"]);
			if (!(dictionary["id"] != string.Empty))
			{
				continue;
			}
			int num = int.Parse(dictionary["id"].ToString());
			int num2;
			try
			{
				num2 = int.Parse(dictionary["price"].ToString());
			}
			catch
			{
				num2 = num + 1;
			}
			int num3;
			try
			{
				num3 = int.Parse(dictionary["cooldown"].ToString());
			}
			catch
			{
				num3 = num + 1;
			}
			Debug.Log("warriorButtonPrefabs: " + num);
			UIButton component = UnityEngine.Object.Instantiate(warriorButtonPrefabs[num]).GetComponent<UIButton>();
			component.name = warriorButtons[i].name;
			component.transform.parent = warriorButtons[i].transform.parent;
			component.transform.localScale = warriorButtons[i].transform.localScale;
			component.transform.localRotation = warriorButtons[i].transform.localRotation;
			component.transform.localPosition = warriorButtons[i].transform.localPosition;
			if (i < goldenSlotCount)
			{
				num2 = (int)Mathf.Floor((float)num2 * 0.8f);
				component.transform.Find("Cost").GetComponent<UIButton>().Text = num2.ToString();
				component.transform.Find("Normal").GetComponent<UIButton>().SetSize(0f, 0f);
			}
			else
			{
				component.transform.Find("Cost").GetComponent<UIButton>().Text = num2.ToString();
				component.transform.Find("Golden").GetComponent<UIButton>().SetSize(0f, 0f);
			}
			UnityEngine.Object.Destroy(warriorButtons[i].gameObject);
			warriorButtons[i] = component;
			StarStrike_ConditionalAction starStrike_ConditionalAction = new StarStrike_UnitCreatorAction(mineralProducer, warriorPrefabs[num], num2, num);
			ActionButton actionButton = component.gameObject.AddComponent<ActionButton>();
			actionButton.type = ActionButton.ActionType.Spawn;
			actionButton.Action = starStrike_ConditionalAction;
			actionButton.Button = component;
			actionButton.CooldownTime = num3;
			Debug.Log("Name:" + component.name + ",  cooltime: " + num3);
			string text = dictionary["UnitName"];
			switch (text)
			{
			case "Army_Owl":
			case "Army_Gorillas":
			case "Army_Lion":
				actionButton.disableOnInvoke = true;
				actions.Add(text, starStrike_ConditionalAction);
				if (text == "Army_Gorillas")
				{
					actionButton.summonChildIndex = 2;
					actionButton.summonChildPrefab = warriorPrefabs[3];
				}
				break;
			}
		}
		if (selectedSpecialAttackArray.Count == 2)
		{
			spellButtons[0].transform.localPosition = new Vector3(73f, -33f, 0f);
			spellButtons[1].transform.localPosition = new Vector3(-46f, -33f, 0f);
			spellButtons[2].transform.localPosition = new Vector3(-2000f, -2000f, 0f);
		}
		else
		{
			spellButtons[0].transform.localPosition = new Vector3(73f, -33f, 0f);
			spellButtons[1].transform.localPosition = new Vector3(-46f, -33f, 0f);
			spellButtons[2].transform.localPosition = new Vector3(-169f, -33f, 0f);
		}
		int goldenSlotCount2 = _UserProfileManager.getGoldenSlotCount("goldenSlot02_lvl");
		Debug.Log(">>>> GOLDEN SPECIL SLOT: " + goldenSlotCount2);
		for (int j = 0; j < selectedSpecialAttackArray.Count; j++)
		{
			Dictionary<string, string> dictionary2 = (Dictionary<string, string>)selectedSpecialAttackArray[j];
			Debug.Log("selectedSpecialAttackArray objDict[id]: " + dictionary2["id"]);
			Debug.Log("selectedSpecialAttackArray objDict[UnitName]: " + dictionary2["UnitName"]);
			if (dictionary2["id"] != string.Empty)
			{
				int num4 = int.Parse(dictionary2["id"].ToString());
				Debug.Log("selectedSpecialAttackArray spellButtonPrefabs: " + num4);
				int num5;
				try
				{
					num5 = int.Parse(dictionary2["cooldown"].ToString());
				}
				catch
				{
					num5 = num4 + 1;
				}
				if (j < goldenSlotCount2)
				{
					num5 = (int)Mathf.Floor((float)num5 * 0.8f);
				}
				UIButton component2 = UnityEngine.Object.Instantiate(spellButtonPrefabs[num4]).GetComponent<UIButton>();
				if (j < goldenSlotCount2)
				{
					Debug.Log(">>> Golden");
					component2.transform.Find("Normal").GetComponent<UIButton>().SetSize(0f, 0f);
				}
				else
				{
					Debug.Log(">>> Normal");
					component2.transform.Find("Golden").GetComponent<UIButton>().SetSize(0f, 0f);
				}
				component2.name = spellButtons[j].name;
				component2.transform.parent = spellButtons[j].transform.parent;
				component2.transform.localScale = spellButtons[j].transform.localScale;
				component2.transform.localRotation = spellButtons[j].transform.localRotation;
				component2.transform.localPosition = spellButtons[j].transform.localPosition;
				UnityEngine.Object.Destroy(spellButtons[j].gameObject);
				spellButtons[j] = component2;
				ActionButton actionButton2 = component2.gameObject.AddComponent<ActionButton>();
				Debug.Log("spellPrefabs[k]: " + spellPrefabs[num4]);
				Debug.Log("StartPostions[k]: " + StartPostions[j]);
				actionButton2.type = ActionButton.ActionType.Spell;
				actionButton2.Action = new StarStrike_PrefabInstatiatorAction(spellPrefabs[num4], StartPostions[j]);
				actionButton2.Button = component2;
				actionButton2.CooldownTime = num5;
			}
		}
		AdjustSize();
		FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
		fontManagerInstance.SetBigFontMat(gameLevelText);
		fontManagerInstance.SetSmallFontMat(loseCollectedText);
		fontManagerInstance.SetSmallFontMat(victoryAwardText);
		fontManagerInstance.SetSmallFontMat(victoryCollectedText);
		fontManagerInstance.SetSmallFontMat(victoryTotalText);
		Color color = new Color(0.9843137f, 0.75686276f, 0f);
		loseCollectedText.Color = color;
		victoryAwardText.Color = color;
		victoryCollectedText.Color = color;
		victoryTotalText.Color = color;
		gameLevelText.Text = langMan.getLangData("InGameStartLevel").Replace("#", _CurrentLevel.ToString());
		loseCollectedText.Text = langMan.getLangData("endGameCollected").Replace("#", _CurrentLevel.ToString());
		victoryAwardText.Text = langMan.getLangData("endGameAward").Replace("#", _CurrentLevel.ToString());
		victoryCollectedText.Text = langMan.getLangData("endGameCollected").Replace("#", _CurrentLevel.ToString());
		victoryTotalText.Text = langMan.getLangData("endGameTotal").Replace("#", _CurrentLevel.ToString());
		destroyedPanel.Dismiss();
		victoryPanel.Dismiss();
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
		UpgradeWater();
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL)
		{
			switch (_CurrentLevel)
			{
			case 1:
				tutorialManager.Push(0, 5f);
				tutorialManager.Push(1);
				tutorialManager.Push(10, 5f);
				break;
			case 2:
				tutorialManager.Push(5);
				StartCoroutine(SetMinerals(5, 5f, true));
				break;
			case 3:
				tutorialManager.Push(7);
				tutorialManager.Push(8);
				tutorialManager.Push(14);
				tutorialManager.Push(9);
				break;
			}
		}
	}

	private void AdjustSize()
	{
		float num = (float)Screen.height / (float)Screen.width / expectedScreenRatio;
		Debug.Log(Screen.width * Screen.height);
		Debug.Log("size: " + num);
		if (num > 1f)
		{
			Camera[] componentsInChildren = GetComponentsInChildren<Camera>();
			Camera[] array = componentsInChildren;
			foreach (Camera camera in array)
			{
				Debug.Log("c.orthographicSize = " + camera.orthographicSize);
				camera.orthographic = true;
				camera.orthographicSize = num * 320f;
			}
		}
	}

	private IEnumerator SetMinerals(int minerals, float delay, bool ignoreTimeScale)
	{
		if (ignoreTimeScale)
		{
			float pauseEndTime = Time.realtimeSinceStartup + delay;
			while (Time.realtimeSinceStartup < pauseEndTime)
			{
				yield return 0;
			}
		}
		else
		{
			yield return new WaitForSeconds(delay);
		}
		mineralProducer.CurrentMinerals = minerals;
	}

	public void checkPotionButton()
	{
		for (int i = 0; i < potionButtons.Length && (i != 3 || invincibleHeroCount <= 0); i++)
		{
			int num = i + 1;
			int itemCount = _UserProfileManager.getItemCount("item0" + num + "_lvl");
			potionButtons[i].Text = "x " + itemCount;
			if (itemCount > 0)
			{
				potionButtons[i].SetControlState(UIButton.CONTROL_STATE.ACTIVE);
			}
			else
			{
				potionButtons[i].SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (pause)
			{
				Continue();
			}
			else
			{
				Pause();
			}
		}
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
		if (mineralProducer.CanUpgrade())
		{
			if (waterButton.controlState == UIButton.CONTROL_STATE.DISABLED)
			{
				waterButton.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			}
			if (waterProgress.controlState == UIButton.CONTROL_STATE.DISABLED)
			{
				waterProgress.SetControlState(UIButton.CONTROL_STATE.NORMAL);
			}
		}
		else
		{
			if (waterButton.controlState != UIButton.CONTROL_STATE.DISABLED)
			{
				waterButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
			if (waterProgress.controlState != UIButton.CONTROL_STATE.DISABLED)
			{
				waterProgress.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			}
		}
		float currentMinerals = mineralProducer.CurrentMinerals;
		float y = Mathf.Min(Mathf.Max(currentMinerals / (float)mineralProducer.MaxMinerals, 0f), 1f);
		waterProgress.Text = ((int)currentMinerals).ToString();
		waterBar.transform.localScale = new Vector3(1f, y, 1f);
		if (!gameStateManager.IsCurrentState(StarStrike_GameState.LEVEL_START) && gameStartPanel.gameObject.activeInHierarchy)
		{
			gameStartPanel.Dismiss();
			ShowTutorial();
		}
		if (gameStateManager.IsCurrentState(stateToLevelComplete) && !LevelCompletePrompt)
		{
			GameSummaryPrompt(true);
		}
		if (!displayTimer.HasElapsed())
		{
			displayTimer.Update();
		}
		else if (incomingPanel.gameObject.activeInHierarchy)
		{
			incomingPanel.Dismiss();
		}
	}

	private void ShowTutorial()
	{
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL)
		{
			tutorialManager.ShowNext();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!victoryPanel.gameObject.activeInHierarchy && !destroyedPanel.gameObject.activeInHierarchy)
		{
			Pause();
		}
	}

	private void OnApplicationFocus(bool state)
	{
		OnApplicationPause(state);
	}

	private void Continue()
	{
		Time.timeScale = 1f;
		pause = false;
		pausePanel.Dismiss();
	}

	private void Pause()
	{
		if (Time.timeScale > 0f)
		{
			Time.timeScale = 0f;
			pause = true;
			pausePanel.BringIn();
		}
	}

	private void Retry()
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		loadingManagerInstance.setSceneId("Mini_Game");
		SceneManager.LoadScene("Loading");
	}

	private void Quit()
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		loadingManagerInstance.setSceneId("FD_Upgrade");
		SceneManager.LoadScene("Loading");
	}

	public void UpdateHeroBar(float hp)
	{
		Debug.Log("UpdateHeroBar ===> " + hp);
		if (hp < 0f)
		{
			hp = 0f;
		}
		hpBar.transform.localScale = new Vector3(hp, 1f, 1f);
		heroHealth = hp;
		showDangerSign();
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL && _CurrentLevel == 3 && hp < 0.8f && !_genHelp)
		{
			genUnit();
			_genHelp = true;
		}
	}

	public void UpdateProgressBar(float progress)
	{
		Debug.Log("=======>UpdateProgressBar: " + progress);
		progressBar.transform.localScale = new Vector3(progress, 1f, 1f);
		float num = 170f * (1f - progress);
		Debug.Log("++++ sword: " + num);
		sword.transform.position = new Vector3(100f - num, sword.transform.position.y, sword.transform.position.z);
	}

	public void updateTreeHealth(float hp)
	{
		treeHealth = hp;
		if (treeHealth <= 0f)
		{
			_PlayerLost = true;
		}
		showDangerSign();
	}

	public void showDangerSign()
	{
		if (treeHealth <= 0.3f || heroHealth <= 0.3f)
		{
			dangerPanel.BringIn();
		}
		else
		{
			dangerPanel.Dismiss();
		}
	}

	public void GameSummaryPrompt(bool win)
	{
		Time.timeScale = 0f;
		LevelCompletePrompt = true;
		tutorialManager.Hide();
		hideGenHelpPanel();
		int num = _DropItemManager.ReturnSunPower();
		int num2 = 0;
		SingletonMonoBehaviour<AudioManager>.Instance.StopBackgroundMusic();
		if (win)
		{
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
			{
				_UserProfileManager.setFinishedLevel_NMode(_UserProfileManager.getGameLevel_NMode());
			}
			else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
			{
				_UserProfileManager.setFinishedLevel_HalloweenMode(_UserProfileManager.getGameLevel_HalloweenMode());
			}
			Debug.Log("******WIN*********");
			victoryPanel.BringIn();
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL)
			{
				victoryPanel.transform.Find("Door Exit").gameObject.SetActiveRecursivelyLegacy(false);
			}
			if (victoryAudioClip != null)
			{
				SingletonMonoBehaviour<AudioManager>.Instance.Play(victoryAudioClip);
			}
			UIButton component = victoryPanel.transform.Find("Continue").GetComponent<UIButton>();
			UIButton component2 = CollectedCoinValue.GetComponent<UIButton>();
			UIButton component3 = AwardedCoinValue.GetComponent<UIButton>();
			UIButton component4 = TotalCoinsValue.GetComponent<UIButton>();
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE && _UserProfileManager.getGameLevel_NMode() >= _UserProfileManager.getMaxLevel_NMode())
			{
				component.SetValueChangedDelegate(GoToEndingScene);
			}
			else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN && _UserProfileManager.getGameLevel_HalloweenMode() >= _UserProfileManager.getMaxLevel_HalloweenMode())
			{
				component.SetValueChangedDelegate(GoToEndingScene);
			}
			else if (_UserProfileManager.getGameLevel() >= _UserProfileManager.getMaxLevel())
			{
				component.SetValueChangedDelegate(GoToEndingScene);
			}
			else
			{
				component.SetValueChangedDelegate(OnBackToUpgradeScene);
			}
			component.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			int num3 = _KillEnemiesManager.ReturnAward();
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL && _UserProfileManager.getIsReachedMaxLevel() == 1)
			{
				num3 = 0;
			}
			num2 = num3 + num;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Hashtable hashtable = new Hashtable();
			int levelNumber = levelManager.GetLevelNumber();
			dictionary.Add("Collected Coins", num);
			hashtable.Add("Collected Coins", num);
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
			{
				//MunerisController.Instance.ReportEvent("Completed Night Mode Level " + _UserProfileManager.getGameLevel_NMode(), hashtable);
			}
			else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
			{
				//MunerisController.Instance.ReportEvent("Completed Halloween Mode Level " + _UserProfileManager.getGameLevel_HalloweenMode(), hashtable);
			}
			else
			{
//				MunerisController.Instance.ReportEvent("Completed Stage " + levelNumber, hashtable);
			}
			int totalCoinsSpent = _UserProfileManager.getTotalCoinsSpent();
			dictionary.Clear();
			hashtable.Clear();
			dictionary.Add("Spent Coins", totalCoinsSpent);
			hashtable.Add("Spent Coins", totalCoinsSpent);
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL)
			{
				//MunerisController.Instance.ReportEvent("Spent SunPower when beat stage " + levelNumber, hashtable);
			}
			StartCoroutine(CountCoin(component2, num, null));
			StartCoroutine(CountCoin(component3, num3, null));
			StartCoroutine(CountCoin(component4, num2, StopCountVictory));
			int gameLevel = _UserProfileManager.getGameLevel();
			Debug.Log("******currentLevel: " + gameLevel);
			Debug.Log("******currentLevel Award: " + num3);
			int num4 = gameLevel + 1;
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
			{
				_UserProfileManager.setGameLevel_NMode(_UserProfileManager.getGameLevel_NMode() + 1);
			}
			else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
			{
				_UserProfileManager.setGameLevel_HalloweenMode(_UserProfileManager.getGameLevel_HalloweenMode() + 1);
			}
			else
			{
				_UserProfileManager.setGameLevel(num4);
			}
			Debug.Log("******promotedLevel: " + num4);
			if (_UserProfileManager.getGameLevel() != 3)
			{
				Debug.Log(">>>>SHOW INTERSTITIAL AD - After finish level.");
//				MunerisController.Instance.ReportInterstitialAd();
			}
		}
		else
		{
			Debug.Log("******LOSE*********");
			destroyedPanel.BringIn();
			if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL)
			{
				destroyedPanel.transform.Find("Door Exit").gameObject.SetActiveRecursivelyLegacy(false);
			}
			if (defeatAudioClip != null)
			{
				SingletonMonoBehaviour<AudioManager>.Instance.Play(defeatAudioClip);
			}
			UIButton component5 = destroyedPanel.transform.Find("Continue").GetComponent<UIButton>();
			UIButton component6 = DestroyPanelCoin.GetComponent<UIButton>();
			component5.SetValueChangedDelegate(OnBackToUpgradeScene);
			component5.SetControlState(UIButton.CONTROL_STATE.DISABLED);
			num2 = num;
			StartCoroutine(CountCoin(component6, num2, StopCountLose));
		}
		_UserProfileManager.addCoin(num2);
	}

	private IEnumerator CountCoin(UIButton label, int value, Action callback)
	{
		float currentValue = Mathf.Floor(value / 2);
		while (!countEnd && currentValue < (float)value)
		{
			currentValue = ((value > 1000) ? (currentValue + 102f) : ((value > 150) ? (currentValue + 8f) : ((value <= 50) ? (currentValue + 1f) : (currentValue + 3f))));
			label.Text = currentValue.ToString();
			yield return 1;
		}
		label.Text = value.ToString();
		if (callback != null)
		{
			callback();
		}
	}

	private void StopCountVictory()
	{
		countEnd = true;
		UIButton component = victoryPanel.transform.Find("Continue").GetComponent<UIButton>();
		component.SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}

	private void StopCountLose()
	{
		countEnd = true;
		UIButton component = destroyedPanel.transform.Find("Continue").GetComponent<UIButton>();
		component.SetControlState(UIButton.CONTROL_STATE.NORMAL);
	}

	private void GoToEndingScene(IUIObject obj)
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		loadingManagerInstance.setSceneId("FD_Ending");
		SceneManager.LoadScene("Loading");
	}

	private void HandleAskForReviewDontAskAgainEvent()
	{
		PlayerPrefs.SetInt("RatingPopupPercentage", 0);
		OnBackToUpgradeScene(null);
	}

	private void HandleAskForReviewRemindMeLaterEvent()
	{
		int @int = PlayerPrefs.GetInt("RatingPopupPercentage");
		@int = ((@int >= 100) ? 50 : ((@int >= 50) ? 20 : 0));
		PlayerPrefs.SetInt("RatingPopupPercentage", @int);
		OnBackToUpgradeScene(null);
	}

	private void HandleAskForReviewWillOpenMarketEvent()
	{
		PlayerPrefs.SetInt("RatingPopupPercentage", 0);
		OnBackToUpgradeScene(null);
	}

	private void HandleAlertButtonClicked(string title)
	{
		Debug.Log("alertButtonClicked: " + title);
		switch (title)
		{
		case "OK":
			Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=" + _UserProfileManager.getAppID() + "&pageNumber=0&sortOrdering=1&type=Purple+Software&mt=8");
			PlayerPrefs.SetInt("RatingPopupPercentage", 0);
			OnBackToUpgradeScene(null);
			break;
		case "Don't Ask Again":
			PlayerPrefs.SetInt("RatingPopupPercentage", 0);
			OnBackToUpgradeScene(null);
			break;
		case "Cancel":
		{
			int @int = PlayerPrefs.GetInt("RatingPopupPercentage");
			@int = ((@int >= 100) ? 50 : ((@int >= 50) ? 20 : 0));
			PlayerPrefs.SetInt("RatingPopupPercentage", @int);
			OnBackToUpgradeScene(null);
			break;
		}
		}
	}

	private void HandlePromptCancelled()
	{
		Debug.Log("promptCancelled");
	}

	private void OnBackToUpgradeScene(IUIObject obj)
	{
		LoadingManager loadingManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LoadingManagerInstance;
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE || _UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			loadingManagerInstance.setSceneId("FD_Preparation");
			SceneManager.LoadScene("Loading");
			return;
		}
		if (!PlayerPrefs.HasKey("RatingPopupPercentage"))
		{
			PlayerPrefs.SetInt("RatingPopupPercentage", 100);
		}
		int @int = PlayerPrefs.GetInt("RatingPopupPercentage");
		if (_UserProfileManager.getGameLevel() <= 1)
		{
			Retry();
			return;
		}
		if (_UserProfileManager.getGameLevel() == 5 && !showPromptToRate)
		{
			showPromptToRate = true;
			if (@int >= 100)
			{
				Debug.Log("bundleID:" + _UserProfileManager.getBundleId());
				if (_UserProfileManager.getAndroidMarket() == "google")
				{
					EtceteraAndroid.askForReviewNow("Rate Us", "Any comment is welcome, thank you.", _UserProfileManager.getBundleId());
				}
				return;
			}
		}
		else if (_UserProfileManager.getGameLevel() == 10)
		{
			if (@int >= 50)
			{
				int num = UnityEngine.Random.Range(0, 2);
				if (num <= 0)
				{
					Debug.Log("bundleID:" + _UserProfileManager.getBundleId());
					if (_UserProfileManager.getAndroidMarket() == "google")
					{
						EtceteraAndroid.askForReviewNow("Rate Us", "Any comment is welcome, thank you.", _UserProfileManager.getBundleId());
					}
					return;
				}
				PlayerPrefs.SetInt("RatingPopupPercentage", 20);
			}
		}
		else if (_UserProfileManager.getGameLevel() == 15 && @int >= 20)
		{
			int num2 = UnityEngine.Random.Range(0, 5);
			if (num2 <= 0)
			{
				Debug.Log("bundleID:" + _UserProfileManager.getBundleId());
				if (_UserProfileManager.getAndroidMarket() == "google")
				{
					EtceteraAndroid.askForReviewNow("Rate Us", "Any comment is welcome, thank you.", _UserProfileManager.getBundleId());
				}
				return;
			}
			PlayerPrefs.SetInt("RatingPopupPercentage", 0);
		}
		loadingManagerInstance.setSceneId("FD_Upgrade");
		SceneManager.LoadScene("Loading");
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_EventType eventType = gameEvent.GetEventType();
		if (eventType == StarStrike_EventType.SPAWN_WAVE)
		{
			displayTimer.Reset();
		}
	}

	public GameObject GetUnit(int index)
	{
		return warriorPrefabs[index];
	}

	public void GarfieldMaterial(bool isInvicible)
	{
		if (isInvicible)
		{
			garfieldMaterial.mainTexture = Resources.Load("Textures/Normal/G01_Garfield_gold") as Texture2D;
		}
		else
		{
			garfieldMaterial.mainTexture = Resources.Load("Textures/Normal/G01_Garfield") as Texture2D;
		}
	}

	private void DrinkPotion()
	{
		Debug.Log(">>> Drink potion >>>");
		hero.HealMaxDamage();
		if (invincibleHeroCount == 0)
		{
			hero.StartInvincible(1f);
		}
		_UserProfileManager.addItemCount("item01_lvl", -1);
		checkPotionButton();
		if (tutorialManager.IsShowing && tutorialManager.CurrentPanelIndex == 12)
		{
			tutorialManager.ShowNext();
		}
	}

	private void DrinkInviciblePotion()
	{
		hero.StartInvincible(15f);
		_UserProfileManager.addItemCount("item04_lvl", -1);
		checkPotionButton();
		StartInvicible(true);
		if (tutorialManager.IsShowing && tutorialManager.CurrentPanelIndex == 15)
		{
			tutorialManager.ShowNext();
		}
	}

	private void StartInvicible(bool isStart)
	{
		if (isStart)
		{
			SingletonMonoBehaviour<AudioManager>.Instance.BackgroundMusic.Pause();
			potionButtons[3].SetControlState(UIButton.CONTROL_STATE.DISABLED);
			SpriteText component = potionButtons[3].transform.Find("Time").GetComponent<SpriteText>();
			invincibleHeroCount = 15;
			if (invincibleHeroCount < 0)
			{
				invincibleHeroCount = 0;
			}
			component.Text = "00:" + invincibleHeroCount;
			potionButtons[3].Text = string.Empty;
			StartCoroutine(countDownTime(1f));
		}
		else
		{
			SingletonMonoBehaviour<AudioManager>.Instance.BackgroundMusic.Play();
			potionButtons[3].SetControlState(UIButton.CONTROL_STATE.NORMAL);
			SpriteText component2 = potionButtons[3].transform.Find("Time").GetComponent<SpriteText>();
			component2.Text = string.Empty;
			checkPotionButton();
		}
	}

	public IEnumerator countDownTime(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		SpriteText timeTxt = potionButtons[3].transform.Find("Time").GetComponent<SpriteText>();
		invincibleHeroCount--;
		if (invincibleHeroCount < 10)
		{
			timeTxt.Text = "00:0" + invincibleHeroCount;
		}
		else
		{
			timeTxt.Text = "00:" + invincibleHeroCount;
		}
		if (invincibleHeroCount == 0)
		{
			StartInvicible(false);
		}
		else
		{
			StartCoroutine(countDownTime(1f));
		}
	}

	private void DrinkHealAll()
	{
		SetHealFactor(0.5f);
		SetHealAll(true);
		_UserProfileManager.addItemCount("item02_lvl", -1);
		checkPotionButton();
		if (tutorialManager.IsShowing && tutorialManager.CurrentPanelIndex == 13)
		{
			tutorialManager.ShowNext();
		}
	}

	private void DrinkInstantPopcorn()
	{
		int num = 30;
		if (tutorialManager.IsShowing && tutorialManager.CurrentPanelIndex == 14)
		{
			mineralProducer.AddMinerals(mineralProducer.MaxMinerals);
			tutorialManager.ShowNext();
		}
		else
		{
			mineralProducer.AddMinerals(num);
		}
		_UserProfileManager.addItemCount("item03_lvl", -1);
		checkPotionButton();
	}

	public float GetHealFactor()
	{
		return _HealFactor;
	}

	public void SetHealFactor(float heal)
	{
		_HealFactor = heal;
	}

	private void UpgradeWater()
	{
		mineralProducer.Upgrade();
		GameObject gameObject = UnityEngine.Object.Instantiate(waterProgressPrefabs[mineralProducer.Level]);
		gameObject.name = waterProgress.name;
		gameObject.transform.parent = waterProgress.transform.parent;
		gameObject.transform.localScale = waterProgress.transform.localScale;
		gameObject.transform.localRotation = waterProgress.transform.localRotation;
		gameObject.transform.localPosition = waterProgress.transform.localPosition;
		UnityEngine.Object.Destroy(waterProgress.gameObject);
		waterProgress = gameObject.GetComponent<UIButton>();
		waterBar = waterProgress.transform.Find("Bar").gameObject;
		waterButton.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		waterProgress.SetControlState(UIButton.CONTROL_STATE.DISABLED);
		if (mineralProducer.Level < 3)
		{
			waterButton.Text = "Lv." + (mineralProducer.Level + 1);
		}
		else
		{
			waterButton.Text = "MAX";
		}
		waterProgress.Text = mineralProducer.smoothBar.GetCurrentValue().ToString();
		if (tutorialManager.IsShowing && tutorialManager.CurrentPanelIndex == 9)
		{
			tutorialManager.ShowNext();
		}
	}

	private void OnDestroy()
	{
		Time.timeScale = 1f;
	}

	public void OnArmyUnitDead(StarStrike_ArmyUnit armyUnit)
	{
		Debug.Log("army unit dead: " + armyUnit.unitName);
		if (actions != null && actions.Count > 0 && actions.ContainsKey(armyUnit.unitName))
		{
			StarStrike_ConditionalAction starStrike_ConditionalAction = actions[armyUnit.unitName];
			if (starStrike_ConditionalAction != null)
			{
				((StarStrike_UnitCreatorAction)starStrike_ConditionalAction).disabled = false;
			}
		}
	}

	public bool ReturnLevelComplete()
	{
		return LevelCompletePrompt;
	}

	public void SetPlayerLost()
	{
		_PlayerLost = true;
	}

	public bool ReturnPlayerLost()
	{
		return _PlayerLost;
	}

	public void genUnit()
	{
		Debug.Log("============genUnit==========");
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(spellPrefabs[3], StartPostions[0], spellPrefabs[3].transform.rotation);
		Special_FireBall component = gameObject.GetComponent<Special_FireBall>();
		component.setGenHelpUnit();
		Invoke("showStage3Help", 1.5f);
	}

	private void showStage3Help()
	{
		SpriteText component = genHelpPanel.transform.Find("Caption").GetComponent<SpriteText>();
		tutorialManager.Hide();
		genHelpPanel.BringIn();
		if (component != null)
		{
			FontManager fontManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.FontManagerInstance;
			fontManagerInstance.SetBigFontMat(component);
			component.Text = langMan.getLangData("Stage3GenHelp");
		}
		_showStage3Help = true;
		Time.timeScale = 0f;
	}

	public void hideGenHelpPanel()
	{
		_showStage3Help = false;
		genHelpPanel.Dismiss();
	}

	public void SetHealAll(bool heal)
	{
		_HealAll = heal;
		if (heal)
		{
			Invoke("ResetHealAll", 0.1f);
		}
	}

	private void ResetHealAll()
	{
		_HealAll = false;
	}

	public bool CheckHealAll()
	{
		return _HealAll;
	}
}
