using System.Collections;
using UnityEngine;

public class MinigameBridge
{
	private static GameObject mgNetController;

	public static GameObject getController()
	{
		if (!mgNetController)
		{
			mgNetController = GameObject.FindWithTag("MinigameNetController");
		}
		return mgNetController;
	}

	public static void gameEnd(int score, int lv)
	{
		GameObject controller = getController();
		if ((bool)controller)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["score"] = score;
			hashtable["lv"] = lv;
			controller.SendMessage("SendStat", hashtable);
		}
		else
		{
			Debug.Log("No MinigameNetController is found");
		}
	}

	public static void gameQuit(int lv)
	{
		GameObject controller = getController();
		if ((bool)controller)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["lv"] = lv;
			controller.SendMessage("GameQuit", hashtable);
		}
		else
		{
			Debug.Log("No MinigameNetController is found");
		}
	}

	public static void updateLevel(int toLV)
	{
		GameObject controller = getController();
		if ((bool)controller)
		{
			controller.SendMessage("UpdateLevel", toLV);
		}
		else
		{
			Debug.Log("No MinigameNetController is found");
		}
	}
}
