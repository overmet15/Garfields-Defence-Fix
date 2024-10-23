using System.Collections;

public class StarStrike_GameStateManager
{
	private Stack stack;

	private static StarStrike_GameStateManager ONLY_INSTANCE;

	private StarStrike_GameStateManager()
	{
		stack = new Stack();
	}

	public static StarStrike_GameStateManager GetInstance()
	{
		if (ONLY_INSTANCE == null)
		{
			ONLY_INSTANCE = new StarStrike_GameStateManager();
		}
		return ONLY_INSTANCE;
	}

	public static void DeleteInstance()
	{
		ONLY_INSTANCE = null;
	}

	public void Push(StarStrike_GameState state)
	{
		stack.Push(state);
	}

	public void Pop()
	{
		stack.Pop();
	}

	public bool IsCurrentState(StarStrike_GameState state)
	{
		if (stack.Count == 0)
		{
			return false;
		}
		return (int)stack.Peek() == (int)state;
	}

	public StarStrike_GameState GetCurrentState()
	{
		return (StarStrike_GameState)(int)stack.Peek();
	}

	public void Clear()
	{
		stack.Clear();
	}
}
