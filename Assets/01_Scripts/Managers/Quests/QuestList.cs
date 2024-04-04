using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestList")]
public class QuestList : ScriptableObject
{
	[Header("게임 전체 퀘스트 목록.")]
	public List<QuestInfo> allQuests;
}
