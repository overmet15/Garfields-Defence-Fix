using System.Collections;
using UnityEngine;

internal class StarStrike_GuidedMovement
{
	private Transform transform;

	private IList controlPointList;

	private float velocity;

	private int currentIndex;

	private Vector3 currentDirection;

	private StarStrike_CountdownTimer pathTimer;

	private bool finished;

	public StarStrike_GuidedMovement(Transform transform, float velocity)
	{
		this.transform = transform;
		this.velocity = velocity;
		controlPointList = new ArrayList();
		Restart();
	}

	public void AddControlPoint(Vector3 controlPoint)
	{
		controlPointList.Add(controlPoint);
	}

	public bool IsFinished()
	{
		return finished;
	}

	public void Restart()
	{
		currentIndex = -1;
		finished = false;
	}

	public void Update()
	{
		if (finished)
		{
			return;
		}
		StarStrike_Assertion.Assert(controlPointList.Count > 1, "There should more than one control point in the path.");
		if (pathTimer != null)
		{
			pathTimer.Update();
		}
		if (pathTimer == null || pathTimer.HasElapsed())
		{
			currentIndex++;
			if (currentIndex >= controlPointList.Count - 1)
			{
				finished = true;
				return;
			}
			Vector3 vector = (Vector3)controlPointList[currentIndex];
			Vector3 vector2 = (Vector3)controlPointList[currentIndex + 1];
			Vector3 vector3 = vector2 - vector;
			float countdownTime = vector3.magnitude / velocity;
			pathTimer = new StarStrike_CountdownTimer(countdownTime);
			currentDirection = vector3.normalized;
			transform.position = vector;
			Debug.Log("^^^^^^^^this.transform.position: " + transform.position);
		}
		else
		{
			Vector3 translation = currentDirection * (velocity * Time.deltaTime);
			transform.Translate(translation);
		}
	}
}
