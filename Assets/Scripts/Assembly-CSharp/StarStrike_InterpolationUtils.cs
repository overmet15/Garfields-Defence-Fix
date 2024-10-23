internal class StarStrike_InterpolationUtils
{
	private StarStrike_InterpolationUtils()
	{
	}

	public static float SmoothStep(float interpolationValue)
	{
		return interpolationValue * interpolationValue * (3f - 2f * interpolationValue);
	}

	public static float GetParabolicPos(float t, float y0, float y1, float dh)
	{
		return GetParabolicPos(t, y0, y1, dh, 0.2f);
	}

	public static float GetParabolicPos(float t, float y0, float y1, float dh, float tFracIncrease)
	{
		float num = tFracIncrease * tFracIncrease;
		float num2 = y0 - dh;
		float num3 = (y1 * tFracIncrease - num2 - y0 * (tFracIncrease - 1f)) / (tFracIncrease - num);
		float num4 = y1 - y0 - num3;
		return num3 * t * t + num4 * t + y0;
	}
}
