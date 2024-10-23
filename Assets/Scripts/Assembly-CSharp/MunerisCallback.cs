using System.Collections.Generic;
using Muneris;
using Muneris.Messaging;
using Muneris.Pushnotification;
using Muneris.Takeover;
using Muneris.Virtualgood;
using Muneris.Virtualitem;
using Outblaze;
using UnityEngine;

public class MunerisCallback : ICallback, IDetectEnvarsCargoChangeCallback, IOpenPushNotificationCallback, IRegisterPushNotificationCallback, IUnregisterPushNotificationCallback, ITakeoverCallback, IFindVirtualGoodsCallback, IPurchaseVirtualGoodCallback, IReceiveVirtualItemBundleMessageCallback, ISendVirtualItemBundleAcknowledgmentCallback
{
	private string _currentVirtualGood = string.Empty;

	public void onDetectEnvarsCargoChange(JsonObject cargo)
	{
		Debug.Log("onDetectEnvarsCargoChange");
		//MunerisController.Instance.cargo = cargo;
		CargoManager.OnCargoChange(cargo);
		//MunerisController.Instance.CheckListVirtualGoods();
	}

	private void ShowLoadingPanel()
	{
		GameObject gameObject = GameObject.Find("Loading Panel");
		if (gameObject != null)
		{
			UIPanel component = gameObject.transform.Find("Loading").GetComponent<UIPanel>();
			component.BringIn();
			Debug.Log("ShowLoadingPanel() - successful");
		}
		else
		{
			Debug.Log("ShowLoadingPanel() - NOT successful");
		}
	}

	private void HideLoadingPanel()
	{
		GameObject gameObject = GameObject.Find("Loading Panel");
		if (gameObject != null)
		{
			UIPanel component = gameObject.transform.Find("Loading").GetComponent<UIPanel>();
			component.Dismiss();
			Debug.Log("HideLoadingPanel() - successful");
		}
		else
		{
			Debug.Log("HideLoadingPanel() - NOT successful");
		}
	}

	public void onStartTakeoverRequest(TakeoverEvent takeoverEvent)
	{
		takeoverEvent.useRootViewOrActivity();
		//if (takeoverEvent.getEvent() == MunerisController.Instance.PrivacyPolicyEventName || takeoverEvent.getEvent() == MunerisController.Instance.CustomerSupportEventName)
		//{
		//	ShowLoadingPanel();
		//	MunerisController.Instance.ShowingTakeover = true;
		//}
		Debug.Log("onStartTakeoverRequest");
	}

	public void onLoadTakeover(TakeoverEvent takeoverEvent, TakeoverResponse takeoverResponse)
	{
		Debug.Log("onLoadTakeover");
		AudioListener.volume = 0f;
		takeoverResponse.showBuiltInView();
	}

	public void onDismissTakeover(TakeoverEvent takeoverEvent)
	{
		//MunerisController.Instance.ShowingTakeover = false;
		Debug.Log("onDismissTakeover");
		HideLoadingPanel();
	}

	public void onFailTakeover(TakeoverEvent takeoverEvent, MunerisException exception)
	{
		Debug.Log("onFailTakeover " + exception.ToString());
		if (takeoverEvent.getEvent().Equals("video") || takeoverEvent.getEvent().Equals("offerwall") || takeoverEvent.getEvent().Equals("moreapps") || takeoverEvent.getEvent().Equals("privacypolicy") || takeoverEvent.getEvent().Equals("customersupport"))
		{
			Debug.Log("No Event with this name:" + takeoverEvent.getEvent() + " Available!");
			LanguageManager component = GameObject.Find("LanguageManager").GetComponent<LanguageManager>();
			PopupManager.Instance.ShowPopup(PopupManager.PopupType.Warning, component.getLangData("NoEventAccessError"));
		}
		HideLoadingPanel();
	}

	public void onEndTakeoverRequest(TakeoverEvent takeoverEvent)
	{
		//MunerisController.Instance.ShowingTakeover = false;
		Debug.Log("onEndTakeoverRequest");
		if (SingletonMonoBehaviour<InstanceManager>.Instance.UserProfileManagerInstance.getSoundSetting() == 1)
		{
			AudioListener.volume = 1f;
		}
		else
		{
			AudioListener.volume = 0f;
		}
		HideLoadingPanel();
	}

	public void onReceiveVirtualItemBundleMessage(VirtualItemBundleMessage message)
	{
		Debug.Log("onReceiveVirtualItemBundleMessage");
		message.sendAcknowledgment().execute();
	}

