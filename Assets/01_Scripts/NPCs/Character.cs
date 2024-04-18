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

	public Dialogue talkData;
	
	public void OnTalk()
	{
		GameManager.instance.uiManager.dialogueUI.talker = this;
		//GameManager.instance.uiManager.dialogueUI.talker = this;
		Debug.Log($"{FullName}이 말하는 중 ...");
		talkData.OnShown();
	}
}
