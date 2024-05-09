using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButtonUI : MonoBehaviour
{
	Image img;
	TextMeshProUGUI itemName;
	GameObject mask;
	ItemCollection connected;

	string name;

	
	public void SetInfo(ItemCollection i)
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
		if(mask == null)
		{
			mask = transform.Find("Image/ImageMask").gameObject;
		}
		
		
		img.sprite = connected.myItem.icon;

		if(i.discovered)
		{
			mask.SetActive(false);
			itemName.text = connected.myItem.MyName;
		}
		else
		{
			mask.SetActive(true);
			itemName.text = "???";
		}
	}

	public void RefreshInfo()
	{
		SetInfo(connected);
	}

	public void OnClick()
	{

		GameManager.instance.uiManager.yinyangitemDetail.SetInfo(connected);
	}
}