	public void onSendVirtualItemBundleAcknowledgment(VirtualItemBundleAcknowledgment acknowledgment, VirtualItemBundleAcknowledgment outboxAcknowledgment, CallbackContext callbackContext, MunerisException exception)
	{
		if (exception == null)
		{
			VirtualItemBundle virtualItemBundle = acknowledgment.getMessage().getVirtualItemBundle();
			foreach (VirtualItemAndQuantity virtualItemAndQuantity in virtualItemBundle.getVirtualItemAndQuantities())
			{
				VirtualItem virtualItem = virtualItemAndQuantity.getVirtualItem();
				int quantity = virtualItemAndQuantity.getQuantity();
				if (virtualItem.getType() == VirtualItemType.Consumable)
				{
					if (_currentVirtualGood.Equals(string.Empty))
					{
						Debug.Log("You have received " + quantity + " points!");
						Debug.Log("onCreditReceived >>>> " + quantity);
						LanguageManager languageManagerInstance = SingletonMonoBehaviour<InstanceManager>.Instance.LanguageManagerInstance;
						UserProfileManager.Quantity += quantity;
						UserProfileManager.Message = languageManagerInstance.getLangData("TapjoyPointReward1") + quantity + languageManagerInstance.getLangData("TapjoyPointReward2") + "\n" + acknowledgment.getMessage().getText(string.Empty);
					}
					else
					{
						InAppsController component = GameObject.Find("InAppsPurchaseControllers").GetComponent<InAppsController>();
						component.HandlePurchaseSucceededEvent(_currentVirtualGood);
					}
				}
				else if (virtualItem.getType() != VirtualItemType.NonConsumable)
				{
				}
			}
		}
		_currentVirtualGood = string.Empty;
	}

	public void onFindVirtualGoods(List<VirtualGood> virtualGoods, CallbackContext callbackContext, MunerisException exception)
	{
		Debug.Log("onFindVirtualGoods: Enter ");
		if (virtualGoods != null)
		{
			Debug.Log("onFindVirtualGoods: List Not null. Count: " + virtualGoods.Count);
			for (int i = 0; i < virtualGoods.Count; i++)
			{
				//MunerisController.Instance.AddVirtualGood(virtualGoods[i]);
			}
			//MunerisController.Instance.SetVirtualGoodReady();
		}
	}

	public void onPurchaseVirtualGood(VirtualGood virtualGood, CallbackContext callbackContext, MunerisException exception)
	{
		if (exception == null)
		{
			Debug.Log("Purchase successful. Waiting for VirtualItemBundleMessage to give ");
			_currentVirtualGood = virtualGood.getVirtualGoodId();
		}
		else if (exception.GetType() == typeof(PurchaseCancelledException))
		{
			Debug.Log("Purchase cancelled by user. We don't show any message in this situation");
			InAppsController component = GameObject.Find("InAppsPurchaseControllers").GetComponent<InAppsController>();
			component.HandlePurchaseCancelledEvent(virtualGood.getVirtualGoodId());
		}
		else
		{
			Debug.Log("Purchase not successful. " + exception.Message);
			InAppsController component2 = GameObject.Find("InAppsPurchaseControllers").GetComponent<InAppsController>();
			component2.HandlePurchaseFailedEvent(virtualGood.getVirtualGoodId());
		}
	}

	public void onOpenPushNotification(JsonObject data)
	{
		string text = data.ToString();
		Debug.Log("PUSH NOTIFICATION RECEIVED: " + text);
	}

	public void onRegisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception)
	{
		Debug.Log("Muneris: Register Push notification Id: " + registrationId + "-Provider:" + provider);
		if (exception == null)
		{
			Debug.Log("Muneris: Register Push notification SUCCESSFUL");
		}
		else
		{
			Debug.Log("Muneris: Register Push notification NOT SUCCESSFUL. Exception:" + exception.Message);
		}
	}

	public void onUnregisterPushNotification(string registrationId, PushNotificationServiceProvider provider, MunerisException exception)
	{
		Debug.Log("Muneris: UnRegister Push notification Id: " + registrationId + "-Provider:" + provider);
		if (exception == null)
		{
			Debug.Log("Muneris: UN-Register Push notification SUCCESSFUL");
		}
		else
		{
			Debug.Log("Muneris: UN-Register Push notification NOT SUCCESSFUL. Exception:" + exception.Message);
		}
	}
}
