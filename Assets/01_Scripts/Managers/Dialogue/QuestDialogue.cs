using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "대화/퀘스트")]
public class QuestDialogue : Dialogue
{
    public QuestInfo info;

	public override void NextDialogue()
	{
		if (info)
		{
			QuestManager.AssignQuest(info.questName);
		}
		base.NextDialogue();
	}
}
