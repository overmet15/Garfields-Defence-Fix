using UnityEngine;

[RequireComponent(typeof(StarStrike_Detonator))]
public class StarStrike_DetonatorSmoke : StarStrike_DetonatorComponent
{
	private const float _baseSize = 1f;

	private const float _baseDuration = 8f;

	private const float _baseDamping = 0.1300004f;

	private Color _baseColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	private float _scaledDuration;

	private GameObject _smokeA;

	private StarStrike_DetonatorBurstEmitter _smokeAEmitter;

	public Material smokeAMaterial;

	private GameObject _smokeB;

	private StarStrike_DetonatorBurstEmitter _smokeBEmitter;

	public Material smokeBMaterial;

	public bool drawSmokeA = true;

	public bool drawSmokeB = true;

	public override void Init()
	{
		FillMaterials(false);
		BuildSmokeA();
		BuildSmokeB();
	}

	public void FillMaterials(bool wipe)
	{
		if (!smokeAMaterial || wipe)
		{
			smokeAMaterial = MyDetonator().smokeAMaterial;
		}
		if (!smokeBMaterial || wipe)
		{
			smokeBMaterial = MyDetonator().smokeBMaterial;
		}
	}

	public void BuildSmokeA()
	{
		_smokeA = new GameObject("SmokeA");
		_smokeAEmitter = _smokeA.AddComponent<StarStrike_DetonatorBurstEmitter>();
		_smokeA.transform.parent = base.transform;
		_smokeA.transform.localPosition = localPosition;
		_smokeAEmitter.material = smokeAMaterial;
		_smokeAEmitter.exponentialGrowth = false;
		_smokeAEmitter.sizeGrow = 0.095f;
	}

	public void UpdateSmokeA()
	{
		_smokeA.transform.localPosition = Vector3.Scale(localPosition, new Vector3(size, size, size));
		_smokeA.transform.LookAt(Camera.main.transform);
		_smokeA.transform.localPosition = -(Vector3.forward * -1.5f);
		_smokeAEmitter.color = base.color;
		_smokeAEmitter.duration = duration * 0.5f;
		_smokeAEmitter.durationVariation = 0f;
		_smokeAEmitter.timeScale = timeScale;
		_smokeAEmitter.count = 2f;
		_smokeAEmitter.particleSize = 25f;
		_smokeAEmitter.sizeVariation = 3f;
		_smokeAEmitter.velocity = new Vector3(3f, 4f, 4f);
		_smokeAEmitter.startRadius = 10f;
		_smokeAEmitter.size = size;
		_smokeAEmitter.useExplicitColorAnimation = true;
		_smokeAEmitter.explodeDelayMin = explodeDelayMin;
		_smokeAEmitter.explodeDelayMax = explodeDelayMax;
		Color color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
		Color color2 = new Color(0.2f, 0.2f, 0.2f, 0.7f);
		Color color3 = new Color(0.2f, 0.2f, 0.2f, 0.4f);
		Color color4 = new Color(0.2f, 0.2f, 0.2f, 0f);
		_smokeAEmitter.colorAnimation[0] = color;
		_smokeAEmitter.colorAnimation[1] = color2;
		_smokeAEmitter.colorAnimation[2] = color2;
		_smokeAEmitter.colorAnimation[3] = color3;
		_smokeAEmitter.colorAnimation[4] = color4;
	}

	public void BuildSmokeB()
	{
		_smokeB = new GameObject("SmokeB");
		_smokeBEmitter = _smokeB.AddComponent<StarStrike_DetonatorBurstEmitter>();
		_smokeB.transform.parent = base.transform;
		_smokeB.transform.localPosition = localPosition;
		_smokeBEmitter.material = smokeBMaterial;
		_smokeBEmitter.exponentialGrowth = false;
		_smokeBEmitter.sizeGrow = 0.095f;
	}

	public void UpdateSmokeB()
	{
		_smokeB.transform.localPosition = Vector3.Scale(localPosition, new Vector3(size, size, size));
		_smokeB.transform.LookAt(Camera.main.transform);
		_smokeB.transform.localPosition = -(Vector3.forward * -1f);
		_smokeBEmitter.color = base.color;
		_smokeBEmitter.duration = duration * 0.5f;
		_smokeBEmitter.durationVariation = 0f;
		_smokeBEmitter.count = 2f;
		_smokeBEmitter.particleSize = 25f;
		_smokeBEmitter.sizeVariation = 3f;
		_smokeBEmitter.velocity = new Vector3(7f, 7f, 7f);
		_smokeBEmitter.startRadius = 10f;
		_smokeBEmitter.size = size;
		_smokeBEmitter.useExplicitColorAnimation = true;
		_smokeBEmitter.explodeDelayMin = explodeDelayMin;
		_smokeBEmitter.explodeDelayMax = explodeDelayMax;
		Color color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
		Color color2 = new Color(0.2f, 0.2f, 0.2f, 0.7f);
		Color color3 = new Color(0.2f, 0.2f, 0.2f, 0.4f);
		Color color4 = new Color(0.2f, 0.2f, 0.2f, 0f);
		_smokeBEmitter.colorAnimation[0] = color;
		_smokeBEmitter.colorAnimation[1] = color2;
		_smokeBEmitter.colorAnimation[2] = color2;
		_smokeBEmitter.colorAnimation[3] = color3;
		_smokeBEmitter.colorAnimation[4] = color4;
	}

	public void Reset()
	{
		FillMaterials(true);
		on = true;
		size = 1f;
		duration = 8f;
		explodeDelayMin = 0f;
		explodeDelayMax = 0f;
		color = _baseColor;
	}

	public override void Explode()
	{
		if (!(detailThreshold > detail) && on)
		{
			UpdateSmokeA();
			UpdateSmokeB();
			if (drawSmokeA)
			{
				_smokeAEmitter.Explode();
			}
			if (drawSmokeB)
			{
				_smokeBEmitter.Explode();
			}
		}
	}
}
