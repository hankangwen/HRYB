using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
	
    public void RefreshAvailQuests()
	{
		//
		Dictionary<QuestType, SortedSet<QuestInfo>> typeQuestPair = new Dictionary<QuestType, SortedSet<QuestInfo>>();
		for (int i = ((int)QuestType.Main); i <= ((int)QuestType.Sub); i++)
		{
			typeQuestPair.Add((QuestType)i, new SortedSet<QuestInfo>());
		}
		for (int i = 0; i < GameManager.instance.qManager.currentAbleQuest.Count; i++)
		{
			typeQuestPair[GameManager.instance.qManager.currentAbleQuest[i].type].Add(GameManager.instance.qManager.currentAbleQuest[i]);
		}
		foreach (var item in typeQuestPair)
		{

			foreach (var quest in item.Value)
			{

			}
		}
	}

	public void RefreshQuestUIState()
	{
		
	}
}
