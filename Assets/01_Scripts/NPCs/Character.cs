using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "캐릭터 데이터")]

//이게 없으면 말을 못할듯?
public class Character : ScriptableObject
{
	internal Actor self;

    public string baseName;
	public string subName;

	public Dialogue talkData;
	
	public void OnTalk()
	{
		GameManager.instance.uiManager.dialogueUI.talker = this;
		talkData.OnShown();

		//애니메이션을 재생하든 뭘 하든?
	}
}
