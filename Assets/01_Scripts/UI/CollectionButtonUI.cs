using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButtonUI : MonoBehaviour
{
	Image img;
	TextMeshProUGUI itemName;


	Item connected;
	HashSet<ItemAmountPair> reqItem;
	public void SetInfo(Item i)
	{
		connected = i;
		if (img == null)
		{
			img = transform.Find("Image").GetComponent<Image>();
		}
		if (itemName == null)
		{

			itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
		}

		img.sprite = connected.icon;
		itemName.text = connected.MyName;
	}

	public void OnClick()
	{
		if (connected is YinyangItem yyItem)
		{
			GameManager.instance.uiManager.yinyangitemDetail.SetInfo(yyItem);
		}
	}
}
