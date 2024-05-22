using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HPBar : MonoBehaviour
{
	LifeModule lf;
	Image hp;
	TextMeshProUGUI nameText;

	Image whiteAdequity;

	private void Awake()
	{
		hp = transform.Find("HPBar/HP").GetComponent<Image>();
		lf = GetComponentInParent<LifeModule>();
		nameText = transform.Find("NameBack/NameText").GetComponent<TextMeshProUGUI>();
		whiteAdequity = transform.Find("BlackBack/WhiteBack").GetComponent<Image>();

		nameText.text = lf.gameObject.name;
	}

	private void Update()
	{
		hp.fillAmount = lf.yy.white / lf.initYinYang.white;
		whiteAdequity.fillAmount = lf.initAdequity.white / (lf.initAdequity.white + lf.initAdequity.black);

		if (lf.yy.white == lf.initYinYang.white || lf.yy.white <= 0)
		{
			this.GetComponentInParent<Canvas>().enabled = false;
		}
		else
		{
			this.GetComponentInParent<Canvas>().enabled = true;
		}
		
	}
}
