using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftMethod
{
	None,
	Base,

}

public struct ItemAmountPair
{
    public Item info;
    public int num;

    public ItemAmountPair(string name, int cnt = 1)
	{
        info = Item.nameDataHashT[name.GetHashCode()] as Item;
        num = cnt;
	}
}

public struct Recipe
{
	public HashSet<ItemAmountPair> recipe;
	public HashSet<CraftMethod> requirement;

	public Recipe(HashSet<ItemAmountPair> rcp, HashSet<CraftMethod> req)
	{
		recipe = rcp;
		requirement = req;
	}
}


public class Crafter
{

	CraftMethod curMethod;
	public CraftMethod CurMethod { get => curMethod; set => curMethod = value;}

    public static Hashtable itemAmtRecipeHash = new Hashtable()
	{
		{ new ItemAmountPair("����"), new Recipe(new HashSet<ItemAmountPair>{ new ItemAmountPair("��������", 2) }, new HashSet<CraftMethod>{ CraftMethod.None, CraftMethod.Base} )},
	};

	public static void AddRecipe(ItemAmountPair resItem, Recipe recipe)
	{
		itemAmtRecipeHash.Add(resItem, recipe);
	}

    public bool Craft(ItemAmountPair data)
	{
		Debug.Log(curMethod);
		if (itemAmtRecipeHash.ContainsKey(data))
		{
			Recipe recipe = (Recipe)itemAmtRecipeHash[data];
			if(recipe.requirement.Contains(curMethod))
			{
				foreach (ItemAmountPair items in recipe.recipe)
				{
					if (!GameManager.instance.pinven.RemoveItem(items.info, items.num))
					{
						Debug.Log("������ ����");
						return false;
					}
				}
				if (GameManager.instance.pinven.AddItem(data.info, data.num) > 0)
				{
					Debug.Log("�Ϻ� ȹ�� ����");
					return true;
				}
				else
				{
					return true;
				}
			}
			Debug.Log("���� �䱸 ���� ����");
			return false;
		}
		Debug.Log("������ ����.");
		return false;
	}
}
