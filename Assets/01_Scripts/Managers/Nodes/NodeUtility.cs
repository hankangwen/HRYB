using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StatUpgradeType
{
	White,
	Black,
	Atk,
	MoveSpeed,
	CooldownRdc,

	Callback
}

public class NodeUtility
{

	public const string NODEPATH = "Nodes/";

	public const string NODEFILENAME = "Node";

	public static List<List<PlayerNode>> LoadNodeData()
	{
		List<List<PlayerNode>> res = new List<List<PlayerNode>>();
		List<PlayerNode> datas = Resources.LoadAll<PlayerNode>(NODEPATH).ToList();

		string[] splitter;
		for (int i = 0; i < datas.Count; i++) //무조건 순섲대로 정렬되어있다는 가정하에 한다.
		{
			splitter = datas[i].name.Split('_');
			int circleIdx = int.Parse(splitter[0]);
			if(res.Count <= circleIdx)
			{
				res.Add(new List<PlayerNode>());
			}
			res[circleIdx].Add(datas[i]);
		}

		return res;
	}

	public static List<PlayerNode> LoadNodeData(int circleIdx)
	{
		List<PlayerNode> res = new List<PlayerNode>();
		List<PlayerNode> datas = Resources.LoadAll<PlayerNode>(NODEPATH).ToList();

		string[] splitter;
		for (int i = 0; i < datas.Count; i++) //무조건 순섲대로 정렬되어있다는 가정하에 한다.
		{
			splitter = datas[i].name.Split('_');
			int cIdx = int.Parse(splitter[0]);
			if(cIdx == circleIdx)
			{
				res.Add(datas[i]);
			}
		}

		return res;
	}

	public static PlayerNode LoadNodeData(int circleIdx, int orderIdx)
	{
		PlayerNode res = null;
		List<PlayerNode> datas = Resources.LoadAll<PlayerNode>(NODEPATH).ToList();

		string[] splitter;
		for (int i = 0; i < datas.Count; i++) //무조건 순섲대로 정렬되어있다는 가정하에 한다.
		{
			splitter = datas[i].name.Split('_');
			int cIdx = int.Parse(splitter[0]);
			int oIdx = int.Parse(splitter[1]);
			if (cIdx == circleIdx && oIdx == orderIdx)
			{
				res = datas[i];
				break;
			}
		}


		return res;
	}

	public static string GetName(PlayerNode inf)
	{
		switch (inf.nodeType)
		{
			case StatUpgradeType.White:
				GameManager.globalStringBuilder.Append("양 + ");
				GameManager.globalStringBuilder.Append(inf.amt);
				GameManager.globalStringBuilder.Append("의");
				break;
			case StatUpgradeType.Black:
				GameManager.globalStringBuilder.Append("음 + ");
				GameManager.globalStringBuilder.Append(inf.amt);
				GameManager.globalStringBuilder.Append("의");
				break;
			case StatUpgradeType.Atk:
				GameManager.globalStringBuilder.Append("힘 + ");
				GameManager.globalStringBuilder.Append(inf.amt);
				GameManager.globalStringBuilder.Append("의");
				break;
			case StatUpgradeType.MoveSpeed:
				GameManager.globalStringBuilder.Append("신속 + ");
				GameManager.globalStringBuilder.Append(inf.amt);
				GameManager.globalStringBuilder.Append("의");
				break;
			case StatUpgradeType.CooldownRdc:
				GameManager.globalStringBuilder.Append("순환 + ");
				GameManager.globalStringBuilder.Append(inf.amt);
				GameManager.globalStringBuilder.Append("의");
				break;
			case StatUpgradeType.Callback:
				GameManager.globalStringBuilder.Append("특수 ");
				break;
			default:
				break;
		}
		GameManager.globalStringBuilder.Append("혈");
		string res = GameManager.globalStringBuilder.ToString();
		GameManager.globalStringBuilder.Clear();
		return res;
	}

	public static string ToStringKorean(StatUpgradeType act)
	{
		switch (act)
		{
			case StatUpgradeType.White:
				return "양(하얀거)";
			case StatUpgradeType.Black:
				return "음(검은거)";
			case StatUpgradeType.Atk:
				return "공격";
			case StatUpgradeType.MoveSpeed:
				return "이동속도";
			case StatUpgradeType.CooldownRdc:
				return "쿨다운 감소";
			case StatUpgradeType.Callback:
				return "특수";
			default:
				Debug.LogWarning($"{act} 상태에 대한 한글 번역이 제공되지 않습니다.");
				return act.ToString();
		}
	}
}
