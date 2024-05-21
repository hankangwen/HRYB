using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StatUpgradeType
{
	White,
	Black,
	 WhiteAtk,
	 //WhiteAtk,
	 BlackAtk,
	 //BlackAtk,
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
		System.Text.StringBuilder sb;
		bool usingGlobal = GameManager.GetGlobalSB(out sb);
		switch (inf.nodeType)
		{
			case StatUpgradeType.White:
				sb.Append("양 + ");
				sb.Append(inf.amt);
				sb.Append("의");
				break;
			case StatUpgradeType.Black:
				sb.Append("음 + ");
				sb.Append(inf.amt);
				sb.Append("의");
				break;
			 case StatUpgradeType.WhiteAtk:
				 sb.Append("힘 + ");
				 sb.Append(inf.amt);
				 sb.Append("의");
				 break;
			 case StatUpgradeType.BlackAtk:
				 sb.Append("정신 + ");
				 sb.Append(inf.amt);
				 sb.Append("의");
				 break;
			case StatUpgradeType.MoveSpeed:
				sb.Append("신속 + ");
				sb.Append(inf.amt);
				sb.Append("의");
				break;
			case StatUpgradeType.CooldownRdc:
				sb.Append("순환 + ");
				sb.Append(inf.amt);
				sb.Append("의");
				break;
			case StatUpgradeType.Callback:
				sb.Append("특수 ");
				break;
			default:
				break;
		}
		sb.Append("혈");
		string res = sb.ToString();
		sb.Clear();
		GameManager.ReturnGlobalSB(usingGlobal);
		
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
			 case StatUpgradeType.WhiteAtk:
				 return "양 공격력";
			 case StatUpgradeType.BlackAtk:
				 return "음 공격력";
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
