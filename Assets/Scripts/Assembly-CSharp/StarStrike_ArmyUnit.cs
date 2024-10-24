using System.Collections;
using Outblaze;
using UnityEngine;

public class StarStrike_ArmyUnit : MonoBehaviour, StarStrike_Targetable
{
	private const float UNIT_VELOCITY_PER_SPEED_POINT = 0.08f;

	private const float JERRY_MELEE_DEAL_DAMAGE_TIME = 11f / 30f;

	private const float JERRY_HEAVY_DEAL_DAMAGE_TIME = 13f / 30f;

	private const float TOM_MELEE_DEAL_DAMAGE_TIME = 2f / 15f;

	private const float TOM_HEAVY_DEAL_DAMAGE_TIME = 0.3f;

	private const float ATTACK_FAST = 0.5f;

	private const float ATTACK_MID = 1f;

	private const float ATTACK_SLOW = 1.6666666f;

	public static string ATTACK_UNIT_ACTION = "Attack Unit";

	public static string ATTACK_BASE_ACTION = "Attack Base";

	public static string STUN_ACTION = "Stun";

	public string unitName;

	public AttackType attackType;

	public AttackSpeed attackSpeed = AttackSpeed.MID;

	public int UnitID;

	public bool ShowHP = true;

	private bool _canGroupHeal = true;

	public Owner owner;

	private UnitType unitType;

	private int maxLife;

	private int currentLife;

	private int _FinalCure;

	public bool _WeakenUnit;

	private bool _PlayingHurray;

	private int attackDamage;

	private int attackDamageToBase;

	private float velocity;

	private float _DoctorVelocity;

	private float attackIntervalTime;

	private StarStrike_GameStateManager gameStateManager;

	private Transform thisTransform;

	public GameObject damageIndicatorPrefab;

	public GameObject healIndicatorPrefab;

	private Transform _Shadow;

	private StarStrike_ArmyBehaviour behaviour;

	private StarStrike_ActionManager actionManager;

	private StarStrike_Action attackUnitAction;

	private StarStrike_Action attackBaseAction;

	private StarStrike_Action attackIntervalWaitAction;

	private StarStrike_Action moveAction;

	private StarStrike_Action destroyedAction;

	private StarStrike_Action deadAction;

	private StarStrike_Action HeroIdleAcion;

	public float Hero_Velocity;

	private float _HPRegenRate;

	public GameObject BackwardRotation;

	public GameObject ForwardRotation;

	public GameObject EZGUI;

	public GameObject RangeCollider;

	public GameObject[] HeroWeapons;

	public GameObject[] HeroWeapons_Reverse;

	private static InGameUIDemo _InGameUIDemo;

	private bool _CanMoveBackward = true;

	private bool _CanMoveForward = true;

	private bool _HeroStandAndCheck;

	private bool _HeroMoving;

	private bool _HeroSpecialAttack;

	private bool _HeroOff;

	private bool _HeroRangeAttacking;

	private bool _HeroMeleeAttacking;

	private Animation viewAnimation;

	private int _TempTargetTicket;

	private string[] Weapons = new string[2] { "Weapon1", "Weapon2" };

	private static DropItemsManager _DropItemsManager;

	private int sunReward;

	private float sunRewardProb;

	private int waterReward;

	private float waterRewardProb;

	private static KillEnemiesManager _KillEnemiesManager;

	public int DefenseID;

	private static TutorialManager tutorialManager;

	private static UserProfileManager userProfileManager;

	private GameObject hpBarObject;

	private GameObject hpBar;

	public Height height = Height.SHORT;

	public float attackSFXOffset;

	public AudioClip attackSFX;

	public AudioClip[] hurtSFX;

	public AudioClip[] HeroSound;

	public Material material;

	private static UnitIDManager unitIDManager;

	private static FD_ForrestConfiguration forestConfig;

	private static StarStrike_ArmyUnitConfiguration armyUnitConfiguration;

	private static Transform heroTransform;

	public bool is_invincible;

	private static bool showedTutorial;

	private static bool showedHurtTutorial;

