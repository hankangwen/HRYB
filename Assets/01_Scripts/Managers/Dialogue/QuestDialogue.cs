using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "대화/퀘스트")]
public class QuestDialogue : Dialogue
{
    public QuestInfo info;

	public override Dialogue Copy()
	{
		QuestDialogue ret = new QuestDialogue();
		ret.text = text;
		ret.typeDel = typeDel;
		ret.owner = owner;
		ret.next = next;
		ret.info = info;
		return ret;
	}

	public override void NextDialogue()
	{
		if (info)
		{
			QuestManager.AssignQuest(info.questName, owner);
			owner.latestQuest = info;
			owner.latestQuest.onCompleteAction.AddListener(() =>
			{
				if(owner.latestQuest == info)
				{
					owner.latestQuest = null;
					Debug.Log("QUEST REMOVEDEDED");
				}
				Debug.Log("QUEST NOTREMOVENVENV");
			});
		}
		base.NextDialogue();
	}
}
