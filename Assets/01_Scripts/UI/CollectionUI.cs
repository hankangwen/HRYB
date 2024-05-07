using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CollectionUI : MonoBehaviour, IOpenableWindowUI
{
	Transform content;
	public const string COLLECTIONBUTTON = "CollectionItem";
	
	List<GameObject> buttons = new List<GameObject>();

	private void Awake()
	{
		content = transform.Find("ItemView/Viewport/Content");
	}

	public void OnOpen()
	{
		foreach (Item item in Item.AllYinyangItems.Values)
		{
			GameObject g = PoolManager.GetObject(COLLECTIONBUTTON, content);
			CollectionButtonUI btn = g.GetComponent<CollectionButtonUI>();
			btn.SetInfo(item as YinyangItem);
			buttons.Add(g);
		}
	}

	public void OnClose()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			PoolManager.ReturnObject(buttons[i]);
		}
		buttons.Clear();
	}

	public void WhileOpening()
	{
		
	}

	public void Refresh()
	{
		
	}
}
