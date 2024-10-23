using System.Collections;
using UnityEngine;

public class StarStrike_JerryUnitControl : MonoBehaviour, StarStrike_UiRenderer, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private Vector3 hudScale;

	private IList buttonList;

	private static string MELEE_UNIT = "AstroMice";

	private static string RANGED_UNIT = "MobileTurret";

	private static string HEAVY_UNIT = "Healer";

	private static string EMP_SPECIAL = "EMP";

	private static string METEOR_SPECIAL = "Meteor";

	private static string ROCKET_GLOVE_SPECIAL = "RocketGlove";

	public Rect hudRect;

	public Texture2D hudImage;

	public Texture unitButtonGlow;

	public Texture cooldownOverlay;

	public GUIStyle astroMiceButtonStyle;

	public GUIStyle astroMiceDisabledButtonStyle;

	public Rect astroMiceButtonRect;

	public GameObject astroMicePrefab;

	public GUIStyle mobileTurretButtonStyle;

	public GUIStyle mobileTurrentDisabledButtonStyle;

	public Rect mobileTurretButtonRect;

	public GameObject mobileTurretPrefab;

	public GUIStyle roboSpikeButtonStyle;

	public GUIStyle roboSpikeDisabledButtonStyle;

	public Rect roboSpikeButtonRect;

	public GameObject roboSpikePrefab;

	public Texture specialAttackGlow;

	public GUIStyle empButtonStyle;

	public GUIStyle empDisabledButtonStyle;

	public Rect empButtonRect;

	public GameObject empPrefab;

	private static Vector3 EMP_START_POS = new Vector3(0f, 2f, -2f);

	public GUIStyle meteorButtonStyle;

	public GUIStyle meteorDisabledButtonStyle;

	public Rect meteorButtonRect;

	public GameObject meteorPrefab;

	public GUIStyle rocketGloveButtonStyle;

	public GUIStyle rocketGloveDisabledButtonStyle;

	public Rect rocketGloveRect;

	public GameObject rocketGlovePrefab;

	private static Vector3 ROCKET_GLOVE_START = new Vector3(-19f, 2f, 0f);

	public GUIStyle priceLabelStyle;

	public Rect astroMicePriceRect;

	private int astroMicePrice;

	public Rect mobileTurretPriceRect;

	private int mobileTurretPrice;

	public Rect roboSpikePriceRect;

	private int roboSpikePrice;

	public Rect posAstroMice;

	public Rect posMobileTurret;

	public Rect posRoboSpike;

	public Rect posEmpRect;

	public Rect posMeteorRect;

	public Rect posRocketGlovesRect;

	public Texture unitUnavailableTexture;

	public Texture specialAttackUnavailableTexture;

	private StarStrike_GameStateManager gameStateManager;

	private StarStrike_MineralProducer mineralProducer;

	private StarStrike_ArmyUnitConfiguration armyUnitConfiguration;

	private StarStrike_SpecialAttackConfiguration specialAttackConfiguration;

	private int previousLevel;

	private StarStrike_LevelManager levelManager;

	private StarStrike_CooldownButton astroMiceButton;

	private StarStrike_CooldownButton mobileTurretButton;

	private StarStrike_CooldownButton roboSpikeButton;

	private StarStrike_CooldownButton empButton;

	private StarStrike_CooldownButton meteorButton;

	private StarStrike_CooldownButton rocketGloveButton;

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		hudScale = new Vector3((float)Screen.width / 800f, (float)Screen.height / 600f, 1f);
		mineralProducer = GameObject.Find("MineralProducer").GetComponent<StarStrike_MineralProducer>();
		StarStrike_Assertion.Assert(mineralProducer, "mineralProducer can't be null");
		buttonList = new ArrayList();
		armyUnitConfiguration = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		specialAttackConfiguration = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		astroMicePrice = GetPrice(MELEE_UNIT);
		mobileTurretPrice = GetPrice(RANGED_UNIT);
		roboSpikePrice = GetPrice(HEAVY_UNIT);
		astroMiceButton = CreateUnitCreatorButton(MELEE_UNIT, astroMicePrefab, astroMiceButtonStyle, astroMiceDisabledButtonStyle, astroMiceButtonRect);
		mobileTurretButton = CreateUnitCreatorButton(RANGED_UNIT, mobileTurretPrefab, mobileTurretButtonStyle, mobileTurrentDisabledButtonStyle, mobileTurretButtonRect);
		roboSpikeButton = CreateUnitCreatorButton(HEAVY_UNIT, roboSpikePrefab, roboSpikeButtonStyle, roboSpikeDisabledButtonStyle, roboSpikeButtonRect);
		empButton = CreateInstantiatorButton(EMP_SPECIAL, EMP_START_POS, empPrefab, empButtonStyle, empDisabledButtonStyle, empButtonRect);
		meteorButton = CreateInstantiatorButton(METEOR_SPECIAL, Vector3.zero, meteorPrefab, meteorButtonStyle, meteorDisabledButtonStyle, meteorButtonRect);
		rocketGloveButton = CreateInstantiatorButton(ROCKET_GLOVE_SPECIAL, ROCKET_GLOVE_START, rocketGlovePrefab, rocketGloveButtonStyle, rocketGloveDisabledButtonStyle, rocketGloveRect);
		previousLevel = 0;
		levelManager = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.AssertNotNull(levelManager, "levelManager");
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private int GetPrice(string unitName)
	{
		StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(unitName);
		return int.Parse(unitDefinition.GetAttributeValue("price"));
	}

	private StarStrike_CooldownButton CreateUnitCreatorButton(string unitName, GameObject prefab, GUIStyle buttonStyle, GUIStyle disabledButtonStyle, Rect buttonRect)
	{
		Debug.Log("============unitName: " + unitName);
		StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(unitName);
		int price = int.Parse(unitDefinition.GetAttributeValue("price"));
		float cooldownTime = float.Parse(unitDefinition.GetAttributeValue("cooldown"));
		StarStrike_ConditionalAction action = new StarStrike_UnitCreatorAction(mineralProducer, prefab, price);
		return new StarStrike_CooldownButton(buttonStyle, disabledButtonStyle, cooldownOverlay, buttonRect, cooldownTime, action);
	}

	private StarStrike_CooldownButton CreateUnitCreatorButton(string unitName, GameObject prefab, GUIStyle buttonStyle, Rect buttonRect)
	{
		return CreateUnitCreatorButton(unitName, prefab, buttonStyle, buttonStyle, buttonRect);
	}

	private StarStrike_CooldownButton CreateInstantiatorButton(string specialAttackName, Vector3 position, GameObject prefab, GUIStyle buttonStyle, GUIStyle disabledButtonStyle, Rect buttonRect)
	{
		StarStrike_ObjectDefinition definition = specialAttackConfiguration.GetDefinition(specialAttackName);
		float cooldownTime = float.Parse(definition.GetAttributeValue("cooldown"));
		StarStrike_ConditionalAction action = new StarStrike_PrefabInstatiatorAction(prefab, position);
		return new StarStrike_CooldownButton(buttonStyle, disabledButtonStyle, cooldownOverlay, buttonRect, cooldownTime, action);
	}

	private void RepopulateButtonList()
	{
		buttonList.Clear();
		TryAddUnitButton(MELEE_UNIT, astroMiceButton);
		TryAddUnitButton(RANGED_UNIT, mobileTurretButton);
		TryAddUnitButton(HEAVY_UNIT, roboSpikeButton);
		TryAddSpecialAttackButton(EMP_SPECIAL, empButton);
		TryAddSpecialAttackButton(METEOR_SPECIAL, meteorButton);
		TryAddSpecialAttackButton(ROCKET_GLOVE_SPECIAL, rocketGloveButton);
	}

	private void TryAddUnitButton(string itemName, StarStrike_CooldownButton button)
	{
		if (levelManager.IsEnabledInCurrentLevel(itemName))
		{
			button.ResetCooldown();
			buttonList.Add(button);
		}
		else
		{
			buttonList.Add(new StarStrike_TextureHudElement(unitUnavailableTexture, button.GetRect()));
		}
	}

	private void TryAddSpecialAttackButton(string itemName, StarStrike_CooldownButton button)
	{
		if (levelManager.IsEnabledInCurrentLevel(itemName))
		{
			button.ResetCooldown();
			buttonList.Add(button);
		}
		else
		{
			buttonList.Add(new StarStrike_TextureHudElement(specialAttackUnavailableTexture, button.GetRect()));
		}
	}

	private void Update()
	{
		int levelNumber = levelManager.GetLevelNumber();
		if (previousLevel != levelNumber)
		{
			RepopulateButtonList();
			previousLevel = levelNumber;
		}
		UpdateElements();
	}

	private void UpdateElements()
	{
		if (!gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME))
		{
			return;
		}
		foreach (StarStrike_HudElement button in buttonList)
		{
			button.Update();
		}
	}

	public void RenderUI()
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, hudScale);
		foreach (StarStrike_HudElement button in buttonList)
		{
			button.OnGUI();
		}
		GUI.DrawTexture(hudRect, hudImage);
		RenderButtonGlow(astroMiceButton, unitButtonGlow);
		RenderButtonGlow(mobileTurretButton, unitButtonGlow);
		RenderButtonGlow(roboSpikeButton, unitButtonGlow);
		RenderButtonGlow(empButton, specialAttackGlow);
		RenderButtonGlow(meteorButton, specialAttackGlow);
		RenderButtonGlow(rocketGloveButton, specialAttackGlow);
		GUI.Label(astroMicePriceRect, astroMicePrice.ToString(), priceLabelStyle);
		GUI.Label(mobileTurretPriceRect, mobileTurretPrice.ToString(), priceLabelStyle);
		GUI.Label(roboSpikePriceRect, roboSpikePrice.ToString(), priceLabelStyle);
	}

	private static void RenderButtonGlow(StarStrike_CooldownButton button, Texture glowTexture)
	{
		if (button.IsClickable())
		{
			Rect rect = button.GetRect();
			Rect position = default(Rect);
			position.x = rect.x + (rect.width - (float)glowTexture.width) / 2f;
			position.y = rect.y + (rect.height - (float)glowTexture.height) / 2f;
			position.width = glowTexture.width;
			position.height = glowTexture.height;
			GUI.DrawTexture(position, glowTexture);
		}
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
	}
}
