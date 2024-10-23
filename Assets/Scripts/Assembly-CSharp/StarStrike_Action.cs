public interface StarStrike_Action
{
	string GetId();

	void OnPush();

	void OnReveal();

	void Update();

	void FixedUpdate();

	void OnCover();

	void OnPop();

	bool IsDone();
}
