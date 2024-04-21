using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choist : MonoBehaviour
{
	public int choiceNum;

	public TMP_Text choiceTxt;

	public void SetText(string text)
	{
		if(choiceTxt == null) 
		{
			choiceTxt = GetComponentInChildren<TMP_Text>();
		}
		choiceTxt.text = text;
	}

	public void OnChoiceButtonDown()
	{
		GameManager.instance.uiManager.dialogueUI.chosen = choiceNum;
	}


}
