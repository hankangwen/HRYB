using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
	public LifeModule lf;
	public Image hpBarBlack;
	public Image hpBarWhite;

	float value = 0;

	private void Awake()
	{

		hpBarBlack = transform.Find("BlackHP/Black").GetComponent<Image>();
		hpBarWhite = transform.Find("WhiteHP/White").GetComponent<Image>();
		
		
		lf = GetComponentInParent<LifeModule>();

		if(lf.initYinYang.white > lf.initYinYang.black)
		{
			hpBarBlack.rectTransform.sizeDelta = new Vector2();
			value = lf.initYinYang.black / lf.initYinYang.white;
		}
		else
		{

		}
	}

	private void Update()
	{
		hpBarWhite.fillAmount = lf.yy.white / lf.initYinYang.white;
		hpBarBlack.fillAmount = lf.yy.black / lf.initYinYang.black;

		

		if ((lf.yy.white >= lf.initYinYang.white && lf.yy.black >= lf.initYinYang.black) || (lf.yy.white <= 0 && lf.yy.black <= 0))
		{
			this.GetComponentInParent<Canvas>().enabled = false;
		}
		else
		{
			this.GetComponentInParent<Canvas>().enabled = true;
		}
	}
}
