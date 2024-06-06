using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Choist : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Sprite hoverSprite;
	Sprite original;
	
	Image icon;

	public int choiceNum;

	public TMP_Text choiceTxt;


	public void SetText(string text)
	{
		if(choiceTxt == null) 
		{
			choiceTxt = GetComponentInChildren<TMP_Text>();
			
		}

		if (icon == null)
		{
			icon = GetComponent<Image>();
			original = icon.sprite;
		}
		choiceTxt.text = text;
	}

	public void OnChoiceButtonDown()
	{
		GameManager.instance.uiManager.dialogueUI.chosen = choiceNum;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		icon.sprite = hoverSprite;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		icon.sprite=  original;
	}
}
