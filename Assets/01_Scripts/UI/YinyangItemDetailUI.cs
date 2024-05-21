using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;



public class YinyangItemDetailUI : MonoBehaviour
{
	ItemCollection cur;

	Image image;
	
	TextMeshProUGUI itemName;
	TextMeshProUGUI itemDesc;
	
	UIPolygon statPolygon;
	
	Image moistGauge;
	Image poisonGauge;

	GameObject maskItem;
	GameObject maskGroup;
	List<GameObject> buttons = new List<GameObject>();

	Transform content;

	public const string MAKABLEITEM = "GettableItem";
	private void Awake()
	{
		gameObject.SetActive(false);
	}
	public void SetInfo(ItemCollection item)
	{

		for (int i = 0; i < buttons.Count; i++)
		{
			PoolManager.ReturnObject(buttons[i]);
		}
		buttons.Clear();

		if (image == null)
		{
			image = transform.Find("Img/ItemImg").GetComponent<Image>();
		}
		if (itemName == null)
		{
			itemName = transform.Find("Name/ItemName").GetComponent<TextMeshProUGUI>();
		}
		if(itemDesc == null)
		{
			itemDesc = transform.Find("Desc/DescText").GetComponent<TextMeshProUGUI>();
		}
		if (content == null)
		{
			content = transform.Find("ItemBack/ResultView/Viewport/Content");
		}
		if (statPolygon == null)
		{
			statPolygon = transform.Find("StatBack/StatPolygon").GetComponent<UIPolygon>();
		}
		if(moistGauge == null)
		{
			moistGauge = transform.Find("StatBack/MoistGauge").GetComponent<Image>();
		}
		if(poisonGauge == null)
		{
			poisonGauge = transform.Find("StatBack/PoisonGauge").GetComponent<Image>();
		}
		if (maskItem == null)
		{
			maskItem = transform.Find("Img/ItemImg/ItemMask").gameObject;
		}
		if (maskGroup == null)
		{
			maskGroup = transform.Find("StatBack/MaskGroup").gameObject;
		}

		gameObject.SetActive(true);

		

		image.sprite = item.myItem.icon;
		itemName.text = item.myItem.MyName;
		itemDesc.text = item.myItem.desc;
		statPolygon.VerticesDistances[0] = ((YinyangItem)item.myItem).detailParams[DetailParameter.Sweet];
		statPolygon.VerticesDistances[1] = ((YinyangItem)item.myItem).detailParams[DetailParameter.Sour];
		statPolygon.VerticesDistances[2] = ((YinyangItem)item.myItem).detailParams[DetailParameter.Bitter];
		statPolygon.VerticesDistances[3] = ((YinyangItem)item.myItem).detailParams[DetailParameter.Salty];
		statPolygon.VerticesDistances[4] = ((YinyangItem)item.myItem).detailParams[DetailParameter.Spicy];
		statPolygon.SetVerticesDirty();
		moistGauge.fillAmount = ((YinyangItem)item.myItem).detailParams[DetailParameter.Moist];
		poisonGauge.fillAmount = ((YinyangItem)item.myItem).detailParams[DetailParameter.Poison];

		foreach (Item i in item.ResultItems)
		{
			

			if(i is YinyangItem)
			{
				GameObject g = PoolManager.GetObject(MAKABLEITEM, content);
				CollectionButtonUI btn = g.GetComponent<CollectionButtonUI>();
				btn.SetInfo(GameManager.instance.pedia.materialCollections[(YinyangItem)i]);
				buttons.Add(g);
			}
		}

		if (item.discovered)
		{
			maskItem.SetActive(false);
			maskGroup.SetActive(false);
		}
		else
		{
			maskItem.SetActive(true);
			maskGroup.SetActive(true);
		}

		cur = item;

	}

	public void RefreshInfo()
	{
		SetInfo(cur);
	}
}
