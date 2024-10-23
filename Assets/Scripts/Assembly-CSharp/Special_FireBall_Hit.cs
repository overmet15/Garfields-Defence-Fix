using UnityEngine;

public class Special_FireBall_Hit : MonoBehaviour
{
	private int damage;

	public string _Definition;

	private int _Damage;

	private void Start()
	{
		StarStrike_SpecialAttackConfiguration component = GameObject.Find("SpecialAttackConfiguration").GetComponent<StarStrike_SpecialAttackConfiguration>();
		FD_ObjectLevelDefinition currentLevel = component.GetCurrentLevel(_Definition);
		_Damage = int.Parse(currentLevel.GetAttributeValue("attack"));
		Debug.Log("Special Attack: " + _Definition);
		Debug.Log("damage: " + _Damage);
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
}
