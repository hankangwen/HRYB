using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
	None = -1,
	Solid,
	Liquid,
	Air,
}

public enum ItemRarity
{
	S,
	A,
	B,
	C,
	D,
	F,

	Medicine,
	Material,
}

public class Specials
{
	public System.Func<bool> onActivated;
	public float removeTime;

	public void DeleteSpecial()
	{
		onActivated = null;
	}

	public Specials(System.Func<bool> onAct, float removeT)
	{
		onActivated = onAct;
		removeTime = removeT;
	}
}

[System.Serializable]
public class Item : IComparable // #################
{
	public static Specials removeOnComp  = new Specials(() => true, Mathf.Infinity);
    public static Hashtable nameDataHashT = new Hashtable()
	{
		{"나뭇가지".GetHashCode(), new Item("나뭇가지","나무에서 열리는 가지를 나뭇가지라고 부르더라", ItemType.Solid, 999, null, false) },
		{"인삼".GetHashCode(), new YinyangItem("인삼", "사실 인삼이나 산삼이나 거기서 거기다", ItemType.Solid, 999, null, false, new YinYang(0), DetailAmount.Empty, false, "삼") },
		{"물".GetHashCode(), new YinyangItem("물", "물로 보지 말랜다", ItemType.Liquid,  999, null, false, new YinYang(0), DetailAmount.Empty, false, "수")},
		{"활".GetHashCode(), new Item("활", "적당한 나무활이다. 사용해서 장착해볼까?", ItemType.Solid, 999, new Specials(()=>{
			GameManager.instance.pinven.ObtainWeapon();
			return true;
		}, Mathf.Infinity), false) },
		{"밧줄".GetHashCode(), new Item("밧줄", "무언가에 매기 위해 맨 줄이다.", ItemType.Solid,  999, new Specials(()=>
		{
			return (GameManager.instance.pActor.atk as PlayerAttack).ThrowRope();
		}, Mathf.Infinity), false) },
		{"섬유".GetHashCode(), new Item("섬유", "튼튼한 섬유이다.", ItemType.Solid, 999, null, false) },
		{"산삼".GetHashCode(), new YinyangItem("산삼", "심 봤 다", ItemType.Solid, 999, null, false, null, DetailAmount.Empty, false, "삼") },
		{"잘린 산삼".GetHashCode(), new YinyangItem("잘린 산삼", "손질 완료된 산삼이다.", ItemType.Solid, 999, null, false, null, DetailAmount.Empty, false, "삼") },
		{"녹제".GetHashCode(), new YinyangItem("녹제", "사슴의 발굽이다.", ItemType.Solid, 999, null, false, new YinYang(0), DetailAmount.Empty, false) },
		{"녹각".GetHashCode(), new YinyangItem("녹각", "사슴의 뿔이다.", ItemType.Solid, 999, null, false, new YinYang(0), DetailAmount.Empty, false) },
		{"녹용".GetHashCode(), new YinyangItem("녹용", "사슴의 뿔을 손질했다.", ItemType.Solid, 999, null, false, new YinYang(0), DetailAmount.Empty, false) },
		{"도약탕".GetHashCode(), new Medicines("도약탕", "다리의 힘을 비약적으로 상승시켜 땅을 박차고 공중으로 도약할 수 있게 해준다.", ItemType.Liquid, 999, null, false, new YinYang(0), DetailAmount.Empty) },
	}; //같은 이름의 아이템을 같은 물건으로 취급하기 위해 사용.

	static Hashtable allYinyangItems;
	public static Hashtable AllYinyangItems
	{
		get
		{
			if(allYinyangItems == null)
			{
				allYinyangItems = new Hashtable();
				foreach (int item in nameDataHashT.Keys)
				{
					if(nameDataHashT[item] is YinyangItem yy && !(nameDataHashT[item] is Medicines))
					{
						allYinyangItems.Add(item, nameDataHashT[item]);
					}
				}
			}
			return allYinyangItems;
		}
	}

