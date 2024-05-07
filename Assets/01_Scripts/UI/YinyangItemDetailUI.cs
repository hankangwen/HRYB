using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class YinyangItemDetailUI : MonoBehaviour
{
	YinyangItem cur;

	Image image;
	TextMeshProUGUI itemName;
	TextMeshProUGUI itemDesc;
	UIPolygon statPolygon;
	Image moistGauge;
	Image poisonGauge;


	Transform content;

	public const string MAKABLEITEM = "CollectionItem";
	private void Awake()
	{
		gameObject.SetActive(false);
	}
	public void SetInfo(YinyangItem item)
	{
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
			content = transform.Find("ItemBack/ResultView/Content");
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

		gameObject.SetActive(true);

		image.sprite = item.icon;
		itemName.text = item.MyName;
		itemDesc.text = item.desc;
		statPolygon.VerticesDistances[0] = item.detailParams[DetailParameter.Sweet];
		statPolygon.VerticesDistances[1] = item.detailParams[DetailParameter.Sour];
		statPolygon.VerticesDistances[2] = item.detailParams[DetailParameter.Bitter];
		statPolygon.VerticesDistances[3] = item.detailParams[DetailParameter.Salty];
		statPolygon.VerticesDistances[4] = item.detailParams[DetailParameter.Spicy];
		statPolygon.SetVerticesDirty();
		moistGauge.fillAmount = item.detailParams[DetailParameter.Moist];
		poisonGauge.fillAmount = item.detailParams[DetailParameter.Poison];

		foreach (ItemAmountPair i in Crafter.recipeItemTableTrim.Keys)
		{
			if(i.info.originalName == item.originalName)
			{

				//HashSet<ItemAmountPair>
				//Crafter.recipeItemTableTrim[i];
				GameObject g = PoolManager.GetObject(MAKABLEITEM, content);
				CollectionButtonUI btn = g.GetComponent<CollectionButtonUI>();
				btn.SetInfo(item as YinyangItem);
				//buttons.Add(g);
			}
		}
		
		cur = item;

	}

	public void RefreshInfo()
	{
		SetInfo(cur);
	}
}
