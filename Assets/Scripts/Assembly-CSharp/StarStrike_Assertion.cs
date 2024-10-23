using System;

internal class StarStrike_Assertion
{
	private StarStrike_Assertion()
	{
	}

	public static void Assert(bool expression, string assertErrorMessage)
	{
		if (!expression)
		{
			throw new InvalidOperationException(assertErrorMessage);
		}
	}

	public static void AssertNotNull(object pointer, string name)
	{
		Assert(pointer != null, name + " should not be null");
	}
}
