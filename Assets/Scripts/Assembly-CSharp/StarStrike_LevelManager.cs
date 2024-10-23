using System.Collections;
using Outblaze;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarStrike_LevelManager : MonoBehaviour, StarStrike_EventListener<StarStrike_EventType, StarStrike_AttachmentKey>
{
	private const float AFTER_BASE_DESTROYED_WAIT_TIME = 3f;

	public TextAsset levelXmlConfig;

	public GameObject[] enemyUnitPrefabs;

	public Material[] enemyUnitMaterials;

	private StarStrike_GameStateManager gameStateManager;

	private StarStrike_LevelDefinitionlManager definitionManager;

	private StarStrike_LevelDefinition currentLevel;

	private int levelNumber;

	private IList observerList;

	private StarStrike_LevelManagerStateStack stateStack;

	private StarStrike_LevelManagerState waitBeforeFirstWaveState;

	private StarStrike_LevelManagerState waitingState;

	private StarStrike_LevelManagerState waitingLevelEndState;

	private StarStrike_LevelManagerState spawnNextWaveState;

	private StarStrike_LevelManagerState spawnRandomWaveState;

	private StarStrike_LevelManagerState waitBetweenWaves;

	private StarStrike_CountdownTimer waitLoadJerryLoseTimer;

	private StarStrike_CountdownTimer waitAfterTomDestroyedTimer;

	private UserProfileManager _UserProfileManager;

	public GameObject EZGUI;

	private InGameUIDemo _InGameUIDemo;

	private DropItemsManager _DropItemManager;

	private void Start()
	{
		_DropItemManager = GameObject.Find("DropItemsManager").GetComponent<DropItemsManager>();
		_UserProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		_InGameUIDemo = EZGUI.transform.GetComponent<InGameUIDemo>();
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		observerList = new ArrayList();
		stateStack = new StarStrike_LevelManagerStateStack();
		waitingState = new StarStrike_WaitingState();
		waitingLevelEndState = new StarStrike_WaitingLevelEnd();
		stateStack.Push(waitingState);
		StarStrike_GameConfiguration component = GameObject.Find("GameConfiguration").GetComponent<StarStrike_GameConfiguration>();
		float waitTime = 1;

		if (float.TryParse(component.GetAttribute("firstWaveWaitTime"), out float e))
			waitTime = e;
		else Debug.Log("Couldnt parse float");
		waitBeforeFirstWaveState = new StarStrike_TimedWaitState(waitTime);
		definitionManager = new StarStrike_LevelDefinitionlManager(levelXmlConfig.text);
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			levelNumber = _UserProfileManager.getGameLevel_NMode() - 1;
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
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			levelNumber = _UserProfileManager.getGameLevel_HalloweenMode() - 1;
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
			levelNumber = _UserProfileManager.getGameLevel() - 1;
			for (int k = 0; k < 26; k++)
			{
				if (_UserProfileManager.ChineseNewYear)
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
				else
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
		}
		Debug.Log("=====Furry Log Event===== ");
		string eventName = "Start Stage " + levelNumber;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("stage", 1);
//		MunerisController.Instance.ReportEvent(eventName, hashtable);
		_DropItemManager.GetCurrentLevel(_UserProfileManager.getGameLevel());
		PrepareNextLevel();
		waitLoadJerryLoseTimer = new StarStrike_CountdownTimer(3f);
		waitAfterTomDestroyedTimer = new StarStrike_CountdownTimer(3f);
		StarStrike_EventManagerInstance.GetInstance().AddListener(this);
	}

	private void Update()
	{
		CheckInGame();
		CheckWaitLoadJerryLose();
		CheckTomDestroyedWait();
	}

	private void CheckInGame()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME))
		{
			UpdateStateStack();
			CheckSpawnNextWave();
			CheckSpawnRandomWave();
		}
	}

	private void CheckWaitLoadJerryLose()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.WAIT_LOAD_JERRY_LOSE))
		{
			if (waitLoadJerryLoseTimer.HasElapsed())
			{
				_InGameUIDemo.GameSummaryPrompt(false);
				gameStateManager.Pop();
			}
			waitLoadJerryLoseTimer.Update();
		}
	}

	private void CheckTomDestroyedWait()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.WAIT_AFTER_TOM_DESTROYED))
		{
			if (waitAfterTomDestroyedTimer.HasElapsed())
			{
				gameStateManager.Pop();
				gameStateManager.Push(StarStrike_GameState.LEVEL_COMPLETE);
			}
			waitAfterTomDestroyedTimer.Update();
		}
	}

	private void UpdateStateStack()
	{
		if (stateStack.IsEmpty())
		{
			stateStack.Push(waitingState);
		}
		else
		{
			stateStack.Top().Update();
		}
		CleanStack();
	}

	private void CleanStack()
	{
		while (stateStack.Top().IsDone())
		{
			stateStack.Pop();
		}
	}

	private void CheckSpawnNextWave()
	{
		if (stateStack.Top() == waitingState && currentLevel != null && currentLevel.HasNextWave())
		{
			stateStack.Push(waitBetweenWaves);
			stateStack.Push(spawnNextWaveState);
			PostEventSpawnWave();
		}
	}

	private void CheckSpawnRandomWave()
	{
		if (stateStack.Top() == waitingLevelEndState)
		{
			stateStack.Push(waitBetweenWaves);
			stateStack.Push(spawnRandomWaveState);
			PostEventSpawnWave();
		}
	}

	private void PostEventSpawnWave()
	{
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.SPAWN_WAVE);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
	}

	private void TransitionToWaitLevelEnd()
	{
		stateStack.Clear();
		stateStack.Push(waitingLevelEndState);
	}

	private void PrepareNextLevel()
	{
		if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.NIGHT_MODE)
		{
			levelNumber = _UserProfileManager.getGameLevel_NMode();
		}
		else if (_UserProfileManager.getCurrentPlayMode() == InGamePlayMode.HALLOWEEN)
		{
			levelNumber = _UserProfileManager.getGameLevel_HalloweenMode();
		}
		else
		{
			levelNumber = _UserProfileManager.getGameLevel();
		}
		Debug.Log("@@@@@levelNumber PrepareNextLevel: " + levelNumber);
		currentLevel = definitionManager.MoveToNextLevel();
		currentLevel.ResetIterator();
		waitBetweenWaves = new StarStrike_WaitAfterSpawnWave(currentLevel.GetWaveTimeInterval());
		spawnNextWaveState = new StarStrike_SpawningNextWave(currentLevel, enemyUnitPrefabs, new StarStrike_NextWaveResolver());
		spawnRandomWaveState = new StarStrike_SpawningNextWave(currentLevel, enemyUnitPrefabs, new StarStrike_RandomWaveResolver());
		stateStack.Push(waitBeforeFirstWaveState);
		MinigameBridge.updateLevel(levelNumber);
		NotifyObservers(LevelManagerEvent.ON_CHANGE_LEVEL);
	}

	public bool IsEnabledInCurrentLevel(string itemName)
	{
		return currentLevel.HasEnabledItem(itemName);
	}

	public void ProcessEvent(StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent)
	{
		switch (gameEvent.GetEventType())
		{
		case StarStrike_EventType.TOM_BASE_DESTROYED:
			gameStateManager.Push(StarStrike_GameState.WAIT_AFTER_TOM_DESTROYED);
			waitAfterTomDestroyedTimer.Reset();
			break;
		case StarStrike_EventType.LEVEL_COMPLETE_CONFIRMED:
			stateStack.Clear();
			stateStack.Push(waitingState);
			if (definitionManager.HasNextLevel())
			{
				PrepareNextLevel();
			}
			else
			{
				gameStateManager.Push(StarStrike_GameState.GAME_COMPLETED);
			}
			break;
		case StarStrike_EventType.JERRY_BASE_DESTROYED:
			gameStateManager.Push(StarStrike_GameState.WAIT_LOAD_JERRY_LOSE);
			waitLoadJerryLoseTimer.Reset();
			break;
		}
	}

	private void LoadJerryLoseAnimation()
	{
		StarStrike_LevelManager component = GameObject.Find("LevelManager").GetComponent<StarStrike_LevelManager>();
		StarStrike_Assertion.Assert(component != null, "levelManager should not be null");
		Object.DontDestroyOnLoad(GameObject.Find("RetainedElements"));
		SceneManager.LoadScene("JerryLose");
	}

	public int GetLevelNumber()
	{
		return levelNumber;
	}

	public void AddObserver(StarStrike_LevelManagerObserver observer)
	{
		StarStrike_Assertion.Assert(!observerList.Contains(observer), "Can't add observer. The manager already has it.");
		observerList.Add(observer);
	}

	private void NotifyObservers(LevelManagerEvent managerEvent)
	{
		foreach (StarStrike_LevelManagerObserver observer in observerList)
		{
			observer.ProcessEvent(managerEvent);
		}
	}
}
