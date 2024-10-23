using UnityEngine;

internal interface StarStrike_Targetable
{
	GameObject GetGameObject();

	void ReceiveDamage(int damage);

	bool IsAlive();
}
