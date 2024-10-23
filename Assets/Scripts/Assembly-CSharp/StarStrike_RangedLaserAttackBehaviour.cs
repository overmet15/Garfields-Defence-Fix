using UnityEngine;

internal class StarStrike_RangedLaserAttackBehaviour : StarStrike_AttackBehaviourAdapter
{
	private const float EMIT_LASER_TIME = 0.5f;

	private StarStrike_LaserEmitter laserEmitter;

	private Animation animation;

	private int damage;

	private bool laserEmitted;

	private StarStrike_CountdownTimer emitLaserTimer;

	private StarStrike_ArmyUnit armyUnit;

	private static string ATTACK_ANIMATION = "Attack";

	public StarStrike_RangedLaserAttackBehaviour(StarStrike_ArmyBehaviour behaviour)
	{
		GameObject gameObject = behaviour.GetOwnerUnit().gameObject;
		armyUnit = gameObject.GetComponent<StarStrike_ArmyUnit>();
		damage = behaviour.GetOwnerUnit().GetAttackDamage();
		laserEmitter = gameObject.GetComponent<StarStrike_LaserEmitter>();
		StarStrike_Assertion.Assert(laserEmitter != null, "laserEmitter must not be null");
		animation = behaviour.GetOwnerUnit().transform.Find("View").GetComponentInChildren<Animation>();
		if (armyUnit.ReturnUnitType() == UnitType.DEFENSE)
		{
			float countdownTime = 1f / 3f;
			emitLaserTimer = new StarStrike_CountdownTimer(countdownTime);
		}
		else
		{
			emitLaserTimer = new StarStrike_CountdownTimer(0.5f);
		}
	}

	public override void Start()
	{
		base.Start();
		laserEmitted = false;
		emitLaserTimer.Reset();
		animation.CrossFade(ATTACK_ANIMATION);
		if (armyUnit.ReturnUnitType() != UnitType.DEFENSE)
		{
		}
	}

	public override void Update(StarStrike_Targetable targetable)
	{
		emitLaserTimer.Update();
		if (!laserEmitted && emitLaserTimer.HasElapsed() && targetable != null)
		{
			Transform transform = targetable.GetGameObject().transform.Find("Mesh");
			StarStrike_Assertion.Assert(transform != null, "targetTransform must not be null.");
			laserEmitter.EmitLaser(transform, damage, armyUnit.ReturnUnitID());
			laserEmitted = true;
		}
		if (!animation.IsPlaying(ATTACK_ANIMATION))
		{
			MarkAsDone();
		}
	}
}
