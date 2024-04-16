using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class QuestSerializer : Editor
{
	public const string OUTPUTPATH = "/Output/";
	public const string OUTPUTFILENAMECSV = "QuestCSV.csv";
	public const string OUTPUTFILENAMETSV = "QuestTSV.tsv";

	//퀘스트정보
	public const string QUESTNAME = "이름";

	public const string COMPCONDITION = "조건";
	public const string PARAMETER = "세부";
	public const string ITEMHANDLEMODE = "제거여부";
	public const string DEFEATENEMYMODE = "처치조건";
	public const string DEFEATPARAMETER = "적 세부";
	public const string INVERTED = "반전";
	public const string COMPCOUNT = "필요횟수";


	public const string CONJUNCTION = "연결";
	
	//보상정보
	public const string REWARDTYPE = "보상";
	public const string REWARDAMOUNT = "갯수";

	//
	public const string REPEATABLECOUNT = "반복";


	static List<QuestInfo> quests = new List<QuestInfo>();


	[MenuItem("퀘스트/퀘스트 내보내기 (.csv)")]
	public static void DoSerialize()
	{
		quests = Resources.LoadAll<QuestInfo>(QuestManager.QUESTINFOPATH).ToList();
		CreateCSV();
	}

	[MenuItem("퀘스트/퀘스트 내보내기 (.tsv)")]
	public static void DoSerializeTSV()
	{
		quests = Resources.LoadAll<QuestInfo>(QuestManager.QUESTINFOPATH).ToList();
		CreateTSV();
	}

	public static void CreateCSV()
	{
		StreamWriter writer = new StreamWriter($"{Application.dataPath}{OUTPUTPATH}{OUTPUTFILENAMECSV}", false, System.Text.Encoding.UTF8);

		Debug.Log("파일에 쓰는중...");
		try
		{
			int maxConditionAmount = -1;
			int maxRewardAmount = -1;
			for (int i = 0; i < quests.Count; i++)
			{
				if (maxConditionAmount < quests[i].myInfo.Count)
				{
					maxConditionAmount = quests[i].myInfo.Count;
				}
				if (maxRewardAmount < quests[i].rewardInfo.Count)
				{
					maxRewardAmount = quests[i].rewardInfo.Count;
				}
			}

			if(maxConditionAmount < 0 || maxRewardAmount < 0)
			{
				throw new UnityException("조건 또는 보상이 없어 퀘스트를 볼 수 없습니다.");
			}

			writer.Write(QUESTNAME);
			writer.Write(',');

			for (int i = 0; i < maxConditionAmount; i++)
			{
				writer.Write(CombineAppendComma(i.ToString(), COMPCONDITION, PARAMETER, ITEMHANDLEMODE, DEFEATENEMYMODE, DEFEATPARAMETER, INVERTED, COMPCOUNT, CONJUNCTION));
			}

			for (int i = 0; i < maxRewardAmount; i++)
			{
				writer.Write(CombineAppendComma(i.ToString(), REWARDTYPE, REWARDAMOUNT));
			}

			writer.Write(REPEATABLECOUNT);
			writer.WriteLine();


			for (int i = 0; i < quests.Count; i++)
			{
				writer.Write($"{quests[i].questName},");

				for (int j = 0; j < quests[i].myInfo.Count; j++)
				{
					writer.Write
						($"{quests[i].myInfo[j].objective},{quests[i].myInfo[j].parameter},{QuestManager.ToStringKorean(quests[i].myInfo[j].itemMode)},{QuestManager.ToStringKorean(quests[i].myInfo[j].defeatMode)},{quests[i].myInfo[j].defeatParameter},{quests[i].myInfo[j].inverted},{quests[i].myInfo[j].repeatCount},{QuestManager.ToStringKorean(quests[i].myInfo[j].afterAct)},");
				}
				for (int j = 0; j < quests[i].rewardInfo.Count; j++)
				{
					writer.Write($"{QuestManager.ToStringKorean(quests[i].rewardInfo[j].rewardType)},{quests[i].rewardInfo[j].parameter},");
				}
				writer.Write($"{quests[i].completableCount}");
				writer.WriteLine();
			}
			Debug.Log($"{quests.Count} 개의 정보를 성공적으로 저장했습니다.");
		}
		finally
		{
			writer.Flush();
			writer.Close();
			Debug.Log("파일 쓰기 종료.");
		}
		
	}

	public static void CreateTSV()
	{
		StreamWriter writer = new StreamWriter($"{Application.dataPath}{OUTPUTPATH}{OUTPUTFILENAMETSV}", false, System.Text.Encoding.UTF8);

		Debug.Log("파일에 쓰는중...");
		try
		{
			int maxConditionAmount = -1;
			int maxRewardAmount = -1;
			for (int i = 0; i < quests.Count; i++)
			{
				if (maxConditionAmount < quests[i].myInfo.Count)
				{
					maxConditionAmount = quests[i].myInfo.Count;
				}
				if (maxRewardAmount < quests[i].rewardInfo.Count)
				{
					maxRewardAmount = quests[i].rewardInfo.Count;
				}
			}

			if (maxConditionAmount < 0 || maxRewardAmount < 0)
			{
				throw new UnityException("조건 또는 보상이 없어 퀘스트를 볼 수 없습니다.");
			}

			writer.Write(QUESTNAME);
			writer.Write(',');

			for (int i = 0; i < maxConditionAmount; i++)
			{
				writer.Write(CombineAppendComma(i.ToString(), COMPCONDITION, PARAMETER, ITEMHANDLEMODE, DEFEATENEMYMODE, DEFEATPARAMETER, INVERTED, COMPCOUNT, CONJUNCTION));
			}

			for (int i = 0; i < maxRewardAmount; i++)
			{
				writer.Write(CombineAppendComma(i.ToString(), REWARDTYPE, REWARDAMOUNT));
			}

			writer.Write(REPEATABLECOUNT);
			writer.WriteLine();


			for (int i = 0; i < quests.Count; i++)
			{
				writer.Write($"{quests[i].questName},");

				for (int j = 0; j < quests[i].myInfo.Count; j++)
				{
					writer.Write
						($"{quests[i].myInfo[j].objective},{quests[i].myInfo[j].parameter},{QuestManager.ToStringKorean(quests[i].myInfo[j].itemMode)},{QuestManager.ToStringKorean(quests[i].myInfo[j].defeatMode)},{quests[i].myInfo[j].defeatParameter},{quests[i].myInfo[j].inverted},{quests[i].myInfo[j].repeatCount},{QuestManager.ToStringKorean(quests[i].myInfo[j].afterAct)},");
				}
				for (int j = 0; j < quests[i].rewardInfo.Count; j++)
				{
					writer.Write($"{QuestManager.ToStringKorean(quests[i].rewardInfo[j].rewardType)},{quests[i].rewardInfo[j].parameter},");
				}
				writer.Write($"{quests[i].completableCount}");
				writer.WriteLine();
			}
			Debug.Log($"{quests.Count} 개의 정보를 성공적으로 저장했습니다.");
		}
		finally
		{
			writer.Flush();
			writer.Close();
			Debug.Log("파일 쓰기 종료.");
		}

	}

	public static string CombineAppendComma(string appendText, params string[] combiners)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < combiners.Length; i++)
		{
			sb.Append(combiners[i]);
			sb.Append(appendText);
			sb.Append(',');
		}
		return sb.ToString();
	}

	public static string CombineAppendTab(string appendText, params string[] combiners)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < combiners.Length; i++)
		{
			sb.Append(combiners[i]);
			sb.Append(appendText);
			sb.Append('\t');
		}
		return sb.ToString();
	}
}
