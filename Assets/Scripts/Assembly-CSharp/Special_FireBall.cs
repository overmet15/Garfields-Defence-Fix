using UnityEngine;

public class Special_FireBall : MonoBehaviour
{
	private const float STARTING_Y = 11f;

	private const float RIGHT_MOST_X = 16f;

	private const float LEFT_MOST_X = -20f;

	private const float START_MAX_X = -15f;

	private const float MIN_END_X_OFFSET = 1f;

	public float OffsetX;

	public float OffsetY;

	public float LifeTime;

	public string Definition;

	public float AnimationOffset;

	public float HitColliderOffset;

	public GameObject HitCollider;

	private GameObject unit;

	private GameObject Hero;

	private int _randomUnitIndex;

	private Transform thisTransform;

	private Transform viewTransform;

	private int damage;

	private Vector3 direction;

	private float velocity;

	private float rotationVelocity;

	private StarStrike_ArmyUnit ArmyUnit;

	private StarStrike_ArmyUnit _SummonUnit;

	private InGameUIDemo _InGameUIDemo;

	private bool _GenHelp;

	public int _helpNum;

	public int[] _helpUnit;

	public string helpUnitExample = "0 = Kangaroo, 1 = Deer, 2 = Bird, etc.";

	public string[] _ReinLevelComposition;

	private int _Level;

	private int _ReinUnitNum;

	private void Awake()
	{
		Hero = GameObject.Find("Hero");
		if (HitCollider != null)
		{
			HitCollider.SetActiveRecursivelyLegacy(false);
		}
		if (Definition == "Rein")
		{
			Debug.Log("*******Definition is Horn******** ");
			_InGameUIDemo = GameObject.Find("In Game EZGUI").GetComponent<InGameUIDemo>();
			StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
			FD_ObjectLevelDefinition currentLevel = component.GetCurrentLevel(Definition);
			Debug.Log("Horn level:" + currentLevel.GetLevelNum());
			Debug.Log("Horn summon percentage:" + currentLevel.GetAttributeValue("attack"));
			damage = int.Parse(currentLevel.GetAttributeValue("attack"));
			_Level = int.Parse(currentLevel.GetLevelNum());
		}
	}

	private void Start()
	{
		thisTransform = base.transform;
		Init();
	}

	public void Init()
	{
		ArmyUnit = Hero.transform.GetComponent<StarStrike_ArmyUnit>();
		Vector3 position = Hero.transform.position;
		Vector3 position2 = new Vector3(position.x + OffsetX, position.y + OffsetY, position.z);
		thisTransform.position = position2;
		if (Definition == "Rein")
		{
			Debug.Log(" Horn!!!!");
			if (!_GenHelp)
			{
				ArmyUnit.PlayHorn();
				int num = Random.Range(0, 99);
				Debug.Log(" damage ==> " + damage);
				Debug.Log(" random ==> " + num);
				if (num > damage)
				{
					Debug.Log(" <== HealAll ==> ");
					HealAll();
					base.transform.localScale = Vector3.zero;
					Object.Destroy(base.gameObject);
				}
				else
				{
					Debug.Log("AnimationOffset: " + AnimationOffset);
					Invoke("PlayAnimation", AnimationOffset);
					Debug.Log(" <== GenerateUnit ==> ");
					Invoke("GenerateUnit", 1f);
				}
			}
			else
			{
				Invoke("GenerateUnit", 1f);
				Invoke("PlayAnimation", AnimationOffset);
			}
		}
		else if (Definition == "Tornado" || Definition == "BeeStorm")
		{
			ArmyUnit.SpecialAttack("SP");
			Invoke("PlayAnimation", AnimationOffset);
		}
		else if (Definition == "FireBall" || Definition == "Eagle")
		{
			ArmyUnit.SpecialAttack("SP01");
			Invoke("PlayAnimation", AnimationOffset);
		}
	}

	private void HealAll()
	{
		_InGameUIDemo.SetHealFactor(0.1f);
		_InGameUIDemo.SetHealAll(true);
	}

	private void PlayAnimation()
	{
		Debug.Log("PlayAnimation!!");
		thisTransform.GetComponent<Animation>().Play();
		if (HitCollider != null)
		{
			Invoke("ShowCollider", HitColliderOffset);
		}
		Invoke("SelfDestroy", LifeTime);
	}

	private void SelfDestroy()
	{
		Object.Destroy(base.gameObject);
	}

	private void GenerateUnit()
	{
		int num = 0;
		if (_GenHelp)
		{
			int[] helpUnit = _helpUnit;
			foreach (int num2 in helpUnit)
			{
				Debug.Log("_helpUnit ==> " + num2);
			}
			Debug.Log("_helpNum ==> " + _helpNum);
			if (_helpNum > 0)
			{
				num = _helpUnit[_helpNum - 1];
				Debug.Log("****** _GenHelp Index with _helpNum: " + num);
			}
			unit = _InGameUIDemo.GetUnit(num);
			Debug.Log("****** _GenHelp Index: " + num);
			Debug.Log("****** unit: " + unit);
			StarStrike_UnitCreatorAction.Summon(unit, thisTransform.position, num, true);
			_helpNum--;
			if (_helpNum > 0)
			{
				Invoke("GenerateUnit", 0.2f);
			}
			return;
		}
		Debug.Log("==============Rein" + _Level + "===============");
		string text = _ReinLevelComposition[_Level - 1];
		Debug.Log("_ReinLevelComposition ====> " + text);
		int length = text.Length;
		Debug.Log("randMax ====> " + length);
		int num3;
		do
		{
			num3 = Random.Range(0, length);
			_ReinUnitNum = int.Parse(text.Substring(num3, 1));
			Debug.Log("++++++ REIN: unitID = " + num3);
			Debug.Log("_ReinUnitNum ====> " + _ReinUnitNum);
		}
		while (_ReinUnitNum <= 0);
		int[] array = new int[6] { 0, 1, 2, 3, 4, 7 };
		num3 = array[num3];
		Debug.Log("Real unitID ====> " + num3);
		unit = _InGameUIDemo.GetUnit(num3);
		StarStrike_UnitCreatorAction.Summon(unit, thisTransform.position, num, true);
		if (_ReinUnitNum > 1)
		{
			Invoke("GenerateAdditionUnit", 0.2f);
		}
	}

	private void GenerateAdditionUnit()
	{
		StarStrike_UnitCreatorAction.Summon(unit, thisTransform.position, _randomUnitIndex, true);
		_ReinUnitNum--;
		Debug.Log("GenerateAdditionUnit ====> " + _ReinUnitNum);
		if (_ReinUnitNum > 1)
		{
			Invoke("GenerateAdditionUnit", 0.2f);
		}
	}

	private void ShowCollider()
	{
		Debug.Log("ShowCollider ===> " + HitCollider);
		HitCollider.SetActiveRecursivelyLegacy(true);
	}

	public void setGenHelpUnit()
	{
		_GenHelp = true;
	}
}
