using UnityEngine;

public class StarStrike_Detonator : MonoBehaviour
{
	private static float _baseSize = 30f;

	private static Color _baseColor = new Color(1f, 0.423f, 0f, 0.5f);

	private static float _baseDuration = 3f;

	public float size = 10f;

	public Color color = _baseColor;

	public bool explodeOnStart = true;

	public float duration = _baseDuration;

	public float detail = 1f;

	public float destroyTime = 7f;

	public Material fireballAMaterial;

	public Material fireballBMaterial;

	public Material smokeAMaterial;

	public Material smokeBMaterial;

	public Material shockwaveMaterial;

	public Material sparksMaterial;

	public Material glowMaterial;

	public Material heatwaveMaterial;

	private Component[] components;

	private StarStrike_DetonatorFireball _fireball;

	private StarStrike_DetonatorSparks _sparks;

	private StarStrike_DetonatorShockwave _shockwave;

	private StarStrike_DetonatorSmoke _smoke;

	private StarStrike_DetonatorGlow _glow;

	private StarStrike_DetonatorLight _light;

	private StarStrike_DetonatorForce _force;

	private StarStrike_DetonatorHeatwave _heatwave;

	public bool autoCreateFireball = true;

	public bool autoCreateSparks = true;

	public bool autoCreateShockwave = true;

	public bool autoCreateSmoke = true;

	public bool autoCreateGlow = true;

	public bool autoCreateLight = true;

	public bool autoCreateForce = true;

	public bool autoCreateHeatwave;

	private float _lastExplosionTime = 1000f;

	private bool _firstComponentUpdate = true;

	private Component[] _subDetonators;

	public Texture2D defaultFireballATexture;

	private Material defaultFireballAMaterial;

	public Texture2D defaultFireballBTexture;

	private Material defaultFireballBMaterial;

	public Texture2D defaultSmokeATexture;

	private Material defaultSmokeAMaterial;

	public Texture2D defaultSmokeBTexture;

	private Material defaultSmokeBMaterial;

	public Texture2D defaultShockwaveTexture;

	private Material defaultShockwaveMaterial;

	public Texture2D defaultSparksTexture;

	private Material defaultSparksMaterial;

	public Texture2D defaultGlowTexture;

	private Material defaultGlowMaterial;

	public Texture2D defaultHeatwaveTexture;

	private Material defaultHeatwaveMaterial;

	private void Awake()
	{
		FillDefaultMaterials();
		components = GetComponents(typeof(StarStrike_DetonatorComponent));
		Component[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			StarStrike_DetonatorComponent starStrike_DetonatorComponent = (StarStrike_DetonatorComponent)array[i];
			if (starStrike_DetonatorComponent is StarStrike_DetonatorFireball)
			{
				_fireball = starStrike_DetonatorComponent as StarStrike_DetonatorFireball;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorSparks)
			{
				_sparks = starStrike_DetonatorComponent as StarStrike_DetonatorSparks;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorShockwave)
			{
				_shockwave = starStrike_DetonatorComponent as StarStrike_DetonatorShockwave;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorSmoke)
			{
				_smoke = starStrike_DetonatorComponent as StarStrike_DetonatorSmoke;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorGlow)
			{
				_glow = starStrike_DetonatorComponent as StarStrike_DetonatorGlow;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorLight)
			{
				_light = starStrike_DetonatorComponent as StarStrike_DetonatorLight;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorForce)
			{
				_force = starStrike_DetonatorComponent as StarStrike_DetonatorForce;
			}
			if (starStrike_DetonatorComponent is StarStrike_DetonatorHeatwave)
			{
				_heatwave = starStrike_DetonatorComponent as StarStrike_DetonatorHeatwave;
			}
		}
		if (!_fireball && autoCreateFireball)
		{
			_fireball = base.gameObject.AddComponent<StarStrike_DetonatorFireball>();
			_fireball.Reset();
		}
		if (!_smoke && autoCreateSmoke)
		{
			_smoke = base.gameObject.AddComponent<StarStrike_DetonatorSmoke>();
			_smoke.Reset();
		}
		if (!_sparks && autoCreateSparks)
		{
			_sparks = base.gameObject.AddComponent<StarStrike_DetonatorSparks>();
			_sparks.Reset();
		}
		if (!_shockwave && autoCreateShockwave)
		{
			_shockwave = base.gameObject.AddComponent<StarStrike_DetonatorShockwave>();
			_shockwave.Reset();
		}
		if (!_glow && autoCreateGlow)
		{
			_glow = base.gameObject.AddComponent<StarStrike_DetonatorGlow>();
			_glow.Reset();
		}
		if (!_light && autoCreateLight)
		{
			_light = base.gameObject.AddComponent<StarStrike_DetonatorLight>();
			_light.Reset();
		}
		if (!_force && autoCreateForce)
		{
			_force = base.gameObject.AddComponent<StarStrike_DetonatorForce>();
			_force.Reset();
		}
		if (!_heatwave && autoCreateHeatwave && SystemInfo.supportsImageEffects)
		{
			_heatwave = base.gameObject.AddComponent<StarStrike_DetonatorHeatwave>();
			_heatwave.Reset();
		}
		components = GetComponents(typeof(StarStrike_DetonatorComponent));
	}

