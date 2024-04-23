using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedia
{
    public SortedDictionary<YinyangItem, ItemCollection> materialCollections;
    public SortedDictionary<Medicines,  ItemCollection> medicineCollections;

	public ItemPedia()
	{
		Init();
	}

	public void Init()
	{
		foreach (Item item in Item.nameDataHashT.Values)
		{
			if(item is Medicines m)
			{
				medicineCollections.Add(m, new ItemCollection(m, true));
			}
			else if(item is YinyangItem yy)
			{
				materialCollections.Add(yy, new ItemCollection(yy, false));
			}
		}
	}

	public void GotNewItem(YinyangItem data)
	{
		if (materialCollections.ContainsKey(data))
		{
			materialCollections[data].Discover();
		}
		if(data is Medicines m)
		{
			if (medicineCollections.ContainsKey(m))
			{
				medicineCollections[m].Discover();
			}
		}
	}

	public void UsedItem(YinyangItem data)
	{
		if (materialCollections.ContainsKey(data))
		{
			materialCollections[data].UsedWithData(data.processes);
		}
		if (data is Medicines m)
		{
			if (medicineCollections.ContainsKey(m))
			{
				medicineCollections[m].UsedWithData(data.processes);
			}
		}
	}
}
