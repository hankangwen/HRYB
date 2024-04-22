using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
	TMP_Text Title;
	TMP_Text SubTitle;
	TMP_Text Contents;

	public Choist choisePrefab;
	public GameObject choiseList;

	public int chosen = -1;

	public Dialogue currentShown;
	public Character talker;

	List<Choist> choists = new List<Choist>();

	bool stat;
	bool choiceStat;

	private void Awake()
	{
		Contents = transform.GetChild(2).GetComponent<TMP_Text>();
	}

	private void Start()
	{
		Off();
	}


	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			currentShown?.OnClick();
		}
	}

	public void ShowText(string text)
	{
		if (!stat)
		{
			this.gameObject.SetActive(true);
			stat = true;
			GameManager.instance.UnLockCursor();
			GameManager.instance.DisableCtrl(ControlModuleMode.Status);
			GameManager.instance.camManager.FreezeCamX(true);
		}

		Contents.text = text;
	}

	public void Off()
	{
		this.gameObject.SetActive(false);
		choiseList.SetActive(false);
		currentShown = null;
		talker = null;
		chosen = -1;
		stat = false;
		choiceStat = false;
		GameManager.instance.LockCursor();
		GameManager.instance.EnableCtrl(ControlModuleMode.Status);
		GameManager.instance.camManager.UnfreezeCamX();
	}

	public void ShowChoice(List<string> choice)
	{
		if (!choiceStat)
		{
			choiseList.SetActive(true);
		}
		for(int i = choice.Count - 1; i >= 0; i--) 
		{
			Choist cc = Instantiate(choisePrefab, choiseList.transform);
			cc.SetText(choice[i]);
			cc.choiceNum = i;
			choists.Add(cc);
		}
	}

	public void OffChoice()
	{
		choiceStat = false;
		for (int i = 0; i < choists.Count; i++)
		{
			Destroy(choists[i].gameObject);
		}
		choists.Clear();
		
		choiseList.SetActive(false);
	}

}
