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

	Canvas parent;

	private void Awake()
	{
		hp = transform.Find("HPBar/HP").GetComponent<Image>();
		lf = GetComponentInParent<LifeModule>();
		nameText = transform.Find("NameBack/NameText").GetComponent<TextMeshProUGUI>();
		whiteAdequity = transform.Find("BlackBack/WhiteBack").GetComponent<Image>();
		parent = GetComponentInParent<Canvas>();

		nameText.text = lf.gameObject.name;
	}

	private void Update()
	{
		hp.fillAmount = lf.yy.white.Value / lf.yy.white.MaxValue;
		whiteAdequity.fillAmount = lf.adequity.white.Value / (lf.adequity.white.Value + lf.adequity.black.Value);

		if (lf.yy.white.Value >= lf.yy.white.MaxValue || lf.yy.white.Value <= 0)
		{
			parent.enabled = false;
		}
		else
		{
			parent.enabled = true;
		}
		
	}
}