	private void FillDefaultMaterials()
	{
		if (!fireballAMaterial)
		{
			fireballAMaterial = DefaultFireballAMaterial();
		}
		if (!fireballBMaterial)
		{
			fireballBMaterial = DefaultFireballBMaterial();
		}
		if (!smokeAMaterial)
		{
			smokeAMaterial = DefaultSmokeAMaterial();
		}
		if (!smokeBMaterial)
		{
			smokeBMaterial = DefaultSmokeBMaterial();
		}
		if (!shockwaveMaterial)
		{
			shockwaveMaterial = DefaultShockwaveMaterial();
		}
		if (!sparksMaterial)
		{
			sparksMaterial = DefaultSparksMaterial();
		}
		if (!glowMaterial)
		{
			glowMaterial = DefaultGlowMaterial();
		}
		if (!heatwaveMaterial)
		{
			heatwaveMaterial = DefaultHeatwaveMaterial();
		}
	}

	private void Start()
	{
		if (explodeOnStart)
		{
			UpdateComponents();
			Explode();
		}
	}

	private void Update()
	{
		if (destroyTime > 0f && _lastExplosionTime + destroyTime <= Time.time)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void UpdateComponents()
	{
		if (_firstComponentUpdate)
		{
			Component[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				StarStrike_DetonatorComponent starStrike_DetonatorComponent = (StarStrike_DetonatorComponent)array[i];
				starStrike_DetonatorComponent.Init();
				starStrike_DetonatorComponent.SetStartValues();
			}
			_firstComponentUpdate = false;
		}
		if (_firstComponentUpdate)
		{
			return;
		}
		Component[] array2 = components;
		for (int j = 0; j < array2.Length; j++)
		{
			StarStrike_DetonatorComponent starStrike_DetonatorComponent2 = (StarStrike_DetonatorComponent)array2[j];
			if (starStrike_DetonatorComponent2.detonatorControlled)
			{
				starStrike_DetonatorComponent2.size = starStrike_DetonatorComponent2.startSize * (size / _baseSize);
				starStrike_DetonatorComponent2.timeScale = duration / _baseDuration;
				starStrike_DetonatorComponent2.detail = starStrike_DetonatorComponent2.startDetail * detail;
				starStrike_DetonatorComponent2.color = Color.Lerp(starStrike_DetonatorComponent2.startColor, color, color.a);
			}
		}
	}

	public void Explode()
	{
		_lastExplosionTime = Time.time;
		Component[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			StarStrike_DetonatorComponent starStrike_DetonatorComponent = (StarStrike_DetonatorComponent)array[i];
			UpdateComponents();
			starStrike_DetonatorComponent.Explode();
		}
	}

	public void Reset()
	{
		size = _baseSize;
		color = _baseColor;
		duration = _baseDuration;
		FillDefaultMaterials();
	}

	public Material DefaultFireballAMaterial()
	{
		if (defaultFireballAMaterial != null)
		{
			return defaultFireballAMaterial;
		}
		defaultFireballAMaterial = new Material(Shader.Find("Particles/Additive"));
		defaultFireballAMaterial.name = "FireballA-Default";
		Texture2D mainTexture = defaultFireballATexture;
		defaultFireballAMaterial.SetColor("_TintColor", Color.white);
		defaultFireballAMaterial.mainTexture = mainTexture;
		defaultFireballAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return defaultFireballAMaterial;
	}

	public Material DefaultFireballBMaterial()
	{
		if (defaultFireballBMaterial != null)
		{
			return defaultFireballBMaterial;
		}
		defaultFireballBMaterial = new Material(Shader.Find("Particles/Additive"));
		defaultFireballBMaterial.name = "FireballB-Default";
		Texture2D mainTexture = defaultFireballBTexture;
		defaultFireballBMaterial.SetColor("_TintColor", Color.white);
		defaultFireballBMaterial.mainTexture = mainTexture;
		defaultFireballBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		defaultFireballBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return defaultFireballBMaterial;
	}

	public Material DefaultSmokeAMaterial()
	{
		if (defaultSmokeAMaterial != null)
		{
			return defaultSmokeAMaterial;
		}
		defaultSmokeAMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		defaultSmokeAMaterial.name = "SmokeA-Default";
		Texture2D mainTexture = defaultSmokeATexture;
		defaultSmokeAMaterial.SetColor("_TintColor", Color.white);
		defaultSmokeAMaterial.mainTexture = mainTexture;
		defaultSmokeAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return defaultSmokeAMaterial;
	}

	public Material DefaultSmokeBMaterial()
	{
		if (defaultSmokeBMaterial != null)
		{
			return defaultSmokeBMaterial;
		}
		defaultSmokeBMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		defaultSmokeBMaterial.name = "SmokeB-Default";
		Texture2D mainTexture = defaultSmokeBTexture;
		defaultSmokeBMaterial.SetColor("_TintColor", Color.white);
		defaultSmokeBMaterial.mainTexture = mainTexture;
		defaultSmokeBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		defaultSmokeBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return defaultSmokeBMaterial;
	}

	public Material DefaultSparksMaterial()
	{
		if (defaultSparksMaterial != null)
		{
			return defaultSparksMaterial;
		}
		defaultSparksMaterial = new Material(Shader.Find("Particles/Additive"));
		defaultSparksMaterial.name = "Sparks-Default";
		Texture2D mainTexture = defaultSparksTexture;
		defaultSparksMaterial.SetColor("_TintColor", Color.white);
		defaultSparksMaterial.mainTexture = mainTexture;
		return defaultSparksMaterial;
	}

	public Material DefaultShockwaveMaterial()
	{
		if (defaultShockwaveMaterial != null)
		{
			return defaultShockwaveMaterial;
		}
		defaultShockwaveMaterial = new Material(Shader.Find("Particles/Additive"));
		defaultShockwaveMaterial.name = "Shockwave-Default";
		Texture2D mainTexture = defaultShockwaveTexture;
		defaultShockwaveMaterial.SetColor("_TintColor", Color.white);
		defaultShockwaveMaterial.mainTexture = mainTexture;
		return defaultShockwaveMaterial;
	}

	public Material DefaultGlowMaterial()
	{
		if (defaultGlowMaterial != null)
		{
			return defaultGlowMaterial;
		}
		defaultGlowMaterial = new Material(Shader.Find("Particles/Additive"));
		defaultGlowMaterial.name = "Glow-Default";
		Texture2D mainTexture = defaultGlowTexture;
		defaultGlowMaterial.SetColor("_TintColor", Color.white);
		defaultGlowMaterial.mainTexture = mainTexture;
		return defaultGlowMaterial;
	}

	public Material DefaultHeatwaveMaterial()
	{
		if (SystemInfo.supportsImageEffects)
		{
			if (defaultHeatwaveMaterial != null)
			{
				return defaultHeatwaveMaterial;
			}
			defaultHeatwaveMaterial = new Material(Shader.Find("HeatDistort"));
			defaultHeatwaveMaterial.name = "Heatwave-Default";
			Texture2D texture = defaultHeatwaveTexture;
			defaultHeatwaveMaterial.SetTexture("_BumpMap", texture);
			return defaultHeatwaveMaterial;
		}
		return null;
	}
}
