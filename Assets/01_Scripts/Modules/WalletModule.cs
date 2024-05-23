using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SellItem
{
	public Item itemInfo;
	public int price;
	

}

public class WalletModule : Module
{
	public const float DEBTRATIO = 0.1f;


    int money;
	public int maxMoney = -1;

	float prevRegen;
	public int moneyRegenRate = 1000;
	public float purchaseBias = 0.8f;

	public bool debtAble;
	public int DebtMax => (int)(maxMoney * DEBTRATIO);



	ModuleController walletModuleStat = new ModuleController(false);

	public List<SellItem> shopList;
	Dictionary<Item, int> shopDict;

	private void Awake()
	{
		for (int i = 0; i < shopList.Count; i++)
		{
			shopDict.Add(shopList[i].itemInfo, shopList[i].price);
		}
	}

	private void Update()
	{
		RegenerateMoney();
	}

	public int GetShopPrice(string itemName, bool applyBias = true)
	{
		if (shopDict.ContainsKey(Item.GetItem<Item>(itemName)))
		{
			return Mathf.RoundToInt(shopDict[Item.GetItem<Item>(itemName)] * (applyBias ? walletModuleStat.Speed : 1.0f));
		}
		return -1;
	}

	public int GetPurchasePrice(string itemName, bool applyBias = true)
	{
		if (Item.nameDataHashT.ContainsKey(itemName))
		{
			int price;
			if (shopDict.ContainsKey(Item.GetItem<Item>(itemName)))
			{
				price = shopDict[Item.GetItem<Item>(itemName)];
			}
			else
			{
				price = Item.GetItem<Item>(itemName).defaultPrice;
			}

			return Mathf.RoundToInt(price * purchaseBias * (applyBias ? walletModuleStat.Speed : 1.0f));
		}
		return -1;
	}

	public void ShowShop()
	{
		//UI####################
	}

	public void RegenerateMoney()
	{
		if(Time.time - prevRegen >= 60)
		{
			prevRegen = Time.time;
			GetMoney(moneyRegenRate);
		}
	}

	//상대의 아이템을 구매할 경우.
	public int MakePurchase(string name)
	{
		int v = GetPurchasePrice(name);

		if(ExamineLosable(v))
		{
			LoseMoney(v);

			return v;
		}
		return -1;
	}

	public bool ExamineLosable(int amt)
	{
		return money >= amt || (debtAble && money - amt >= DebtMax);
	}

	public void LoseMoney(int amt)
	{
		if (money - amt >= 0)
		{
			money -= amt;
			if (money <= 0)
			{
				walletModuleStat.HandleSpeed(0.5f, ModuleController.SpeedMode.Slow);
			}
			return;
		}

		if (debtAble && money - amt >= DebtMax)
		{
			money -= amt;
			if(money <= 0)
			{
				walletModuleStat.HandleSpeed(0.5f, ModuleController.SpeedMode.Slow);
			}
		}
		
	}

	public void GetMoney(int amt)
	{
		if(maxMoney < 0)
		{
			money += amt;
			if(money > 0)
			{
				walletModuleStat.CompleteReset();
			}
			return;
		}
		if(money < maxMoney)
		{
			money += amt;
			money = Mathf.Clamp(money, DebtMax, maxMoney);
			if (money > 0)
			{
				walletModuleStat.CompleteReset();
			}
		}
	}

	public override void ResetStatus()
	{
		base.ResetStatus();
		//money = 0;  // ??????????
	}
}
