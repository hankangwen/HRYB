using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "캐릭터 데이터")]

//이게 없으면 말을 못할듯?
public class Character : ScriptableObject
{
	internal Actor self;

	public string FullName
	{
		get => $"{subName} {baseName}";
	} 

    public string baseName;
	public string subName;

	public Dialogue initDia;

	bool? q = null;
	public bool Questing
	{
		get
		{
			if(q == null)
			{
				q = false;
				Dialogue nxt = initDia;
				while (true)
				{
					nxt = nxt.next;
					if(nxt == null)
					{
						break;
					}
					if((nxt as QuestDialogue) != null)
					{
						q = true;
						break;
					}

				}
			}
			return (bool)q;
		}
	}

	public bool prevQuesting;

	Dialogue dia;

	private void Awake()
	{
		prevQuesting = Questing;
		q = null;
		dia = initDia.Copy();
	}

	public void SetDialogue(Dialogue dia)
	{
		prevQuesting = Questing;
		q = null;
		this.dia = dia;
	}

	public void ResetDialogue()
	{
		prevQuesting = Questing;
		q = null;
		dia = initDia.Copy();
	}
	
	public void OnTalk()
	{
		GameManager.instance.uiManager.dialogueUI.talker = this;
		//GameManager.instance.uiManager.dialogueUI.talker = this;
		Debug.Log($"{FullName}이 말하는 중 ...");
		dia.OnShown(this);
	}
}
