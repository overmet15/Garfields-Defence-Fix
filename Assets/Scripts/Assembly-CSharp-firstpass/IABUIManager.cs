using UnityEngine;

public class IABUIManager : MonoBehaviour
{
	private void OnGUI()
	{
		float num = 5f;
		float x = 5f;
		float num2 = ((Screen.width < 800 && Screen.height < 800) ? 160 : 320);
		float num3 = ((Screen.width < 800 && Screen.height < 800) ? 40 : 80);
		float num4 = num3 + 10f;
		if (GUI.Button(new Rect(x, num, num2, num3), "Initialize IAB"))
		{
			string publicKey = "your public key from the Android developer portal here";
			IABAndroid.init(publicKey);
		}
		if (GUI.Button(new Rect(x, num += num4, num2, num3), "Start Billing Availability Check"))
		{
			IABAndroid.startCheckBillingAvailableRequest();
		}
		if (GUI.Button(new Rect(x, num += num4, num2, num3), "Test Purchase"))
		{
			IABAndroid.testPurchaseProduct();
		}
		if (GUI.Button(new Rect(x, num += num4, num2, num3), "Test Refund"))
		{
			IABAndroid.testRefundedProduct();
		}
		if (GUI.Button(new Rect(x, num += num4, num2, num3), "Restore Transactions"))
		{
			IABAndroid.restoreTransactions();
		}
		x = (float)Screen.width - num2 - 5f;
		num = 5f;
		if (GUI.Button(new Rect(x, num, num2, num3), "Purchase Real Product"))
		{
			IABAndroid.purchaseProduct("com.prime31.testproduct");
		}
		if (GUI.Button(new Rect(x, num += num4, num2, num3), "Stop Billing Service"))
		{
			IABAndroid.stopBillingService();
		}
	}
}
