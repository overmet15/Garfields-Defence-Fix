using UnityEngine;

public class HealBall : MonoBehaviour
{
	public Owner owner;

	private bool collided;

	private bool targetUnit;

	private void Start()
	{
		collided = false;
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
		if (!collided && CheckWithArmyUnit(other))
		{
			collided = true;
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
		if (component.GetOwner() != owner)
		{
			return false;
		}
		if (!component.IsAlive())
		{
			return false;
		}
		return true;
	}

	public void SetDamage(int damage)
	{
	}
}