	private void Awake()
	{
		if (tutorialManager == null)
		{
			tutorialManager = GameObject.Find("Tutorial Panels").GetComponent<TutorialManager>();
		}
		if (userProfileManager == null)
		{
			userProfileManager = SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance;
		}
		if (unitIDManager == null)
		{
			unitIDManager = GameObject.Find("UnitIDManager").GetComponent<UnitIDManager>();
		}
		if (_DropItemsManager == null)
		{
			_DropItemsManager = GameObject.Find("DropItemsManager").GetComponent<DropItemsManager>();
		}
		if (_KillEnemiesManager == null)
		{
			_KillEnemiesManager = GameObject.Find("KillEnemiesManager").GetComponent<KillEnemiesManager>();
		}
		if (forestConfig == null)
		{
			forestConfig = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
		}
		if (armyUnitConfiguration == null)
		{
			armyUnitConfiguration = GameObject.Find("ArmyUnitConfiguration").GetComponent<StarStrike_ArmyUnitConfiguration>();
		}
		if (_Shadow == null)
		{
			_Shadow = base.transform.Find("Shadow");
		}
		is_invincible = false;
	}

	private void Start()
	{
		if (ShowHP)
		{
			hpBarObject = (GameObject)Object.Instantiate(Resources.Load("HP Bar"));
			hpBar = hpBarObject.transform.Find("Bar").gameObject;
			hpBarObject.transform.parent = base.transform;
			hpBarObject.transform.localScale = new Vector3(0.02f, 0.02f, 1f);
			hpBarObject.transform.eulerAngles = Vector3.zero;
			hpBarObject.transform.position = base.transform.TransformPoint(Vector3.zero) + new Vector3(0f, (float)height / 10f, -1f);
		}
		Transform transform = base.transform.Find("RunParticles");
		if ((bool)transform)
		{
			ParticleSystem.EmissionModule emission = transform.GetComponent<ParticleSystem>().emission;
			emission.enabled = false;
		}
		UnitID = unitIDManager.GetUnitID();
		gameStateManager = StarStrike_GameStateManager.GetInstance();
		thisTransform = base.transform;
		viewAnimation = thisTransform.Find("View").GetComponentInChildren<Animation>();
		if (unitName == "02Squirrel" || unitName == "03Elephant")
		{
			FD_ObjectLevelDefinition currentLevel = forestConfig.GetCurrentLevel(unitName);
			if (currentLevel == null)
			{
				base.gameObject.SetActiveRecursivelyLegacy(false);
				return;
			}
			unitType = UnitType.DEFENSE;
			maxLife = 100;
			attackDamage = int.Parse(currentLevel.GetAttributeValue("attack"));
			Debug.Log("*****" + unitName + " Attack Damage: " + attackDamage);
			velocity = 0f;
			attackIntervalTime = 1.5f;
		}
		else
		{
			StarStrike_ObjectDefinition unitDefinition = armyUnitConfiguration.GetUnitDefinition(unitName);
			unitType = ConvertToUnitType(unitDefinition.GetAttributeValue("unitType"));
			FD_ObjectLevelDefinition currentLevel2 = armyUnitConfiguration.GetCurrentLevel(unitName);
			maxLife = int.Parse(currentLevel2.GetAttributeValue("health"));
			attackDamage = int.Parse(currentLevel2.GetAttributeValue("attack"));
			attackDamageToBase = int.Parse(currentLevel2.GetAttributeValue("attack"));
			//Debug.LogError(unitDefinition.GetAttributeValue("velocity"));
			velocity = float.Parse(unitDefinition.GetAttributeValue("velocity"));
			if (unitType == UnitType.HEAL)
			{
				_DoctorVelocity = velocity;
			}
			//Debug.LogError(unitDefinition.GetAttributeValue("attackIntervalTime"));
			attackIntervalTime = float.Parse(unitDefinition.GetAttributeValue("attackIntervalTime"));

			if (owner == Owner.TOM)
			{
				sunReward = int.Parse(unitDefinition.GetAttributeValue("sunReward"));
				sunRewardProb = float.Parse(unitDefinition.GetAttributeValue("sunRewardProb"));
				waterReward = int.Parse(unitDefinition.GetAttributeValue("waterReward"));
				waterRewardProb = float.Parse(unitDefinition.GetAttributeValue("waterRewardProb"));
			}
		}
		currentLife = maxLife;
		AssignBehaviour();
		actionManager = new StarStrike_ActionManager();
		attackUnitAction = behaviour.GetAction(ATTACK_UNIT_ACTION);
		attackBaseAction = behaviour.GetAction(ATTACK_BASE_ACTION);
		attackIntervalWaitAction = new StarStrike_TimedWaitAction(thisTransform, attackIntervalTime, "ATTACK_INTERVAL_WAIT");
		moveAction = new StarStrike_MoveAction(thisTransform, velocity, "MOVE");
		destroyedAction = new StarStrike_DestroyedAction(thisTransform, "DESTROYED");
		deadAction = new StarStrike_DeadAction(thisTransform, "DEAD");
		HeroIdleAcion = new StarStrike_HeroIdleAction(thisTransform, "IDLE");
		if (unitType == UnitType.HERO)
		{
			material.color = Color.white;
			moveAction = new StarStrike_MoveAction(thisTransform, 13f, 12f, "MOVE");
			forestConfig = GameObject.Find("ForrestConfiguration").GetComponent<FD_ForrestConfiguration>();
			bool flag = false;
			HideHeroWeapons();
			for (int i = 0; i < Weapons.Length; i++)
			{
				FD_ObjectLevelDefinition currentLevel = forestConfig.GetCurrentLevel(Weapons[i]);
				if (currentLevel != null)
				{
					HideHeroWeapons();
					int num = i + 1;
					HeroWeapons[num].SetActiveRecursivelyLegacy(true);
					HeroWeapons_Reverse[num].SetActiveRecursivelyLegacy(true);
					attackDamage = int.Parse(currentLevel.GetAttributeValue("attack"));
					flag = true;
				}
			}
			if (!flag)
			{
				HeroWeapons[0].SetActiveRecursivelyLegacy(true);
				HeroWeapons_Reverse[0].SetActiveRecursivelyLegacy(true);
			}
			actionManager.PushAction(HeroIdleAcion);
			if (_InGameUIDemo == null)
			{
				_InGameUIDemo = EZGUI.transform.GetComponent<InGameUIDemo>();
			}
			_InGameUIDemo.UpdateHeroBar(1f);
			_HPRegenRate = float.Parse(maxLife.ToString()) * 0.01f;
			SetHP(1f);
			LifeRegen();
			heroTransform = thisTransform;
		}
		else
		{
			actionManager.PushAction(moveAction);
		}
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.UNIT_CREATED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.ARMY_UNIT, this);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	private void AssignBehaviour()
	{
		switch (attackType)
		{
		case AttackType.MELEE:
			switch (attackSpeed)
			{
			case AttackSpeed.SLOW:
				behaviour = new StarStrike_MeleeBehaviour(this, 1.6666666f, StarStrike_EventType.HIT_BY_MELEE);
				break;
			case AttackSpeed.MID:
				behaviour = new StarStrike_MeleeBehaviour(this, 1f, StarStrike_EventType.HIT_BY_MELEE);
				break;
			case AttackSpeed.FAST:
				behaviour = new StarStrike_MeleeBehaviour(this, 0.5f, StarStrike_EventType.HIT_BY_MELEE);
				break;
			}
			break;
		case AttackType.RANGED:
			behaviour = new StarStrike_RangedLaserBehaviour(this);
			break;
		case AttackType.HERO:
			behaviour = new StarStrike_HeroBehaviour(this);
			break;
		case AttackType.HEAL:
			behaviour = new StarStrike_RangedLaserBehaviour(this);
			break;
		case AttackType.HEAVY:
			switch (owner)
			{
			case Owner.JERRY:
				behaviour = new StarStrike_MeleeBehaviour(this, 13f / 30f, StarStrike_EventType.HIT_BY_HEAVY);
				break;
			case Owner.TOM:
				behaviour = new StarStrike_MeleeBehaviour(this, 0.3f, StarStrike_EventType.HIT_BY_HEAVY);
				break;
			}
			break;
		}
	}

