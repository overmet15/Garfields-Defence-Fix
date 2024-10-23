using UnityEngine;

public class HeroControl : MonoBehaviour
{
	private Transform thisTransform;

	private StarStrike_Action moveAction;

	private StarStrike_ActionManager actionManager;

	public float Hero_Velocity;

	public float Hero_Force = 0.25f;

	public GameObject BackwardRotation;

	public GameObject ForwardRotation;

	private void Start()
	{
		thisTransform = base.transform;
		actionManager = new StarStrike_ActionManager();
	}

	public void MoveForward()
	{
		thisTransform.rotation = ForwardRotation.transform.rotation;
	}

	public void MoveBackward()
	{
		thisTransform.rotation = BackwardRotation.transform.rotation;
	}

	public void Stand()
	{
		moveAction = new StarStrike_MoveAction(thisTransform, 0f, "MOVE");
		actionManager.PushAction(moveAction);
		thisTransform.rotation = ForwardRotation.transform.rotation;
	}

	private void Update()
	{
		actionManager.Update();
	}
}
