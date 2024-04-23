using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemCollection
{
    public Item myItem;
	public bool discovered;
	public List<HashSet<ProcessType>> useCount;
	public const int MAXUSECOUNT = 5;

	public bool masterable => MAXUSECOUNT > 0;
	public bool isMastered => masterable && useCount.Count >= MAXUSECOUNT;

	bool allowRepeat;

	List<Item> resultItems;
	public List<Item> ResultItems
	{
		get
		{
			if(resultItems.Count <= 0)
			{
				foreach (ItemAmountPair item in Crafter.recipeItemTableTrim.Keys)
				{
					if(item.info.originalName == myItem.originalName)
					{
						foreach (ItemAmountPair resItem in (HashSet<ItemAmountPair>)Crafter.recipeItemTableTrim[item])
						{
							resultItems.Add(resItem.info);
						}
					}
				}
			}

			return resultItems;
		}
	}

	public void Discover()
	{
		if (!discovered)
		{
			discovered = true;
		}
	}

	public void UsedWithData(HashSet<ProcessType> data)
	{
		
		if (allowRepeat || !useCount.Contains(data))
		{
			useCount.Add(data);
		}
	}

	public ItemCollection(bool medicineMode)
	{
		myItem = null;
		discovered = false;
		allowRepeat = medicineMode;
		useCount = new List<HashSet<ProcessType>>(MAXUSECOUNT);
		resultItems = new List<Item>();
	}

	public ItemCollection(Item info, bool medicineMode)
	{
		myItem = info;
		discovered = false;
		allowRepeat = medicineMode;
		useCount = new List<HashSet<ProcessType>>(MAXUSECOUNT);
		resultItems = new List<Item>();
	}

	public override bool Equals(object obj)
	{
		if(obj is ItemCollection col && col.myItem == myItem)
			return true;
		else if(obj is Item item && item == myItem)
			return true;
		return false;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(myItem);
	}

	public static bool operator ==(ItemCollection lft, ItemCollection rht)
	{
		return lft.myItem == rht.myItem;
	}

	public static bool operator !=(ItemCollection lft, ItemCollection rht)
	{
		return lft.myItem != rht.myItem;
	}
}
