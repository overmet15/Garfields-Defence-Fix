using System.Collections;

internal class StarStrike_ActionManager
{
	private StarStrike_ActionStack actionStack;

	private Queue polledActionQueue;

	public StarStrike_ActionManager()
	{
		actionStack = new StarStrike_ActionStack();
		polledActionQueue = new Queue();
	}

	public void Update()
	{
		actionStack.Update();
	}

	public void FixedUpdate()
	{
		actionStack.FixedUpdate();
	}

	public void PushAction(StarStrike_Action action)
	{
		actionStack.Push(action);
	}

	public void PopAction()
	{
		actionStack.Pop();
		if (polledActionQueue.Count > 0)
		{
			StarStrike_Action action = (StarStrike_Action)polledActionQueue.Dequeue();
			actionStack.Push(action);
		}
	}

	public void PollAction(StarStrike_Action action)
	{
		polledActionQueue.Enqueue(action);
	}

	public void ClearActionStack()
	{
		actionStack.Clear();
	}

	public StarStrike_Action GetCurrentAction()
	{
		return actionStack.Top();
	}

	public bool IsActionStackEmpty()
	{
		return actionStack.IsEmpty();
	}
}
