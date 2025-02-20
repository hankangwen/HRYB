using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeDetailUI : MonoBehaviour
{
    public string displayName;

	public string displayReqAmt; //높은 확률로 이후 바뀔듯.

	TextMeshProUGUI nameTxt;
	NeededResource reqTxt;

	private void Awake()
	{
		OffDetail();
	}


	public void ShowDetail(PlayerNode info, Vector3 p)
	{
		transform.position = p;
		gameObject.SetActive(true);
		if(nameTxt == null)
		{
			nameTxt = transform.Find("Names/NodeName").GetComponent<TextMeshProUGUI>();
		}
		if (reqTxt == null)
		{
			reqTxt = transform.Find("Neededs/NeededResource").GetComponent<NeededResource>();
		}

		nameTxt.text = NodeUtility.GetName(info);
		if(info == null)
			return;
		reqTxt.SetInfo(((int)info.needPoint));
	}

	public void OffDetail()
	{
		gameObject.SetActive(false);
	}
}