	private static UnitType ConvertToUnitType(string unitTypeStr)
	{
		if (UnitType.MELEE.ToString().Equals(unitTypeStr))
		{
			return UnitType.MELEE;
		}
		if (UnitType.RANGED.ToString().Equals(unitTypeStr))
		{
			return UnitType.RANGED;
		}
		if (UnitType.HEAVY.ToString().Equals(unitTypeStr))
		{
			return UnitType.HEAVY;
		}
		if (UnitType.HEAL.ToString().Equals(unitTypeStr))
		{
			return UnitType.HEAL;
		}
		if (UnitType.HERO.ToString().Equals(unitTypeStr))
		{
			return UnitType.HERO;
		}
		if (UnitType.DEFENSE.ToString().Equals(unitTypeStr))
		{
			return UnitType.DEFENSE;
		}
		StarStrike_Assertion.Assert(false, "Can't convert the specified string to UnitType");
		return UnitType.MELEE;
	}

	private void Update()
	{
		if (_WeakenUnit)
		{
			SummonReady();
			_WeakenUnit = false;
		}
		if (_InGameUIDemo.CheckHealAll() && _canGroupHeal && owner == Owner.JERRY)
		{
			float num = float.Parse(maxLife.ToString());
			float f = num * _InGameUIDemo.GetHealFactor();
			_FinalCure = int.Parse(Mathf.Floor(f).ToString());
			Invoke("GroupHeal", 1f);
			_canGroupHeal = false;
			Invoke("resetGroupHeal", 2f);
		}
		if (!gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME))
		{
			if (unitType == UnitType.HERO)
			{
				if (gameStateManager.IsCurrentState(StarStrike_GameState.LEVEL_COMPLETE))
				{
					return;
				}
			}
			else
			{
				if (owner != 0 || _PlayingHurray)
				{
					return;
				}
				Debug.Log("CurrentGameState============> " + gameStateManager.GetCurrentState());
				Debug.Log("unitName============> " + unitName);
				Debug.Log("viewAnimation============> " + viewAnimation);
				Invoke("PlayHurray", 0.5f);
				_PlayingHurray = true;
			}
		}
		if (IsAlive())
		{
			DoNextAction();
		}
		actionManager.Update();
		behaviour.CleanDeadTargets();
		if (unitType == UnitType.HERO && _HeroMoving && IsAlive())
		{
			RangeCollider.SetActiveRecursivelyLegacy(false);
		}
		if (userProfileManager.getCurrentPlayMode() == InGamePlayMode.NORMAL && userProfileManager != null && userProfileManager.getGameLevel() == 1 && tutorialManager != null && !showedTutorial && unitType != UnitType.HERO && heroTransform != null && thisTransform.position.x < heroTransform.position.x + 14f)
		{
			showedTutorial = true;
			tutorialManager.Clear();
			tutorialManager.Push(2);
			tutorialManager.Push(3);
			tutorialManager.Push(11);
			tutorialManager.ShowNext();
		}
	}

	private void DoNextAction()
	{
		if (behaviour.HasEnemyInCombat())
		{
			if (!_HeroMoving && !_HeroSpecialAttack)
			{
				StarStrike_ArmyUnit nextTarget = behaviour.GetNextTarget();
				if (nextTarget != null)
				{
					if (nextTarget.unitType == UnitType.HERO)
					{
						float x = base.transform.position.x;
						float x2 = nextTarget.transform.position.x;
						if (HeroInDistance(x, x2))
						{
							AttackUnit();
						}
						else
						{
							behaviour.RemoveEnemyInCombat();
						}
					}
					else
					{
						AttackUnit();
					}
				}
			}
		}
		else if (behaviour.IsEnemyBaseInRange())
		{
			AttackBase();
		}
		else if (IsHeroOff())
		{
			SetHeroOff(false);
		}
		if (!actionManager.IsActionStackEmpty() && actionManager.GetCurrentAction() == moveAction)
		{
		}
	}

	private bool IsAttackingUnit()
	{
		if (actionManager.IsActionStackEmpty())
		{
			return false;
		}
		StarStrike_Action currentAction = actionManager.GetCurrentAction();
		return currentAction == attackUnitAction || currentAction == attackIntervalWaitAction;
	}

	private void AttackUnit()
	{
		if (!IsAttackingUnit())
		{
			if (attackSFX != null)
			{
				StartCoroutine(WaitAndPlay(attackSFXOffset));
			}
			actionManager.PushAction(attackIntervalWaitAction);
			actionManager.PushAction(attackUnitAction);
		}
	}

	public IEnumerator WaitAndPlay(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (attackSFX != null)
		{
			SingletonMonoBehaviour<AudioManager>.Instance.Play(attackSFX);
		}
	}

	private bool IsAttackingBase()
	{
		if (actionManager.IsActionStackEmpty())
		{
			return false;
		}
		return actionManager.GetCurrentAction() == attackBaseAction;
	}

	private void AttackBase()
	{
		if (!IsAttackingBase())
		{
			if (attackSFX != null)
			{
				SingletonMonoBehaviour<AudioManager>.Instance.Play(attackSFX);
			}
			actionManager.PushAction(attackIntervalWaitAction);
			actionManager.PushAction(attackBaseAction);
		}
	}

	private void OnTriggerExit(Collider otherCollider)
	{
		if (unitType != UnitType.HERO && owner == Owner.TOM)
		{
			StarStrike_ArmyUnit starStrike_ArmyUnit = StarStrike_Utils.FindComponentThroughParent<StarStrike_ArmyUnit>(otherCollider.transform);
			if (!(starStrike_ArmyUnit == null) && starStrike_ArmyUnit.unitType == UnitType.HERO)
			{
				behaviour.RemoveUnit(starStrike_ArmyUnit);
			}
		}
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (unitType != UnitType.HERO || !_HeroMoving)
		{
			behaviour.OnCollideWithBody(otherCollider);
		}
	}

	public Owner GetOwner()
	{
		return owner;
	}

	public bool IsEnemy()
	{
		if (owner == Owner.TOM)
		{
			return true;
		}
		return false;
	}

	public bool IsAlive()
	{
		return currentLife > 0;
	}

	private void LifeRegen()
	{
		if (!IsAlive())
		{
			return;
		}
		if (NeedHeal())
		{
			currentLife += int.Parse(Mathf.Floor(_HPRegenRate).ToString());
			if (currentLife > maxLife)
			{
				currentLife = maxLife;
			}
			float num = float.Parse(currentLife.ToString()) / float.Parse(maxLife.ToString());
			_InGameUIDemo.UpdateHeroBar(num);
			SetHP(num);
		}
		Invoke("LifeRegen", 1f);
	}

	public bool NeedHeal()
	{
		return currentLife < maxLife;
	}

	public void HealMaxDamage()
	{
		HealDamage(maxLife);
	}

	public void HealDamage(int cure)
	{
		Debug.Log("Heal Damage: " + cure);
		Debug.Log("Healing =>>>>" + GetUnitType());
		if (IsAlive())
		{
			Vector3 position = thisTransform.Find("Mesh").position;
			position.z = 2f;
			position.y = 0f;
			if ((bool)healIndicatorPrefab)
			{
				Object.Instantiate(healIndicatorPrefab, position, Quaternion.identity);
			}
			Debug.Log("this.currentLife =>>>>" + currentLife);
			currentLife += cure;
			if (currentLife > maxLife)
			{
				currentLife = maxLife;
			}
			float num = float.Parse(currentLife.ToString()) / float.Parse(maxLife.ToString());
			SetHP(num);
			if (unitType == UnitType.HERO)
			{
				_InGameUIDemo.UpdateHeroBar(num);
			}
			Debug.Log("AFTER HEALING this.currentLife =>>>>" + currentLife);
		}
	}

	public void ReceiveDamage(int damage)
	{
		if (!IsAlive())
		{
			SetHP(0f);
		}
		else
		{
			if (is_invincible)
			{
				return;
			}
			StarStrike_Assertion.Assert(damage >= 0, "Damage can't be negative.");
			if (damage <= 0)
			{
				SetHP(0f);
				return;
			}
			currentLife -= damage;
			float num = float.Parse(currentLife.ToString()) / float.Parse(maxLife.ToString());
			if (unitType == UnitType.HERO)
			{
				if (currentLife < 0)
				{
					currentLife = 0;
				}
				_InGameUIDemo.UpdateHeroBar(num);
				if (userProfileManager != null && tutorialManager != null)
				{
					if (userProfileManager.getGameLevel() == 1)
					{
						if (!showedHurtTutorial && num < 0.7f)
						{
							showedHurtTutorial = true;
							tutorialManager.Insert(4);
							if (!tutorialManager.IsShowing)
							{
								tutorialManager.ShowNext();
							}
						}
					}
					else if (userProfileManager.getGameLevel() == 4 && !showedHurtTutorial)
					{
						showedHurtTutorial = true;
						tutorialManager.Insert(15);
						if (!tutorialManager.IsShowing)
						{
							tutorialManager.ShowNext();
						}
						userProfileManager.addItemCount("item04_lvl", 1);
						_InGameUIDemo.checkPotionButton();
					}
				}
				StopAllCoroutines();
				material.color = Color.red;
				StartCoroutine(ResetColor());
			}
			else if (unitName.Equals("Army_Kangaroo") && userProfileManager != null && tutorialManager != null)
			{
				if (userProfileManager.getGameLevel() == 2)
				{
					if (!showedHurtTutorial)
					{
						showedHurtTutorial = true;
						tutorialManager.Insert(13);
						if (!tutorialManager.IsShowing)
						{
							tutorialManager.ShowNext();
						}
						userProfileManager.addItemCount("item02_lvl", 1);
						_InGameUIDemo.checkPotionButton();
					}
				}
				else if (userProfileManager.getGameLevel() == 4 && !showedHurtTutorial)
				{
					showedHurtTutorial = true;
					tutorialManager.Insert(15);
					if (!tutorialManager.IsShowing)
					{
						tutorialManager.ShowNext();
					}
					userProfileManager.addItemCount("item04_lvl", 1);
					_InGameUIDemo.checkPotionButton();
				}
			}
			if (hurtSFX.Length > 0)
			{
				AudioClip audioClip = hurtSFX[Random.Range(0, hurtSFX.Length)];
				if (audioClip != null)
				{
					SingletonMonoBehaviour<AudioManager>.Instance.Play(audioClip);
				}
			}
			Vector3 position = thisTransform.Find("Mesh").position;
			position.z = -10f;
			position.y = 0f;
			Object.Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
			if (!IsAlive())
			{
				StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.UNIT_DESTROYED);
				starStrike_Event.Attach(StarStrike_AttachmentKey.ARMY_UNIT, this);
				StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
				TransitionToDead();
				SetHP(0f);
			}
			else
			{
				SetHP(num);
			}
		}
	}

	private IEnumerator ResetColor()
	{
		yield return new WaitForSeconds(0.1f);
		material.color = Color.white;
	}

	private void SetHP(float value)
	{
		if (hpBarObject != null)
		{
			hpBar.transform.localScale = new Vector3(value, 1f, 1f);
		}
	}

	public void TransitionToDead()
	{
		actionManager.PushAction(deadAction);
		actionManager.PushAction(destroyedAction);
		if (!(unitName == "Army_Buffalo") && !(unitName == "Army_Gorillas") && unitType != UnitType.HERO)
		{
			hpBarObject.SetActiveRecursivelyLegacy(false);
			_Shadow.gameObject.SetActiveRecursivelyLegacy(false);
		}
		if (GetOwner() == Owner.TOM)
		{
			if (_DropItemsManager.CanDropItem())
			{
				renderReward();
			}
			_KillEnemiesManager.KillEnemy();
		}
		if (unitType == UnitType.HERO)
		{
			PostOnDestroyEvent();
			_InGameUIDemo.SetPlayerLost();
		}
		else if (GetOwner() == Owner.JERRY)
		{
			_InGameUIDemo.OnArmyUnitDead(this);
		}
	}

	public StarStrike_ArmyBehaviour GetBehaviour()
	{
		return behaviour;
	}

	public int GetAttackDamage()
	{
		return attackDamage;
	}

	public int GetAttackDamageToBase()
	{
		return attackDamageToBase;
	}

	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	public UnitType GetUnitType()
	{
		return unitType;
	}

	public void PerformAction(StarStrike_Action action)
	{
		if (IsAlive())
		{
			actionManager.PushAction(action);
		}
	}

	public void MoveForward()
	{
		if (!_InGameUIDemo.IsPause && unitType == UnitType.HERO && IsAlive() && !_HeroSpecialAttack)
		{
			_HeroMoving = true;
			if (behaviour.HasEnemyInCombat())
			{
				behaviour.RemoveEnemyInCombat();
			}
			actionManager.ClearActionStack();
			actionManager.PushAction(moveAction);
			thisTransform.rotation = ForwardRotation.transform.rotation;
		}
	}

	public void MoveBackward()
	{
		if (!_InGameUIDemo.IsPause && unitType == UnitType.HERO && IsAlive() && !_HeroSpecialAttack)
		{
			_HeroMoving = true;
			if (behaviour.HasEnemyInCombat())
			{
				behaviour.RemoveEnemyInCombat();
			}
			actionManager.ClearActionStack();
			actionManager.PushAction(moveAction);
			thisTransform.rotation = BackwardRotation.transform.rotation;
		}
	}

	public void Stand()
	{
		if (unitType == UnitType.HERO && IsAlive())
		{
			_HeroStandAndCheck = true;
			_HeroMoving = false;
			if (behaviour.HasEnemyInCombat())
			{
				behaviour.RemoveEnemyInCombat();
			}
			actionManager.ClearActionStack();
			actionManager.PushAction(HeroIdleAcion);
			thisTransform.rotation = ForwardRotation.transform.rotation;
			RangeCollider.SetActiveRecursivelyLegacy(true);
		}
		if (unitType == UnitType.HEAL && IsAlive())
		{
			moveAction = new StarStrike_MoveAction(thisTransform, 0f, "MOVE");
			actionManager.PushAction(moveAction);
			TriggerDoctorCollider();
		}
	}

	public void DoctorMove()
	{
		moveAction = new StarStrike_MoveAction(thisTransform, _DoctorVelocity, "MOVE");
		actionManager.PushAction(moveAction);
	}

	public void SpecialAttack(string animation)
	{
		if (unitType == UnitType.HERO && IsAlive())
		{
			_HeroSpecialAttack = true;
			RangeCollider.SetActiveRecursivelyLegacy(false);
			Invoke("ShowHeroCollider", 0.75f);
			actionManager.ClearActionStack();
			if (viewAnimation != null)
			{
				Debug.Log("Special Attack!!!!!!!!" + viewAnimation);
				viewAnimation.Play(animation);
			}
		}
	}

	public void PlayHorn()
	{
		if (unitType == UnitType.HERO && IsAlive())
		{
			_HeroSpecialAttack = true;
			actionManager.ClearActionStack();
			if (viewAnimation != null)
			{
				Debug.Log("PlayHorn!!!!!!!!" + viewAnimation);
				HideHeroCollider();
				viewAnimation.Play("SP04");
				Invoke("ShowHeroCollider", 1.5f);
			}
		}
	}

	public bool IsHeroOff()
	{
		return _HeroOff;
	}

	public void SetHeroOff(bool off)
	{
		_HeroOff = off;
	}

	private void PostOnDestroyEvent()
	{
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> starStrike_Event = null;
		starStrike_Event = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.JERRY_BASE_DESTROYED);
		starStrike_Event.Attach(StarStrike_AttachmentKey.SOUND_SOURCE, base.gameObject);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(starStrike_Event);
	}

	public void SetHeroTempTarget(int id)
	{
		_TempTargetTicket = id;
	}

	private void ResetHeroTempTarget()
	{
		_TempTargetTicket = 0;
	}

	public int ReturnTempTargetTicket()
	{
		return _TempTargetTicket;
	}

	public void RemoveTempTarget()
	{
		behaviour.RemoveTempTargetArr(_TempTargetTicket);
		ResetHeroTempTarget();
	}

	public float ReturnTransformX()
	{
		return thisTransform.position.x;
	}

	private void renderReward()
	{
		bool flag = false;
		int num = 0;
		Vector3 position = thisTransform.position;
		position.x -= 1f;
		if (_KillEnemiesManager.CanDropWater(waterReward))
		{
			num = Random.Range(0, 100);
			if ((float)num < waterRewardProb)
			{
				flag = false;
				_DropItemsManager.DropItem(position, waterReward, flag);
				return;
			}
		}
		if (_KillEnemiesManager.CanDropSun(sunReward))
		{
			num = Random.Range(0, 100);
			if ((float)num < sunRewardProb)
			{
				flag = true;
				_DropItemsManager.DropItem(position, sunReward, flag);
			}
		}
	}

	public int ReturnUnitID()
	{
		return UnitID;
	}

	public StarStrike_ArmyBehaviour ReturnBehaviour()
	{
		return behaviour;
	}

	public void HeroRangeAttacking()
	{
		_HeroRangeAttacking = true;
		_HeroMeleeAttacking = false;
	}

	public void HeroMeleeAttacking()
	{
		_HeroRangeAttacking = false;
		_HeroMeleeAttacking = true;
	}

	public bool isHeroRangeAttack()
	{
		return _HeroRangeAttacking;
	}

	public bool isHeroMeleeAttack()
	{
		return _HeroMeleeAttacking;
	}

	public bool isHeroMoving()
	{
		return _HeroMoving;
	}

	public bool checkNearestEnemy()
	{
		return _HeroStandAndCheck;
	}

	public void NearestEnemyChecked()
	{
		_HeroStandAndCheck = false;
	}

	public void SummonReady()
	{
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		FD_ObjectLevelDefinition currentLevel = component.GetCurrentLevel("Rein");
		Debug.Log("Horn summon percentage:" + currentLevel.GetAttributeValue("attack"));
		float num = float.Parse(int.Parse(currentLevel.GetAttributeValue("attack")).ToString()) / 100f;
		Debug.Log("weaken =====> " + num);
		Debug.Log("*****" + unitName + "=====SummonUnit=====" + num + "%");
		Debug.Log("*****" + unitName + "=====currentLife=====>>" + returnCurrentLife());
		Debug.Log("*****" + unitName + "=====attackDamage=====>>" + GetAttackDamage());
		maxLife = int.Parse(Mathf.Floor(float.Parse(currentLife.ToString()) * num).ToString());
		attackDamage = int.Parse(Mathf.Floor(float.Parse(attackDamage.ToString()) * num).ToString());
		currentLife = maxLife;
		Debug.Log("*****" + unitName + "=====currentLife=====" + currentLife + "%");
		Debug.Log("*****" + unitName + "=====attackDamage=====" + attackDamage + "%");
	}

	public void SetCanMoveBackward(bool move)
	{
		_CanMoveBackward = move;
	}

	public void SetCanMoveForward(bool move)
	{
		_CanMoveForward = move;
	}

	public bool returnCanMoveForward()
	{
		return _CanMoveForward;
	}

	public bool returnCanMoveBackward()
	{
		return _CanMoveBackward;
	}

	public UnitType ReturnUnitType()
	{
		return unitType;
	}

	private void HideHeroWeapons()
	{
		for (int i = 0; i < HeroWeapons.Length; i++)
		{
			HeroWeapons[i].SetActiveRecursivelyLegacy(false);
			HeroWeapons_Reverse[i].SetActiveRecursivelyLegacy(false);
		}
	}

	private void HideHeroCollider()
	{
		Debug.Log("=====Hide Hero Collider=====");
		RangeCollider.SetActiveRecursivelyLegacy(false);
	}

	private void ShowHeroCollider()
	{
		Debug.Log("=====Show Hero Collider=====");
		RangeCollider.SetActiveRecursivelyLegacy(true);
		_HeroSpecialAttack = false;
	}

	private bool HeroInDistance(float thisX, float heroX)
	{
		float num = 0f;
		num = ((!(thisX > heroX)) ? (heroX - thisX) : (thisX - heroX));
		num = Mathf.Abs(num);
		if (num < 20f)
		{
			return true;
		}
		return false;
	}

	private void TriggerDoctorCollider()
	{
		if (!behaviour.HasEnemyInCombat())
		{
			RangeCollider.SetActiveRecursivelyLegacy(false);
			RangeCollider.SetActiveRecursivelyLegacy(true);
			if (!behaviour.HasEnemyInCombat())
			{
				Invoke("TriggerDoctorCollider", 0.5f);
			}
		}
	}

	public int returnCurrentLife()
	{
		return currentLife;
	}

	public void SetWeaken(bool weaken)
	{
		Invoke("SummonReady", 0.1f);
	}

	private void OnDestroy()
	{
		if (unitType == UnitType.HERO)
		{
			showedTutorial = false;
			showedHurtTutorial = false;
		}
	}

	private void PlayHurray()
	{
		if (!_InGameUIDemo.ReturnPlayerLost() && (bool)viewAnimation["Hurray"])
		{
			viewAnimation.Play("Hurray");
		}
	}

	private void resetGroupHeal()
	{
		_canGroupHeal = true;
	}

	private void GroupHeal()
	{
		HealDamage(_FinalCure);
	}

	public void StartInvincible(float duration)
	{
		is_invincible = true;
		Debug.Log(">>> set true: " + is_invincible);
		material.color = Color.yellow;
		StartCoroutine(WaitInvincible(duration));
		StartCoroutine(BlinkingColor(0.5f));
	}

	public IEnumerator WaitInvincible(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		is_invincible = false;
		material.color = Color.white;
		StopAllCoroutines();
	}

	public IEnumerator BlinkingColor(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (is_invincible)
		{
			if (material.color == Color.yellow)
			{
				material.color = Color.white;
			}
			else
			{
				material.color = Color.yellow;
			}
			StartCoroutine(BlinkingColor(0.5f));
		}
		else
		{
			material.color = Color.white;
		}
	}

	public bool CheckIsInvincible()
	{
		return is_invincible;
	}
}
