using UnityEngine;

public class StarStrike_MineralProducer : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private float currentMinerals;

	private StarStrike_GameStateManager gameStateManager;

	public GUIStyle mineralNumberStyle;

	public Rect mineralNumberLabelRect;

	public Rect mineralBarRect;

	public Texture mineralBar;

	public Rect mineralValueBarRect;

	public Texture mineralValueTexture;

	public StarStrike_SmoothBar smoothBar;

	public Texture backgroundTexture;

	private int level;

	private float[] mineralsPerSecond = new float[4];

	private int[] maxMinerals = new int[4];

	private int[] upgradeCosts = new int[4];

	public int Level
	{
		get
		{
			return level;
		}
	}

	public float CurrentMinerals
	{
		get
		{
			return currentMinerals;
		}
		set
		{
			currentMinerals = value;
		}
	}

	public int MaxMinerals
	{
		get
		{
			return maxMinerals[level];
		}
	}

	public bool CanUpgrade()
	{
		if (upgradeCosts.Length > level)
		{
			return currentMinerals >= (float)upgradeCosts[level];
		}
		return false;
	}

	public void Upgrade()
	{
		if (CanUpgrade())
		{
			currentMinerals -= upgradeCosts[level];
			smoothBar.SetValue((int)currentMinerals);
			level++;
			smoothBar.maxValue = maxMinerals[level];
		}
	}

	private void Awake()
	{
		FD_ForrestConfiguration component = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
		level = component.GetSmithCurrentLevel();
		for (int i = 0; i < 4; i++)
		{
			FD_ObjectLevelDefinition smithPower = component.GetSmithPower(i);
			Debug.Log(smithPower.GetAttributeValue("mineralsPerSecond"));
			mineralsPerSecond[i] = float.Parse(smithPower.GetAttributeValue("mineralsPerSecond"));
;           maxMinerals[i] = int.Parse(smithPower.GetAttributeValue("maxMinerals"));
			upgradeCosts[i] = int.Parse(smithPower.GetAttributeValue("mineralToLevelUp"));
		}
		smoothBar = new StarStrike_SmoothBar(mineralValueTexture, backgroundTexture, mineralValueBarRect, maxMinerals[level], 0.25f, SmoothBarDirection.RIGHT);
	}

	private void Start()
	{
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		smoothBar.ClearUpdateValues();
		smoothBar.SetValue((int)currentMinerals);
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		if (gameStateManager != null && gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME) && !(currentMinerals >= (float)maxMinerals[level]))
		{
			float num = currentMinerals + mineralsPerSecond[level] * Time.deltaTime;
			if (num < (float)maxMinerals[level])
			{
				currentMinerals = num;
			}
			else
			{
				currentMinerals = maxMinerals[level];
			}
			smoothBar.ClearUpdateValues();
			smoothBar.SetValue((int)currentMinerals);
		}
	}

	public void ResetMinerals()
	{
		currentMinerals = 0f;
	}

	public bool CanAfford(int amount)
	{
		return currentMinerals >= (float)amount;
	}

	public void Use(int amount)
	{
		StarStrike_Assertion.Assert(CanAfford(amount), "Can't afford the specified amount.");
		currentMinerals -= amount;
		smoothBar.ClearUpdateValues();
		smoothBar.SetValue((int)currentMinerals);
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		StarStrike_EventType eventType = gameEvent.GetEventType();
		if (eventType == StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED)
		{
			currentMinerals = 0f;
			smoothBar.ClearUpdateValues();
			smoothBar.SetValue((int)currentMinerals);
		}
	}

	public void AddMinerals(int num)
	{
		currentMinerals += num;
	}
}
