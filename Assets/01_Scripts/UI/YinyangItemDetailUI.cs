using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YinyangItemDetailUI : MonoBehaviour
{
	YinyangItem cur;
	HashSet<ItemAmountPair> reqs;

	Image image;
	TextMeshProUGUI itemName;
	Transform content;

	List<ItemReqUI> reqItems = new List<ItemReqUI>();

	const string REQITEM = "ReqItem";
	private void Awake()
	{
		gameObject.SetActive(false);
	}
	public void SetInfo(YinyangItem med, bool isRefresh = false)
	{
		if (image == null)
		{
			image = transform.Find("Image").GetComponent<Image>();
		}
		if (itemName == null)
		{
			itemName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
		}
		if (content == null)
		{
			content = transform.Find("Req/Viewport/Content");
		}


		for (int i = 0; i < reqItems.Count; i++)
		{
			PoolManager.ReturnObject(reqItems[i].gameObject);
		}
		reqItems.Clear();

		if (med == null || (cur == med && !isRefresh))
		{
			gameObject.SetActive(false);
			cur = null;
		}
		else
		{
			gameObject.SetActive(true);

			image.sprite = med.icon;
			itemName.text = med.MyName;
			bool res = true;

			cur = med;
		}

	}

	public void RefreshInfo()
	{
		SetInfo(cur, true);
	}
}
