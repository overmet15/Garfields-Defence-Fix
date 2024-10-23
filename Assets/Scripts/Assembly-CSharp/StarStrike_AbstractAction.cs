internal class StarStrike_AbstractAction : StarStrike_Action
{
	private string id;

	private bool doneFlag;

	public StarStrike_AbstractAction(string id)
	{
		this.id = id;
		doneFlag = false;
	}

	public string GetId()
	{
		return id;
	}

	public virtual void OnPush()
	{
	}

	public virtual void OnReveal()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void OnCover()
	{
	}

	public virtual void OnPop()
	{
	}

	public virtual bool IsDone()
	{
		return doneFlag;
	}

	protected void MarkAsDone()
	{
		doneFlag = true;
	}

	protected void UnmarkAsDone()
	{
		doneFlag = false;
	}
}
