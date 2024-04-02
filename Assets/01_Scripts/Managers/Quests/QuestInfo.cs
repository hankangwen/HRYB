using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestInfo : ScriptableObject
{
	[Header("이름")]
	public string questName;

	[Header("퀘스트 정보. 무조건 앞에서부터 판단함.")]
	public List<CompleteAtom> myInfo;

	[Header("보상 정보.")]
	public List<RewardAtom> rewardInfo;

	[Header("완료 가능 횟수. 0이하일 경우 무한 가능")]
	public int completableCount;

	public UnityEvent onCompleteAction;
	public UnityEvent onAssignedAction;
	public UnityEvent onDismissedAction;



	internal int curCompletedAmount;

	public bool IsDeprived => completableCount >= 0 && curCompletedAmount >= completableCount;

	

	public bool ExamineCompleteStatus() //@@@@@@@@@@@@@@@@@ 이후 게임 로거 또는 퀘스트 트래커를 제작하며 조건을 구할듯.
	{
		bool compStat = false;

		int head = 0;
		while (myInfo[head].afterAct != AfterComplete.FINAL)
		{
			switch (myInfo[head].objective)
			{
				case CompletionAct.None:
					throw new UnityException($"{name} 에서 옳지 않은 완료 조건이 감지됨 : {myInfo[head].objective}");
				case CompletionAct.GetItem:
					break;
				case CompletionAct.HaveItem:
					break;
				case CompletionAct.LoseItem:
					break;
				case CompletionAct.UseItem:
					break;
				case CompletionAct.DefeatTarget:
					break;
				case CompletionAct.CountSecond:
					break;
				case CompletionAct.CountMinute:
					break;
				case CompletionAct.MoveTo:
					break;
				case CompletionAct.InteractWith:
					break;
				case CompletionAct.BelowLevel:
					break;
				case CompletionAct.EqualLevel:
					break;
				case CompletionAct.AboveLevel:
					break;
				default:
					break;
			}
		}

		return compStat;
	}

	public void GiveReward()
	{
		for (int i = 0; i < rewardInfo.Count; i++)
		{
			switch (rewardInfo[i].rewardType)
			{
				case RewardType.Exp:
					Debug.Log($"경험치 {rewardInfo[i].parameter} 제공함");
					break;
				case RewardType.Skill:
					Debug.Log($"스킬 {rewardInfo[i].parameter} 제공함");
					break;
				case RewardType.Item:
					Debug.Log($"아이템 {rewardInfo[i].parameter} 제공함");
					break;
				default:
					break;
			}
		}

		onCompleteAction?.Invoke();
	}

	public override bool Equals(object other)
	{
		return other is QuestInfo info && info.questName == this.questName;
	}

	public override string ToString()
	{
		return questName;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine(questName);
	}
}
