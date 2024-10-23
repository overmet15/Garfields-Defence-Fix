using UnityEngine;

internal class StarStrike_MoveAction : StarStrike_AbstractAction
{
	private static string MOVE_ANIMATION = "Walk";

	private Transform unitTransform;

	private float velocity;

	private Animation animation;

	private Owner owner;

	private StarStrike_ArmyUnit armyUnit;

	private ParticleSystem runParticles;

	private float targetVelocity;

	private float force;

	private float mass = 1f;

	public StarStrike_MoveAction(Transform unitTransform, float velocity, string actionId)
		: base(actionId)
	{
		this.unitTransform = unitTransform;
		armyUnit = this.unitTransform.GetComponent<StarStrike_ArmyUnit>();
		owner = armyUnit.GetOwner();
		force = 1f;
		targetVelocity = velocity;
		this.velocity = velocity;
		animation = unitTransform.Find("View").GetComponentInChildren<Animation>();
	}

	public StarStrike_MoveAction(Transform unitTransform, float force, float velocity, string actionId)
		: base(actionId)
	{
		this.unitTransform = unitTransform;
		armyUnit = this.unitTransform.GetComponent<StarStrike_ArmyUnit>();
		owner = armyUnit.GetOwner();
		this.force = force;
		targetVelocity = velocity;
		animation = unitTransform.Find("View").GetComponentInChildren<Animation>();
		Transform transform = unitTransform.Find("RunParticles");
		if (transform != null)
		{
			runParticles = transform.GetComponent<ParticleSystem>();
		}
		Debug.Log(string.Concat("PARTICLES TRANSFORM: ", transform, ", SYSTEM: ", runParticles));
		if (runParticles != null)
		{
			ParticleSystem.EmissionModule emission = runParticles.emission;
			emission.enabled = false;
		}
	}

	public override void OnPush()
	{
		UnmarkAsDone();
		velocity = 0f;
		PlayMoveAnimation();
	}

	public override void OnReveal()
	{
		PlayMoveAnimation();
	}

	private void SetAnimationSpeed(float amt)
	{
		if (!(animation != null))
		{
			return;
		}
		foreach (AnimationState item in animation)
		{
			item.speed = amt;
		}
	}

	public override void OnPop()
	{
		if (runParticles != null)
		{
			ParticleSystem.EmissionModule emission = runParticles.emission;
			emission.enabled = false;
		}
	}

	private void PlayMoveAnimation()
	{
		if (animation != null && targetVelocity <= 0f)
		{
			animation.CrossFade("Idle");
			if (runParticles != null)
			{
				ParticleSystem.EmissionModule emission = runParticles.emission;
				emission.enabled = false;
			}
		}
		else
		{
			animation.CrossFade(MOVE_ANIMATION);
			if (runParticles != null)
			{
				ParticleSystem.EmissionModule emission2 = runParticles.emission;
				emission2.enabled = true;
			}
		}
	}

	public override bool IsDone()
	{
		return false;
	}

	public override void Update()
	{
		if (!(targetVelocity <= 0f))
		{
			switch (owner)
			{
			}
			TranslateUnit();
		}
	}

	private void TranslateUnit()
	{
		float x = unitTransform.position.x;
		velocity = Mathf.Min(targetVelocity, velocity + Time.deltaTime * force * mass);
		float num = targetVelocity * Time.deltaTime;
		if (unitTransform.localEulerAngles.y < 180f)
		{
			if (x >= 44f)
			{
				unitTransform.position = new Vector3(44f, unitTransform.position.y, unitTransform.position.z);
				return;
			}
		}
		else if (unitTransform.localEulerAngles.y > 180f && x <= -66f)
		{
			unitTransform.position = new Vector3(-66f, unitTransform.position.y, unitTransform.position.z);
			return;
		}
		unitTransform.Translate(unitTransform.forward * num, Space.World);
		SetAnimationSpeed(1f + (1f - velocity / targetVelocity));
	}
}
