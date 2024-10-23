using Outblaze;
using UnityEngine;

public class Special_Rush : MonoBehaviour
{
	public string Definition;

	public float velocity;

	private int _Damage;

	public Vector3 _SpawnPosition;

	public Vector3 _OffPosition;

	private Transform unitTransform;

	private Vector3 initialVelocity;

	public AudioClip attackSFX;

	private AudioSource thisAudioSource;

	private void Start()
	{
		initialVelocity = new Vector3(velocity, 0f, 0f);
		unitTransform = base.transform;
		unitTransform.position = _SpawnPosition;
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		FD_ObjectLevelDefinition currentLevel = component.GetCurrentLevel(Definition);
		_Damage = int.Parse(currentLevel.GetAttributeValue("attack"));
		Debug.Log("Special Attack: " + Definition);
		Debug.Log("damage: " + _Damage);
		if (attackSFX != null)
		{
			thisAudioSource = SingletonMonoBehaviour<AudioManager>.Instance.Play(attackSFX, true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_Damage > 0)
		{
			StarStrike_ArmyUnit component = other.GetComponent<StarStrike_ArmyUnit>();
			if (component != null && component.GetOwner() == Owner.TOM)
			{
				int num = component.returnCurrentLife();
				int num2 = 0;
				num2 = ((_Damage < num) ? _Damage : num);
				_Damage -= num2;
				component.ReceiveDamage(num2);
				Debug.Log("Deal Damage: " + num2);
				Debug.Log("_Damage: " + _Damage);
			}
		}
	}

	private void Update()
	{
		Vector3 translation = initialVelocity * Time.deltaTime;
		InGameUIDemo component = GameObject.Find("In Game EZGUI").GetComponent<InGameUIDemo>();
		unitTransform.Translate(translation, Space.World);
		if (unitTransform.position.x > _OffPosition.x || component.ReturnLevelComplete())
		{
			Object.Destroy(base.gameObject);
			if (thisAudioSource != null)
			{
				Object.Destroy(thisAudioSource);
			}
		}
	}
}
