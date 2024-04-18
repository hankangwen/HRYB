using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
	TMP_Text Title;
	TMP_Text SubTitle;
	TMP_Text Contents;

	public GameObject choisePrefab;
	public GameObject choiseList;

	public int chosen;

	public Dialogue currentShown;
	public Character talker;

	public void ShowText(string text)
	{
		Contents.text = text;
	}

	public void Off()
	{
		this.gameObject.SetActive(false);
	}

	public void ShowChoice(List<string> choise)
	{
		for(int i = choise.Count - 1; i >= 0; i--) 
		{
			GameObject cc = Instantiate(choisePrefab, choiseList.transform);
			cc.GetComponent<Choist>().choiseTxt.text = choise[i];
			cc.GetComponent<Choist>().choiseNum = i;
		}
	}

	public void OffChoice()
	{
		choiseList.SetActive(false);
	}

}
