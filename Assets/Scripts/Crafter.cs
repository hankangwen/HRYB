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
	public List<ItemAmountPair> recipe;
	public CraftMethod requirement;

	public Recipe(List<ItemAmountPair> rcp, CraftMethod req)
	{
		recipe = rcp;
		requirement = req;
	}
}


public class Crafter
{

	CraftMethod bestMethod;
	public CraftMethod BestMethod { get => bestMethod; set => bestMethod = value;}

    public static Hashtable itemAmtRecipeHash = new Hashtable()
	{
		{ new ItemAmountPair("����"), new Recipe(new List<ItemAmountPair>{ new ItemAmountPair("��������", 2) }, CraftMethod.Base )},
	};

	public static void AddRecipe(ItemAmountPair resItem, Recipe recipe)
	{
		itemAmtRecipeHash.Add(resItem, recipe);
	}

    public bool Craft(ItemAmountPair data)
	{
		if (itemAmtRecipeHash.ContainsKey(data))
		{
			Recipe recipe = (Recipe)itemAmtRecipeHash[data];
			if(((int)recipe.requirement) <= ((int)bestMethod))
			{
				for (int i = 0; i < recipe.recipe.Count; i++)
				{
					if (!GameManager.instance.pinven.RemoveItem(recipe.recipe[i].info, recipe.recipe[i].num))
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
