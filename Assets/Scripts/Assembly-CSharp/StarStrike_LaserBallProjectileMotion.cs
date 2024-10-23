using UnityEngine;

internal class StarStrike_LaserBallProjectileMotion : StarStrike_ProjectileMotion
{
	private const float X_OFFSET = 0f;

	public float INITIAL_VELOCITY = 20f;

	private StarStrike_GameStateManager gameStateManager;

	protected override void Initialize()
	{
		base.Initialize();
		gameStateManager = StarStrike_GameStateManager.GetInstance();
	}

	public void SetTarget(Transform target)
	{
		StarStrike_LaserBall component = GetComponent<StarStrike_LaserBall>();
		StarStrike_Assertion.Assert(component != null, "laserBall should not be null");
		component.SetTarget(target);
		float num = base.transform.position.y - target.transform.position.y;
		float num2 = base.transform.position.x - target.transform.position.x;
		float num3 = Mathf.Abs(num) + Mathf.Abs(num2);
		Vector2 vector = new Vector2((0f - INITIAL_VELOCITY) * num2 / num3, (0f - INITIAL_VELOCITY) * num / num3);
		SetInitialVelocity(vector.x, vector.y);
	}

	protected override void DoUpdate()
	{
		if (gameStateManager.IsCurrentState(StarStrike_GameState.IN_GAME) || gameStateManager.IsCurrentState(StarStrike_GameState.LEVEL_COMPLETE) || gameStateManager.IsCurrentState(StarStrike_GameState.GAME_OVER) || gameStateManager.IsCurrentState(StarStrike_GameState.GAME_COMPLETED) || gameStateManager.IsCurrentState(StarStrike_GameState.WAIT_LOAD_JERRY_LOSE) || gameStateManager.IsCurrentState(StarStrike_GameState.WAIT_AFTER_TOM_DESTROYED))
		{
			base.DoUpdate();
		}
	}
}
