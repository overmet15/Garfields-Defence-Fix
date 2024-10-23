using UnityEngine;

public class InGameDangerCorner : MonoBehaviour
{
	public GameObject corner1;

	public GameObject corner2;

	public GameObject corner3;

	public GameObject corner4;

	private Vector3 ScaleUp = new Vector3(1.5f, 1.5f, 1.5f);

	private Vector3 ScaleDown = new Vector3(1f, 1f, 1f);

	private void Start()
	{
	}

	private void Update()
	{
		scaleUp1();
		scaleUp2();
		scaleUp3();
		scaleUp4();
	}

	private void scaleUp1()
	{
		iTween.ScaleTo(corner1, iTween.Hash("scale", ScaleUp, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleDown1"));
	}

	private void scaleDown1()
	{
		iTween.ScaleTo(corner1, iTween.Hash("scale", ScaleDown, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleUp1"));
	}

	private void scaleUp2()
	{
		iTween.ScaleTo(corner2, iTween.Hash("scale", ScaleUp, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleDown1"));
	}

	private void scaleDown2()
	{
		iTween.ScaleTo(corner2, iTween.Hash("scale", ScaleDown, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleUp1"));
	}

	private void scaleUp3()
	{
		iTween.ScaleTo(corner3, iTween.Hash("scale", ScaleUp, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleDown1"));
	}

	private void scaleDown3()
	{
		iTween.ScaleTo(corner3, iTween.Hash("scale", ScaleDown, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleUp1"));
	}

	private void scaleUp4()
	{
		iTween.ScaleTo(corner4, iTween.Hash("scale", ScaleUp, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleDown1"));
	}

	private void scaleDown4()
	{
		iTween.ScaleTo(corner4, iTween.Hash("scale", ScaleDown, "time", 0.2, "loopType", iTween.LoopType.pingPong, "oncomplete", "scaleUp1"));
	}
}
