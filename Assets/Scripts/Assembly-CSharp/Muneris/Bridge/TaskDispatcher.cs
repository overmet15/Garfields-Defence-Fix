using System;
using System.Collections.Generic;
using UnityEngine;

namespace Muneris.Bridge
{
	public class TaskDispatcher : MonoBehaviour
	{
		public delegate void Task();

		private static Queue<Task> sTasks = new Queue<Task>();

		private static object sTasksLock = new object();

		public static void Init()
		{
			DebugUtil.Log("Initializing Muneris.Bridge.TaskDispatcher");
			GameObject gameObject = new GameObject();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.AddComponent<TaskDispatcher>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
		}

		private void Update()
		{
			if (sTasks.Count <= 0)
			{
				return;
			}
			List<Task> list = new List<Task>();
			lock (sTasksLock)
			{
				while (sTasks.Count > 0)
				{
					list.Add(sTasks.Dequeue());
				}
			}
			DebugUtil.Log("Running tasks on main thread");
			foreach (Task item in list)
			{
				try
				{
					item();
				}
				catch (Exception exception)
				{
					DebugUtil.LogException(exception);
				}
			}
		}

		public static void RunOnMainThread(Task task)
		{
			lock (sTasksLock)
			{
				DebugUtil.Log("Adding task to main thread queue");
				sTasks.Enqueue(task);
			}
		}
	}
}
