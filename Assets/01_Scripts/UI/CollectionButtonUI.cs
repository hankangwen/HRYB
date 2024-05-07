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
	public void SetInfo(Item i)
	{
		Debug.LogError("Set");
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

		if(connected is YinyangItem yinyang)
		GameManager.instance.uiManager.yinyangitemDetail.SetInfo(yinyang);
	}
}
