using UnityEngine;

[RequireComponent(typeof(StarStrike_Detonator))]
public class StarStrike_DetonatorSparks : StarStrike_DetonatorComponent
{
	private float _baseSize = 1f;

	private float _baseDuration = 4f;

	private Vector3 _baseVelocity = new Vector3(155f, 155f, 155f);

	private Color _baseColor = Color.white;

	private Vector3 _baseForce = Physics.gravity;

	private float _scaledDuration;

	private GameObject _sparks;

	private StarStrike_DetonatorBurstEmitter _sparksEmitter;

	public Material sparksMaterial;

	public Vector3 velocity;

	public override void Init()
	{
		FillMaterials(false);
		BuildSparks();
	}

	public void FillMaterials(bool wipe)
	{
		if (!sparksMaterial || wipe)
		{
			sparksMaterial = MyDetonator().sparksMaterial;
		}
	}

	public void BuildSparks()
	{
		_sparks = new GameObject("Sparks");
		_sparksEmitter = _sparks.AddComponent<StarStrike_DetonatorBurstEmitter>();
		_sparks.transform.parent = base.transform;
		_sparks.transform.localPosition = localPosition;
		_sparksEmitter.material = sparksMaterial;
		_sparksEmitter.force = Physics.gravity;
		_sparksEmitter.useExplicitColorAnimation = false;
	}

	public void UpdateSparks()
	{
		_scaledDuration = duration * timeScale;
		_sparksEmitter.color = color;
		_sparksEmitter.duration = _scaledDuration / 2f;
		_sparksEmitter.durationVariation = _scaledDuration;
		_sparksEmitter.count = (int)(detail * 50f);
		_sparksEmitter.particleSize = 0.5f;
		_sparksEmitter.sizeVariation = 0.25f;
		_sparksEmitter.velocity = velocity;
		_sparksEmitter.startRadius = 0f;
		_sparksEmitter.size = size;
		_sparksEmitter.explodeDelayMin = explodeDelayMin;
		_sparksEmitter.explodeDelayMax = explodeDelayMax;
	}

	public void Reset()
	{
		FillMaterials(true);
		on = true;
		size = _baseSize;
		duration = _baseDuration;
		explodeDelayMin = 0f;
		explodeDelayMax = 0f;
		color = _baseColor;
		velocity = _baseVelocity;
		force = _baseForce;
	}

	public override void Explode()
	{
		if (on)
		{
			UpdateSparks();
			_sparksEmitter.Explode();
		}
	}
}
