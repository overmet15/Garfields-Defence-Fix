using Outblaze;
using UnityEngine;

public class StarStrike_LaserBall : MonoBehaviour
{
	public enum MissleType
	{
		HEAL = 0,
		ATTACK = 1
	}

	public Owner owner;

	public MissleType missleType;

	private int damage;

	private int _OwnerID;

	private bool collided;

	public AudioClip audioClip;

	public AudioClip audioClip_hitTarget;

	private bool targetUnit;

	private void Awake()
	{
		if (missleType == MissleType.HEAL)
		{
			Vector3 position = base.transform.position;
			base.transform.position = position;
		}
	}

	private void Start()
	{
		collided = false;
		if (audioClip != null)
		{
			SingletonMonoBehaviour<AudioManager>.Instance.Play(audioClip);
		}
		Invoke("SelfDestroy", 1.5f);
	}

	public void SetTarget(Transform transformTarget)
	{
		StarStrike_Base starStrike_Base = StarStrike_Utils.FindComponentThroughParent<StarStrike_Base>(transformTarget);
		if (starStrike_Base == null)
		{
			targetUnit = true;
		}
		else
		{
			targetUnit = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!collided && (CheckWithArmyUnit(other) || CheckWithBase(other)))
		{
			collided = true;
			if (audioClip_hitTarget != null)
			{
				SingletonMonoBehaviour<AudioManager>.Instance.Play(audioClip_hitTarget);
			}
			Object.Destroy(base.gameObject);
		}
	}

	private bool CheckWithArmyUnit(Collider other)
	{
		if (!targetUnit)
		{
			return false;
		}
		StarStrike_ArmyUnit component = other.GetComponent<StarStrike_ArmyUnit>();
		if (component == null)
		{
			return false;
		}
		if (missleType == MissleType.ATTACK && component.GetOwner() == owner)
		{
			return false;
		}
		if (missleType == MissleType.HEAL)
		{
			if (component.GetOwner() != owner)
			{
				Debug.Log("!@#$@!#$@#!$@#! not heal");
				return false;
			}
			if (component.ReturnUnitID() == _OwnerID)
			{
				Debug.Log("!@#$@!#$@#!$@#! not heal 2");
				return false;
			}
		}
		if (!component.IsAlive())
		{
			return false;
		}
		if (missleType == MissleType.HEAL)
		{
			if (component.NeedHeal())
			{
				component.HealDamage(damage);
				return true;
			}
			return false;
		}
		component.ReceiveDamage(damage);
		return true;
	}

	private bool CheckWithBase(Collider other)
	{
		if (missleType == MissleType.ATTACK)
		{
			if (targetUnit)
			{
				return false;
			}
			StarStrike_Base starStrike_Base = StarStrike_Utils.FindComponentThroughParent<StarStrike_Base>(other.transform);
			if (starStrike_Base == null)
			{
				return false;
			}
			if (starStrike_Base.GetOwner() == owner)
			{
				return false;
			}
			starStrike_Base.ReceiveDamage(damage);
			return true;
		}
		return false;
	}

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	public void SetOwnerID(int OwnerID)
	{
		_OwnerID = OwnerID;
	}

	private void SelfDestroy()
	{
		Object.Destroy(base.gameObject);
	}
}
