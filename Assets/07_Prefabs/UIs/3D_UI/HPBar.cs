using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
	public LifeModule lf;
	public Image hpBarBlack;
	public Image blackBack;
	public Image whiteBack;
	public Image hpBarWhite;

	float value = 0;
	const float fullHpwidth = 300;


	private void Awake()
	{

		hpBarBlack = transform.Find("BlackHP/Black").GetComponent<Image>();
		blackBack = transform.Find("BlackHP/Background").GetComponent<Image>();
		hpBarWhite = transform.Find("WhiteHP/White").GetComponent<Image>();
		whiteBack = transform.Find("WhiteHP/Background").GetComponent<Image>();


		lf = GetComponentInParent<LifeModule>();

		//비율에 따라 HP바 크기 재조정
		if(lf.initYinYang.white > lf.initYinYang.black)
		{
			value = lf.initYinYang.black / lf.initYinYang.white;
			ResizeHpBar(value, blackBack, hpBarBlack);
		}
		else
		{
			value = lf.initYinYang.white / lf.initYinYang.black;
			ResizeHpBar(value, whiteBack, hpBarWhite);
		}
	}

	private void Update()
	{
		hpBarWhite.fillAmount = lf.yy.white / lf.initYinYang.white;
		hpBarBlack.fillAmount = lf.yy.black / lf.initYinYang.black;
		

		//HP가 만땅이거나, 0이면 숨기기
		if ((lf.yy.white >= lf.initYinYang.white && lf.yy.black >= lf.initYinYang.black) || (lf.yy.white <= 0 && lf.yy.black <= 0))
			this.GetComponentInParent<Canvas>().enabled = false;
		else
			this.GetComponentInParent<Canvas>().enabled = true;
		
	}


	private void ResizeHpBar(float sizeVlaue, Image background, Image hpbar)
	{
		hpbar.rectTransform.sizeDelta = new Vector2(fullHpwidth * sizeVlaue, hpbar.rectTransform.sizeDelta.y);
		hpbar.rectTransform.anchoredPosition = new Vector2(-(fullHpwidth * sizeVlaue) / 2, hpbar.rectTransform.anchoredPosition.y);

		background.rectTransform.sizeDelta = new Vector2(fullHpwidth * sizeVlaue, background.rectTransform.sizeDelta.y);
		background.rectTransform.anchoredPosition = new Vector2(-(fullHpwidth * sizeVlaue) / 2, background.rectTransform.anchoredPosition.y);
	}
}
