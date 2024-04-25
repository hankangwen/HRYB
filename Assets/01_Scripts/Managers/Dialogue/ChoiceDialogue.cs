using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "대화/선택지")]
public class ChoiceDialogue : Dialogue
{

	public List<string> choiceOptions;

	public List<Dialogue> nexts;

	bool choosing = false;


	public override Dialogue Copy()
	{
		ChoiceDialogue ret = new ChoiceDialogue();
		ret.text = text;
		ret.typeDel = typeDel;
		ret.owner = owner;
		ret.next = next;
		ret.choiceOptions = choiceOptions;
		ret.nexts = nexts;
		return ret;
	}

	protected override IEnumerator DelShowTxt()
	{
		yield return base.DelShowTxt();
		yield return ShowChoice();
	}

	public override void OnClick()
	{
		if (!choosing)
		{
			base.OnClick();
		}
	}

	IEnumerator ShowChoice()
	{
		
		GameManager.instance.uiManager.dialogueUI.ShowChoice(choiceOptions);
		choosing = true;
		yield return new WaitUntil(()=> GameManager.instance.uiManager.dialogueUI.chosen != -1);
		next = nexts[GameManager.instance.uiManager.dialogueUI.chosen];
		GameManager.instance.uiManager.dialogueUI.chosen = -1;
		GameManager.instance.uiManager.dialogueUI.OffChoice();
		choosing =false;
		Debug.LogError("아오");


		///아무튼 다음 대사를 선택지로 정하고 그걸로 띄우기.
		
		//next = nexts[0];
		yield return null;
		NextDialogue();
	}
}
