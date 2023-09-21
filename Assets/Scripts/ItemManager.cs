using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;

	private void Awake()
	{
		items.Add(new Item("��������", ItemType.Solid, StackType.numScale, 10));
		items.Add(new YinyangItem("�λ�", ItemType.Solid, StackType.numScale, 5, '��'));
		items.Add(new YinyangItem("��", ItemType.Solid, StackType.numScale, 10, '��'));
		items.Add(new Item("����", ItemType.Solid, StackType.numScale, 10));
	}
}
