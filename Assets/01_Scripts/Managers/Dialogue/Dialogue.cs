using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Dialogue : ScriptableObject
{
	public string text;
	public float typeDel;

	//public Character owner;

	public Dialogue next;

	StringBuilder sb = new StringBuilder();
	Coroutine ongoing;
	WaitForSeconds ws;

	protected virtual void Awake()
	{
		ws = new WaitForSeconds(typeDel);
	}

	public virtual void OnShown()
	{
		GameManager.instance.uiManager.dialogueUI.currentShown = this;
		ongoing = GameManager.instance.StartCoroutine(DelShowTxt());
	}

	public virtual void OnClick()
	{
		if (ongoing != null)
		{
			ImmediateShow();
		}
		else
		{
			NextDialogue();
		}
	}

	public virtual void ImmediateShow()
	{
		GameManager.instance.uiManager.dialogueUI.ShowText(text);
		sb.Clear();
		GameManager.instance.StopCoroutine(ongoing);
		ongoing = null;
	}

	public virtual void NextDialogue()
	{
		if(next != null)
		{
			next.OnShown();
		}
		else
		{
			GameManager.instance.uiManager.dialogueUI.Off();
		}
	}

	protected virtual IEnumerator DelShowTxt()
	{
		sb.Clear();
		int head = 0;
		while(head < text.Length)
		{
			yield return ws;
			sb.Append(text[head]);

			++head;

			GameManager.instance.uiManager.dialogueUI.ShowText(sb.ToString());
		}
		sb.Clear();
		ongoing = null;
	}
}
