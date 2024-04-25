using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class YinyangItem : Item
{
	//YinYang data;
	//public YinYang yy
	//{
	//	get
	//	{
	//		return data;
	//	}
	//	set
	//	{
	//		data = value;
	//	}
	//}

	public HashSet<ProcessType> processes = new HashSet<ProcessType>();

	public override string MyName
	{
		get
		{
			StringBuilder sb = GameManager.globalStringBuilder;
			bool usingGlobal = true;
			if (GameManager.globalStringBuilderUsing)
			{
				sb = new StringBuilder();
				usingGlobal = false;
			}
			GameManager.globalStringBuilderUsing = true;

			if (processes.Contains(ProcessType.Stir))
			{
				sb.Append(PreProcess.STIRPREFIX);
			}
			if (processes.Contains(ProcessType.Fry))
			{
				sb.Append(PreProcess.FRYPREFIX);
			}
			if (processes.Contains(ProcessType.Burn))
			{
				sb.Append(PreProcess.BURNPREFIX);
			}
			if (processes.Contains(ProcessType.Mash))
			{
				sb.Append(PreProcess.MASHPREFIX);
			}
			sb.Append(base.MyName);
			string res = sb.ToString();
			sb.Clear();
			if(usingGlobal)
				GameManager.globalStringBuilderUsing = false;
			return res;
		} 
		set => base.MyName = value;
	}

	//public float initDec;
	//public float decPerSec;

	public string nameAsChar;

	//public float applySpeed = 1f;
	
	//public virtual float ApplySpeed 
	//{
	//	get
	//	{
	//		return applySpeed;
	//	}
	//}

	public YinyangItem(YinyangItem item) : base(item)
	{
		//data = item.data;
		processes = new HashSet<ProcessType>(item.processes);
		if (item.nameAsChar == "")
		{
			nameAsChar = MyName[UnityEngine.Random.Range(0, MyName.Length)].ToString();
		}
		else
		{
			nameAsChar = item.nameAsChar;
		}
	}

    public YinyangItem(string name, string desc, ItemType iType, int max, Specials used, bool isNewItem, YinYang yyData, string ch = "") : base(name, desc, iType, max, used, isNewItem)
	{
		//data = yyData;
		if(ch == "")
		{
			nameAsChar = MyName[UnityEngine.Random.Range(0, MyName.Length)].ToString();
		}
		else
		{
			nameAsChar = ch;
		}
	}

	public override void Use()
	{
		//GameManager.instance.pActor.life.DamageYY(yy, DamageType.Continuous, ApplySpeed);
		GameManager.instance.pedia.UseItem(this);
		base.Use();
	}

	public override int GetHashCode()
	{
		return MyName.GetHashCode();
	}

	public override int CompareTo(object obj)
	{
		if (obj is YinyangItem item)
		{
			if (originalName.CompareTo(item.originalName) == 0)
			{
				if(item.processes.Count == processes.Count)
				{
					return MyName.CompareTo(item.MyName);
				}
				else
					return processes.Count > item.processes.Count ? 1 : -1;
			}
			else
				return originalName.CompareTo(item.originalName);
		}
		return base.CompareTo(obj);
	}
}
