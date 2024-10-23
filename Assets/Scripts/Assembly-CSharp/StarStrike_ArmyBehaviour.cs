using UnityEngine;

public interface StarStrike_ArmyBehaviour
{
	StarStrike_ArmyUnit GetOwnerUnit();

	void CleanDeadTargets();

	void RemoveTempTargetArr(int id);

	void AttackNearestEnemy(Transform thisTransform);

	void FindTempEnemy(Collider otherCollider);

	bool IsEnemyBaseInRange();

	void MarkEnemyBaseInRange();

	void UnmarkEnemyBaseInRange();

	void OnCollideWithBody(Collider otherCollider);

	void SetEnemyBase(StarStrike_Base enemyBase);

	StarStrike_Base GetEnemyBase();

	void AddEnemyInCombat(StarStrike_ArmyUnit unit);

	void RemoveEnemyInCombat();

	void RemoveUnit(StarStrike_ArmyUnit unit);

	bool HasEnemyInCombat();

	StarStrike_ArmyUnit GetNextTarget();

	StarStrike_Action GetAction(string actionId);
}
