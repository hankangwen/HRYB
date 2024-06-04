using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "대화/일반")]
public class Dialogue : ScriptableObject
{
	public string text;
	public float typeDel;

	public Character owner;

	public Dialogue next;

	protected StringBuilder sb = new StringBuilder();
	protected Coroutine ongoing;

	WaitForSeconds ws;

	private readonly int talkingHash = Animator.StringToHash("Talking");

	public virtual Dialogue Copy()
	{
		Dialogue ret = new Dialogue();
		ret.text = text;
		ret.typeDel = typeDel;
		ret.owner = owner;
		ret.next = next;
		return ret;
	}

	public virtual void OnShown(Character owner)
	{
		if(ws == null)
		{
			ws = new WaitForSeconds(typeDel);
		}
		this.owner = owner;
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
		GameManager.instance.StopCoroutine(ongoing);
		GameManager.instance.uiManager.dialogueUI.ShowText(text);
		sb.Clear();
		ongoing = null;
	}

	public virtual void NextDialogue()
	{
		if(next != null)
		{
			next.OnShown(owner);
		}
		else
		{
			GameManager.instance.uiManager.dialogueUI.Off();
			owner.self.anim.Animators.SetBool(talkingHash, false);
		}
	}

	protected virtual IEnumerator DelShowTxt()
	{
		sb.Clear();
		int head = 0;
		while(head < text.Length)
		{
			while(text[head] == '<')
			{
				bool flag = true;
				while(flag)
				{
					sb.Append(text[head]);
					if(text[head] == '>')
						flag = false;
					++head;
					
				}
			}
			yield return ws;
			sb.Append(text[head]);
			//Debug.Log(sb.ToString());

			++head;

			GameManager.instance.uiManager.dialogueUI.ShowText(sb.ToString());
		}
		sb.Clear();
		ongoing = null;
	}
}