	static Hashtable allMedicines;
	public static Hashtable AllMedicines
	{
		get
		{
			if (allMedicines == null)
			{
				allMedicines = new Hashtable();
				foreach (int item in nameDataHashT.Keys)
				{
					if ((nameDataHashT[item] is Medicines))
					{
						allMedicines.Add(item, nameDataHashT[item]);
					}
				}
			}
			return allMedicines;
		}
	}

	public static Item GetItem(string name)
	{
		return (Item)nameDataHashT[name.GetHashCode()];
	}

	public static Item GetItemOrCreate(string name)
	{
		if (nameDataHashT.ContainsKey(name.GetHashCode()))
		{
			return (Item)nameDataHashT[name.GetHashCode()];
		}
		else
		{
			return new Item(name, "", ItemType.None, 9999, null, true);
		}
	}

	public static Dictionary<Item, int> useCount = new Dictionary<Item, int>();

	public int Id {get => MyName.GetHashCode();}
    public string originalName;
	public virtual string MyName
	{
		get => originalName; set => originalName = value;
	}

	public string desc;

    public Sprite icon;
	public ItemType itemType;

	public Specials onUse;

    public int maxStack;

	public ItemRarity rarity;

	

	public void SetRarity(ItemRarity val)
	{
		rarity = val;
		switch (val)
		{
			case ItemRarity.S:
				break;
			case ItemRarity.A:
				break;
			case ItemRarity.B:
				break;
			case ItemRarity.C:
				break;
			case ItemRarity.D:
				break;
			case ItemRarity.F:
				break;
			default:
				break;
		}
	}

	public Item(Item dat)
	{
		originalName = dat.originalName;
		desc = dat.desc;
		itemType = dat.itemType;
		maxStack = dat.maxStack;
		onUse = dat.onUse;
		icon = dat.icon;
	}

	public Item(string n, string d, ItemType iType, int max, Specials useFunc ,bool isNewItem)
	{
        originalName = n;
		desc = d;
		if (isNewItem)
		{
			InsertToTable();
			//경험치주기@@@@@@@@@@@@@@@@@@22
		}
		itemType = iType;
		maxStack = max;
		onUse = useFunc;
	}

	public virtual void InsertToTable()
	{
		if (!nameDataHashT.ContainsKey(Id))
		{
			Debug.Log($"새로운 아이템 : {MyName}");
			nameDataHashT.Add(Id, this);
		}
	}

	public override int GetHashCode()
	{
		return MyName.GetHashCode();
	}

	public static bool operator ==(Item left, Item right)
	{
		//Debug.Log($"{left?.MyName} == {right?.MyName} --> {left?.MyName == right?.MyName }");
		return left?.MyName == right?.MyName ;
	}

    public static bool operator !=(Item left, Item right)
    {
		return left?.MyName != right?.MyName ;
    }

    public override bool Equals(object obj)
	{
		Debug.Log("???");
		return obj is Item i && i.MyName == MyName;
	}

	public virtual void Use()
	{
		bool res = false;
		if (onUse?.onActivated != null && (res = onUse.onActivated.Invoke()))
		{
			Debug.Log("ITEM REMOVED");
			GameManager.instance.pinven.RemoveItem(this);
			GameManager.instance.qManager.InvokeOnChanged(CompletionAct.UseItem, MyName);
		}
		else
		{
			Debug.Log($"{onUse}");
			Debug.Log($"{onUse?.onActivated != null} && {res}"); //사용실패. 아이템 효과가 없거나 사용이 성공하지 못함.
		}
	}

	public void SetSprite(Sprite sp)
	{
		icon = sp;
	}

	public virtual int CompareTo(object obj)
	{
		if(obj is Item item)
		{
			if(originalName.CompareTo(item.originalName) == 0)
			{
				return 0;
			}
			else
				return originalName.CompareTo(item.originalName);
		}
		return 0;
	}
}

