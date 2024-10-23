using UnityEngine;

internal class StarStrike_Comparison
{
	private const float FLOATING_POINT_TOLERANCE = 0.001f;

	private StarStrike_Comparison()
	{
	}

	public static bool TolerantEquals(float a, float b)
	{
		return Mathf.Abs(a - b) < 0.001f;
	}

	public static bool TolerantGreaterThanOrEquals(float a, float b)
	{
		return a > b || TolerantEquals(a, b);
	}

	public static bool TolerantLesserThanOrEquals(float a, float b)
	{
		return a < b || TolerantEquals(a, b);
	}
}
