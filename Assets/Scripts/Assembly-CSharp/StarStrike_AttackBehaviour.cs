internal interface StarStrike_AttackBehaviour
{
	void Start();

	void Update(StarStrike_Targetable targetable);

	bool IsTimeToDealDamage();

	bool IsDone();
}
