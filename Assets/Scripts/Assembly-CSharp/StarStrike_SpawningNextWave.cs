using System.Collections.Generic;
using UnityEngine;

internal class StarStrike_SpawningNextWave : StarStrike_LevelManagerStateAdapter
{
	private const float SPAWN_INTERVAL_TIME = 1.65f;

	private const float X_SPAWN_POINT = 50f;

	private const float MIN_Z_SPAWN_POINT = 0f;

	private const float MAX_Z_SPAWN_POINT = 100f;

	private StarStrike_LevelDefinition levelDefinition;

	private GameObject[] armyUnitPrefabs;

	private StarStrike_CountdownTimer spawnIntervalTimer;

	private StarStrike_WaveDefinition currentWave;

	private StarStrike_ArmyDefinition currentArmy;

	private int armySpawnCount;

	private StarStrike_WaveResolver waveResolver;

	private List<Queue<int>> depths;

	private List<int> lanes;

	public StarStrike_SpawningNextWave(StarStrike_LevelDefinition levelDefinition, GameObject[] armyUnitPrefabs, StarStrike_WaveResolver waveResolver)
	{
		this.levelDefinition = levelDefinition;
		this.armyUnitPrefabs = armyUnitPrefabs;
		this.waveResolver = waveResolver;
		spawnIntervalTimer = new StarStrike_CountdownTimer(1.65f);
		GenerateLanesAndDepths();
	}

	private void GenerateLanesAndDepths()
	{
		lanes = new List<int>(GenerateRandomLanes());
		depths = new List<Queue<int>>();
		for (int i = 0; i < lanes.Count; i++)
		{
			depths.Add(new Queue<int>(GenerateRandomNumbers(10)));
		}
	}

	private int[] GenerateRandomNumbers(int amount)
	{
		int[] array = new int[amount];
		array[0] = 0;
		for (int i = 1; i < array.Length; i++)
		{
			int num = Random.Range(0, i + 1);
			array[i] = array[num];
			array[num] = i;
		}
		return array;
	}

	private int[] GenerateRandomLanes()
	{
		int[] array = new int[26];
		array[0] = 0;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int j = 1; j < array.Length; j++)
		{
			switch (j)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 6:
			case 10:
			case 11:
			case 13:
			case 17:
			case 19:
			{
				int num4 = Random.Range(0, num + 1);
				array[j] = array[num4];
				array[num4] = j;
				num++;
				break;
			}
			case 5:
			case 7:
			case 8:
			case 14:
			case 15:
			case 16:
			case 18:
			case 22:
			case 24:
			{
				int num4 = Random.Range(9, num2 + 10);
				Debug.Log(num2 + " " + j + " " + num4);
				array[j] = array[num4];
				array[num4] = j;
				num2++;
				break;
			}
			case 9:
			case 12:
			case 20:
			case 21:
			case 23:
			case 25:
			case 26:
			{
				int num4 = Random.Range(18, num3 + 19);
				array[j] = array[num4];
				array[num4] = j;
				num3++;
				break;
			}
			}
		}
		return array;
	}

	public override void ProcessEvent(StarStrike_LevelManagerStateEvent stateEvent)
	{
		if (stateEvent == StarStrike_LevelManagerStateEvent.ON_PUSH)
		{
			UnmarkAsDone();
			StartSpawn();
			StartNextArmy();
		}
	}

	public override void Update()
	{
		if (IsDone())
		{
			return;
		}
		spawnIntervalTimer.Update();
		if (!spawnIntervalTimer.HasElapsed())
		{
			return;
		}
		int num = currentArmy.GetModel() - 1;
		GameObject gameObject = armyUnitPrefabs[num];
		int num2 = lanes[num];
		if (depths[num].Count <= 0)
		{
			depths[num] = new Queue<int>(GenerateRandomNumbers(10));
		}
		int num3 = depths[num].Dequeue();
		float z = num2 * 400 + num3 * 2;
		float y = (float)num2 * 0.07f + (float)num3 * 0.002f;
		Vector3 position = new Vector3(50f, y, z);
		Object.Instantiate(gameObject, position, gameObject.transform.rotation);
		armySpawnCount++;
		if (armySpawnCount == currentArmy.GetCount())
		{
			if (currentWave.HasNext())
			{
				StartNextArmy();
				return;
			}
			MarkAsDone();
			Debug.Log("=========All Wave Spawned=======");
		}
		else
		{
			spawnIntervalTimer.Reset();
		}
	}

	private void StartSpawn()
	{
		currentWave = waveResolver.ResolveNextWave(levelDefinition);
		currentWave.ResetIteration();
	}

	private void StartNextArmy()
	{
		StarStrike_Assertion.Assert(currentWave.HasNext(), "Wave definition should have a next army at this point.");
		currentArmy = currentWave.MoveToNext();
		armySpawnCount = 0;
		spawnIntervalTimer.Reset();
	}
}
