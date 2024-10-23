using UnityEngine;

internal class StarStrike_HeroAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private const float EMIT_LASER_TIME = 1f / 3f;

	private StarStrike_LaserEmitter laserEmitter;

	private Animation animation;

	private int damage;

	private bool laserEmitted;

	private StarStrike_CountdownTimer emitLaserTimer;

	private StarStrike_ArmyUnit armyUnit;

	private string ATTACK_ANIMATION = "Attack";

	private GameObject armyUnitObject;

	private float _Distance = 99f;

	private Transform targetTransform;

	public TutorialManager tutorialManager;

	private bool showedTutorial;

	public StarStrike_HeroAttackBehaviour(StarStrike_ArmyBehaviour behaviour)
	{
		armyUnitObject = behaviour.GetOwnerUnit().gameObject;
		armyUnit = armyUnitObject.GetComponent<StarStrike_ArmyUnit>();
		damage = behaviour.GetOwnerUnit().GetAttackDamage();
		laserEmitter = armyUnitObject.GetComponent<StarStrike_LaserEmitter>();
		StarStrike_Assertion.Assert(laserEmitter != null, "laserEmitter must not be null");
		animation = behaviour.GetOwnerUnit().transform.Find("View").GetComponentInChildren<Animation>();
		emitLaserTimer = new StarStrike_CountdownTimer(1f / 3f);
		tutorialManager = GameObject.Find("Tutorial Panels").GetComponent<TutorialManager>();
	}

	public override void Start()
	{
		base.Start();
		laserEmitted = false;
		emitLaserTimer.Reset();
		if (targetTransform != null)
		{
			float x = armyUnitObject.transform.position.x;
			float x2 = targetTransform.position.x;
			_Distance = Mathf.Abs(x - x2);
		}
		if (_Distance < 5f)
		{
			ATTACK_ANIMATION = "Attack";
		}
		else
		{
			ATTACK_ANIMATION = "RangeAttack";
		}
		animation.CrossFade(ATTACK_ANIMATION);
		if (tutorialManager != null && tutorialManager.IsShowing)
		{
			if (tutorialManager.CurrentPanelIndex == 2)
			{
				tutorialManager.ShowNext();
			}
			else
			{
				tutorialManager.Remove(2);
			}
		}
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		emitLaserTimer.Update();
		if (!laserEmitted && emitLaserTimer.HasElapsed() && targetable != null)
		{
			targetTransform = targetable.GetGameObject().transform.Find("Mesh");
			if (armyUnit.transform.position.x <= targetTransform.position.x)
			{
				StarStrike_Assertion.Assert(targetTransform != null, "targetTransform must not be null.");
				bool hidden = false;
				if (ATTACK_ANIMATION == "Attack")
				{
					hidden = true;
				}
				laserEmitter.EmitLaser(targetTransform, damage, armyUnit.ReturnUnitID(), hidden);
				laserEmitted = true;
			}
			else
			{
				MarkAsDone();
			}
		}
		if (!animation.IsPlaying(ATTACK_ANIMATION))
		{
			MarkAsDone();
		}
	}
}
