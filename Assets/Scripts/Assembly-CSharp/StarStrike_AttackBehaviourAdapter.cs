internal class StarStrike_AttackBehaviourAdapter : StarStrike_AttackBehaviour
{
	private bool done;

	private bool timeToDealDamage;

	public StarStrike_AttackBehaviourAdapter()
	{
		UnmarkAsDone();
		UnmarkTimeToDealDamage();
	}

	public virtual void Start()
	{
		UnmarkAsDone();
		UnmarkTimeToDealDamage();
	}

	public virtual void Update(StarStrike_Targetable targetable)
	{
	}

	public virtual bool IsTimeToDealDamage()
	{
		return timeToDealDamage;
	}

	public virtual bool IsDone()
	{
		return done;
	}

	protected void UnmarkAsDone()
	{
		done = false;
	}

	protected void MarkAsDone()
	{
		done = true;
	}

	protected void UnmarkTimeToDealDamage()
	{
		timeToDealDamage = false;
	}

	protected void MarkTimeToDealDamage()
	{
		timeToDealDamage = true;
	}
}
