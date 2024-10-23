using UnityEngine;
using UnityEngine.SceneManagement;

public class StarStrike_TomLose : MonoBehaviour
{
	public GameObject rocketCannonPrefab;

	private Animation mainAnimation;

	private Animation tomAnimation;

	private Animation jerryAnimation;

	private void Start()
	{
		mainAnimation = GetComponent<Animation>();
		StarStrike_Assertion.AssertNotNull(mainAnimation, "mainAnimation");
		tomAnimation = base.transform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.Assert(tomAnimation != null, "tomAnimation should not be null");
		Transform transform = Camera.main.transform;
		StarStrike_Assertion.Assert(transform != null, "cameraTransform should not be null");
		transform.position = StarStrike_CameraDragListener.LEFTMOST_POSITION;
		jerryAnimation = GameObject.Find("JerryBase").transform.Find("View").GetComponentInChildren<Animation>();
		StarStrike_Assertion.AssertNotNull(jerryAnimation, "jerryAnimation");
	}

	public void PlayLandingTransition()
	{
		tomAnimation.Play("LandingTransition");
	}

	public void PlayLandingIdle()
	{
		tomAnimation.Play("LandingIdle");
	}

	public void PlayHitTransition()
	{
		tomAnimation.Play("HitTransition");
	}

	public void PlayHitLoop()
	{
		tomAnimation.Play("HitLoop");
	}

	public void InstantiateRocketCannon()
	{
		Object.Instantiate(rocketCannonPrefab);
	}

	public void PlayJerryCelebrate()
	{
		jerryAnimation.Play("Celebrate");
		jerryAnimation.PlayQueued("Celebrate Loop");
	}

	public void GameEnd()
	{
		mainAnimation.Stop();
		StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey> gameEvent = new StarStrike_Event<StarStrike_EventType, StarStrike_AttachmentKey>(StarStrike_EventType.TOM_LOSE_ANIMATION_ENDED);
		StarStrike_EventManagerInstance.GetInstance().PostEvent(gameEvent);
		SceneManager.LoadScene("Mini_Game");
	}
}
