using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StackType
{
    numScale,
    litScale, 

}

public enum ItemType
{
	None = -1,
	Solid,
	Liquid,
	Air,
	Jelly,
}

[System.Serializable]
public class Item
{
    public static Hashtable nameDataHashT = new Hashtable()
	{
		{"��������".GetHashCode(), new Item("��������", ItemType.Solid, StackType.numScale, 10, false) },
		{"�λ�".GetHashCode(), new YinyangItem("�λ�", ItemType.Solid, StackType.numScale, 5, false, '��') },
		{"��".GetHashCode(), new YinyangItem("��", ItemType.Solid, StackType.numScale, 10, false, '��')},
		{"����".GetHashCode(), new Item("����", ItemType.Solid, StackType.numScale, 10, false) },
	}; //���� �̸��� �������� ���� �������� ����ϱ� ���� ���.
    public int Id {get => myName.GetHashCode();}
    public string myName;

    public Sprite icon;
	public ItemType itemType;
    public StackType stackType;

    public int maxStack;

    public Item(string n, ItemType iType, StackType sType, int max, bool isLateInit)
	{
        myName = n;
		if (isLateInit)
		{
			if (!nameDataHashT.ContainsKey(Id))
			{
				nameDataHashT.Add(Id, this);
			}
		}
		
		itemType = iType;
		stackType = sType;
		maxStack = max;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public static bool operator ==(Item left, Item right)
	{
		Debug.Log($"{left?.myName} == {right?.myName} ? {left?.myName == right?.myName}");
		return left?.myName == right?.myName;
	}

    public static bool operator !=(Item left, Item right)
    {
		Debug.Log($"{left?.myName} != {right?.myName} ? {left?.myName != right?.myName}");
		return left?.myName != right?.myName;
    }

    public override bool Equals(object obj)
	{
		bool res = (obj as Item)?.Id == this.Id;
		if ((obj as Item)?.Id == null)
		{
			return false;
		}
		return res;
	}
}
