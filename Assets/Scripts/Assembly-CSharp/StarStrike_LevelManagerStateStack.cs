using System.Collections;

internal class StarStrike_LevelManagerStateStack
{
	private Stack stateStack;

	private StarStrike_LevelManagerState topState;

	public StarStrike_LevelManagerStateStack()
	{
		stateStack = new Stack();
	}

	public void Push(StarStrike_LevelManagerState managerState)
	{
		StarStrike_Assertion.Assert(managerState != null, "Can't push a null state.");
		if (!IsEmpty())
		{
			StarStrike_LevelManagerState starStrike_LevelManagerState = Top();
			starStrike_LevelManagerState.ProcessEvent(StarStrike_LevelManagerStateEvent.ON_COVER);
		}
		stateStack.Push(managerState);
		managerState.ProcessEvent(StarStrike_LevelManagerStateEvent.ON_PUSH);
		topState = managerState;
		StarStrike_Assertion.Assert(topState == stateStack.Peek(), "The cached top state must actually be the current top state of the stack.");
	}

	public void Pop()
	{
		StarStrike_Assertion.Assert(!IsEmpty(), "Can no longer pop. Stack is empty.");
		StarStrike_LevelManagerState starStrike_LevelManagerState = (StarStrike_LevelManagerState)stateStack.Peek();
		starStrike_LevelManagerState.ProcessEvent(StarStrike_LevelManagerStateEvent.ON_POP);
		stateStack.Pop();
		if (IsEmpty())
		{
			topState = null;
			return;
		}
		topState = (StarStrike_LevelManagerState)stateStack.Peek();
		topState.ProcessEvent(StarStrike_LevelManagerStateEvent.ON_REVEAL);
		StarStrike_Assertion.Assert(topState == stateStack.Peek(), "The cached top state must actually be the current top state of the stack.");
	}

	public bool IsEmpty()
	{
		return stateStack.Count == 0;
	}

	public void Clear()
	{
		while (!IsEmpty())
		{
			Pop();
		}
	}

	public StarStrike_LevelManagerState Top()
	{
		StarStrike_Assertion.Assert(!IsEmpty(), "Can't get a top state. Stack is empty.");
		StarStrike_Assertion.Assert(topState == stateStack.Peek(), "The cached top state must actually be the current top state of the stack.");
		return topState;
	}
}
