using System.Collections;
using UnityEngine;

public class Utils_ColliderCallback : MonoBehaviour
{
	public MonoBehaviour colliderDelegate;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("thisCollider", GetComponent<Collider>());
		hashtable.Add("otherCollider", other);
		colliderDelegate.SendMessage("onCollisionWithContacts", hashtable, SendMessageOptions.DontRequireReceiver);
	}

	private void OnTriggerExit(Collider other)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("thisCollider", GetComponent<Collider>());
		hashtable.Add("otherCollider", other);
		colliderDelegate.SendMessage("onCollisionExitWithContacts", hashtable, SendMessageOptions.DontRequireReceiver);
	}
}
