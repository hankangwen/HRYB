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

	[Header("퀘스트 중 교체될 대사.")]
	public Dialogue questingDia;

	[Header("퀘스트 후 교체될 대사.")]
	public Dialogue questedDia;

	[Header("퀘스트 설명. 한 퀘스트당 하나")]
	public string descriptions;

	internal int curCompletedAmount;
	internal Character giver;

	public bool IsDeprived => completableCount >= 0 && curCompletedAmount >= completableCount;

	protected virtual void OnEnable()
	{
		ResetQuestCall();
		ResetQuestStartTime();
	}

	public void ResetQuestStartTime(CompletionAct cond = CompletionAct.None)
	{
		for (int i = 0; i < myInfo.Count; i++)
		{
			if(cond == CompletionAct.None || myInfo[i].objective == cond)
			{
				myInfo[i].OnResetTime();
			}
		}
	}

	public void ResetQuestCall(CompletionAct cond = CompletionAct.None)
	{
		for (int i = 0; i < myInfo.Count; i++)
		{
			if (cond == CompletionAct.None || myInfo[i].objective == cond)
			{
				myInfo[i].OnResetCall();

			}
		}
	}
	public QuestInfo()
	{
		questName = "";
		myInfo = new List<CompleteAtom>();
		rewardInfo = new List<RewardAtom>();
		completableCount = 1;
	}

	public QuestInfo(QuestInfo copy)
	{
		if(copy != null)
		{
			questName = copy.questName;
			myInfo = new List<CompleteAtom>(copy.myInfo);
			rewardInfo = new List<RewardAtom>(copy.rewardInfo);
			completableCount = copy.completableCount;
		}
		else
		{
			questName = "";
			myInfo = new List<CompleteAtom>();
			rewardInfo = new List<RewardAtom>();
			completableCount = 1;
		}
	}

	public bool Notify(CompletionAct data, string parameter, int amt)
	{
		bool res = false;
		for (int i = 0; i < myInfo.Count; i++)
		{
			res |= myInfo[i].OnNotification(data, parameter, amt);
		}
		return res;
	}

	public bool ExamineCompleteStatus()
	{
		bool compStat = false;

		int head = 0;
		bool valid = true;
		AfterComplete prevConnection = AfterComplete.FINAL;
		while (valid)
		{
			if(myInfo[head].examineStartTime == null)
			{
				myInfo[head].examineStartTime = (int)Time.time;
			}

			bool res = myInfo[head].isCompleted;
			Debug.Log(myInfo[head].objective + " : " + res);
			if(head == 0)
			{
				compStat = res;
			}

			switch (prevConnection)
			{
				case AfterComplete.AND:
					compStat &= res;
					break;
				case AfterComplete.OR:
					compStat |= res;
					break;
				case AfterComplete.AFTER:
					if (!compStat)
					{
						myInfo[head].Freeze();
						return false;
					}
					else
					{
						myInfo[head].UnFreeze();
						compStat &= res;
					}
					break;
				default:
					break;
			}

			prevConnection = myInfo[head].afterAct;

			if(prevConnection == AfterComplete.FINAL)
				valid = false;
			++head;
		}

		return compStat;
	}

	public void GiveReward()
	{
		GameManager.instance.qManager.InvokeOnChanged(CompletionAct.ClearQuest, questName);

		for (int i = 0; i < myInfo.Count; i++)
		{
			if(myInfo[i].objective == CompletionAct.GetItem || myInfo[i].objective == CompletionAct.HaveItem)
			{
				if(myInfo[i].itemMode == ItemHandleMode.Remove)
				{
					GameManager.instance.pinven.RemoveItem(Item.GetItem(myInfo[i].parameter), myInfo[i].repeatCount);
				}
			}
		}

		for (int i = 0; i < rewardInfo.Count; i++)
		{
			switch (rewardInfo[i].rewardType)
			{
				case RewardType.Exp:
					{
						Debug.Log($"경험치 {rewardInfo[i].parameter} 제공함");
					}
					break;
				case RewardType.Skill:
					{
						Debug.Log($"스킬 {rewardInfo[i].parameter} 제공함");

					}
					break;
				case RewardType.Item:
					{
						Debug.Log($"아이템 {rewardInfo[i].parameter} 제공함");
						GameManager.instance.pinven.AddItem(Item.GetItem(rewardInfo[i].parameter), rewardInfo[i].amount);
					}
					break;
				case RewardType.HealWhite:
					{
						int amt = int.Parse(rewardInfo[i].parameter);
						GameManager.instance.pActor.life.DamageYY(0, -amt, DamageType.NoHit);
					}
					break;
				case RewardType.HealBlack:
					{
						int amt = int.Parse(rewardInfo[i].parameter);
						GameManager.instance.pActor.life.DamageYY(-amt, 0, DamageType.NoHit);
					}
					break;
				case RewardType.Quest:
					{
						QuestManager.AssignQuest(rewardInfo[i].parameter);
					}
					break;
				default:
					break;
			}
		}

		if(giver != null && questedDia != null)
		{
			giver.SetDialogue(questedDia);
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
