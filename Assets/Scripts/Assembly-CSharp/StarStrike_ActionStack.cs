using System.Collections;

internal class StarStrike_ActionStack
{
	private Stack stack;

	private bool popLocked;

	private bool pushLocked;

	public StarStrike_ActionStack()
	{
		stack = new Stack();
		UnlockPop();
		UnlockPush();
	}

	public bool IsEmpty()
	{
		return stack.Count == 0;
	}

	public void Update()
	{
		if (!IsEmpty())
		{
			StarStrike_Action starStrike_Action = (StarStrike_Action)stack.Peek();
			starStrike_Action.Update();
			CleanDoneActions();
		}
	}

	private void CleanDoneActions()
	{
		while (!IsEmpty())
		{
			StarStrike_Action starStrike_Action = (StarStrike_Action)stack.Peek();
			if (starStrike_Action.IsDone())
			{
				Pop();
				continue;
			}
			break;
		}
	}

	public void FixedUpdate()
	{
		if (!IsEmpty())
		{
			StarStrike_Action starStrike_Action = (StarStrike_Action)stack.Peek();
			starStrike_Action.FixedUpdate();
		}
	}

	public StarStrike_Action Top()
	{
		return (StarStrike_Action)stack.Peek();
	}

	private void LockPush()
	{
		pushLocked = true;
	}

	private void UnlockPush()
	{
		pushLocked = false;
	}

	public void Push(StarStrike_Action action)
	{
		VerifyLocks();
		LockPush();
		if (!IsEmpty())
		{
			StarStrike_Action starStrike_Action = (StarStrike_Action)stack.Peek();
			starStrike_Action.OnCover();
		}
		stack.Push(action);
		action.OnPush();
		UnlockPush();
	}

	private void LockPop()
	{
		popLocked = true;
	}

	private void UnlockPop()
	{
		popLocked = false;
	}

	public void Pop()
	{
		VerifyLocks();
		LockPop();
		StarStrike_Action starStrike_Action = (StarStrike_Action)stack.Pop();
		starStrike_Action.OnPop();
		if (!IsEmpty())
		{
			StarStrike_Action starStrike_Action2 = (StarStrike_Action)stack.Peek();
			starStrike_Action2.OnReveal();
		}
		UnlockPop();
		CleanDoneActions();
	}

	private void VerifyLocks()
	{
		StarStrike_Assertion.Assert(!popLocked && !pushLocked, "Can't proceed with operation. Action stack is still mutating.");
	}

	public void Clear()
	{
		while (!IsEmpty())
		{
			Pop();
		}
	}
}
