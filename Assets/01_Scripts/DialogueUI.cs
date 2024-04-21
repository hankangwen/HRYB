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

	public int chosen = -1;

	public Dialogue currentShown;
	public Character talker;

	private void Start()
	{
		Contents = transform.GetChild(2).GetComponent<TMP_Text>();
	}


	private void Update()
	{
		if(Input.GetMouseButton(0))
		{
			currentShown?.OnClick();
		}
	}

	public void ShowText(string text)
	{
		Contents.text = text;
	}

	public void Off()
	{
		this.gameObject.SetActive(false);
	}

	public void ShowChoice(List<string> choice)
	{
		for(int i = choice.Count - 1; i >= 0; i--) 
		{
			GameObject cc = Instantiate(choisePrefab, choiseList.transform);
			cc.GetComponent<Choist>().SetText(choice[i]);
			cc.GetComponent<Choist>().choiceNum = i;
		}
	}

	public void OffChoice()
	{
		
		choiseList.SetActive(false);
	}

}
