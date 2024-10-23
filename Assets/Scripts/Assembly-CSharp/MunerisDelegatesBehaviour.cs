using System.Runtime.CompilerServices;
using UnityEngine;

public class MunerisDelegatesBehaviour : MonoBehaviour
{
	public delegate void PurchaseHandler(string itemNmae);

	[method: MethodImpl(32)]
	public static event PurchaseHandler PurchaseSucceededHandler;

	[method: MethodImpl(32)]
	public static event PurchaseHandler PurchaseFailedHandler;

	[method: MethodImpl(32)]
	public static event PurchaseHandler PurchaseCancelledHandler;

	private void onPurchaseSucceeded(string itemName)
	{
		if (MunerisDelegatesBehaviour.PurchaseSucceededHandler != null)
		{
			MunerisDelegatesBehaviour.PurchaseSucceededHandler(itemName);
		}
	}

	private void onPurchaseFailed(string itemName)
	{
		if (MunerisDelegatesBehaviour.PurchaseFailedHandler != null)
		{
			MunerisDelegatesBehaviour.PurchaseFailedHandler(itemName);
		}
	}

	private void onPurchaseCancelled(string itemName)
	{
		if (MunerisDelegatesBehaviour.PurchaseFailedHandler != null)
		{
			MunerisDelegatesBehaviour.PurchaseFailedHandler(itemName);
		}
	}
}
