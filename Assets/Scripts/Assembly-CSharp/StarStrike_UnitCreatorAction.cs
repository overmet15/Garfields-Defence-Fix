using System.Collections.Generic;
using UnityEngine;

internal class StarStrike_UnitCreatorAction : StarStrike_ConditionalAction
{
	public const float X_SPAWN_POSITION = -75f;

	private const float MIN_Z_SPAWN_POSITION = 0f;

	private const float MAX_Z_SPAWN_POSITION = 10f;

	private StarStrike_MineralProducer mineralProducer;

	private GameObject prefabToCreate;

	private int price;

	private StarStrike_GameStateManager gameStateManager;

	private static bool _Weaken;

	private static List<Queue<int>> depths;

	public static List<int> lanes;

	private int index;

	public bool disabled;

	public StarStrike_UnitCreatorAction(StarStrike_MineralProducer mineralProducer, GameObject prefabToCreate, int price)
		: this(mineralProducer, prefabToCreate, price, 0)
	{
	}

	public StarStrike_UnitCreatorAction(StarStrike_MineralProducer mineralProducer, GameObject prefabToCreate, int price, int index)
	{
		this.mineralProducer = mineralProducer;
		StarStrike_Assertion.Assert(this.mineralProducer != null, "The mineral producer can't be null.");
		this.prefabToCreate = prefabToCreate;
		this.price = price;
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		this.index = index;
		if (lanes == null || depths == null)
		{
			GenerateLanesAndDepths();
		}
	}

	public static void Reset()
	{
		lanes = null;
		depths = null;
	}

	private void GenerateLanesAndDepths()
	{
		lanes = new List<int>(GenerateRandomNumbers(12));
		depths = new List<Queue<int>>();
		for (int i = 0; i < lanes.Count; i++)
		{
			depths.Add(new Queue<int>(GenerateRandomNumbers(100)));
		}
	}

	private static int[] GenerateRandomNumbers(int amount)
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

	public bool CanBeExecuted()
	{
		bool flag = gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME);
		bool flag2 = mineralProducer.CanAfford(price);
		return flag && flag2 && !disabled;
	}

	public void Execute()
	{
		mineralProducer.Use(price);
		Summon(prefabToCreate, new Vector3(-75f, 0f, 0f), index);
	}

	public static void Summon(GameObject prefabToCreate, Vector3 position, int index)
	{
		Summon(prefabToCreate, position, index, false);
	}

	public static void Summon(GameObject prefabToCreate, Vector3 position, int index, bool weaken)
	{
		int num = lanes[index];
		if (depths[index].Count <= 0)
		{
			depths[index] = new Queue<int>(GenerateRandomNumbers(10));
		}
		int num2 = depths[index].Dequeue();
		float z = 200 + num * 400 + num2 * 2;
		float y = 0.02f + (float)num * 0.07f + (float)num2 * 0.002f;
		Vector3 position2 = new Vector3(position.x, y, z);
		Debug.Log("prefabToCreate: " + prefabToCreate);
		GameObject gameObject = Object.Instantiate(prefabToCreate, position2, prefabToCreate.transform.rotation) as GameObject;
		Debug.Log("obj: " + gameObject);
	}
}
