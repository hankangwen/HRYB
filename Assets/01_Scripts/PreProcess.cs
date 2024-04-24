using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ProcessType //손질은 사실상 제작이다.
{
    None = -1,

    
    Fry,
	Burn,
	Stir,
	Mash,

}

public class PreProcess
{
	public const string FRYPREFIX = "볶은 ";
	public const string BURNPREFIX = "태운 ";
	public const string STIRPREFIX = "데친 ";
	public const string MASHPREFIX = "빻은 ";

	protected Item info;
    public ProcessType type;
    public string prefix;

    protected PreProcess(ProcessType t, Item itemInfo)
	{
		if(itemInfo is YinyangItem yy )
		{
			info = new YinyangItem(yy);
			type = t;
			switch (t)
			{
				case ProcessType.None:
					break;
				case ProcessType.Fry:
					prefix = "작";
					break;
				case ProcessType.Burn:
					prefix = "소";
					break;
				default:
					Debug.LogWarning($"{type} 상태에 대한 단어는 정의되어있지 않습니다");
					break;
			}
		}
		else
		{
			Debug.LogError("잘못된 아이템을 조리하려 하고 있다.");
		}
	}

	public virtual ItemAmountPair EndProcess()
	{

		//StringBuilder sb = new StringBuilder();
		//sb.Append(prefix);
		//sb.Append(info.info.MyName);
		//info.info.MyName = sb.ToString();
		//sb.Clear();
		//sb.Append(prefix);
		//sb.Append((info.info as YinyangItem).nameAsChar);
		//(info.info as YinyangItem).nameAsChar = sb.ToString();

		(info as YinyangItem).processes.Add(type);

		info.InsertToTable();

		return new ItemAmountPair(info);
	}

	public override bool Equals(object obj)
	{
		return obj is PreProcess p && p.type == type;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(type);
	}

	public static ItemAmountPair ApplyProcessTo(ProcessType type, ItemAmountPair to, bool autoRemovePrev = true, bool autoAddAfter = true)
	{
		if (autoRemovePrev)
		{
			GameManager.instance.pinven.RemoveItem(to.info, 1);
		}
		PreProcess pp = new PreProcess(type, to.info);
		ItemAmountPair after = pp.EndProcess();

		if (autoAddAfter)
		{
			GameManager.instance.pinven.AddItem(after.info, 1);
		}
		return after;
	}
}

//public class Burn : PreProcess
//{
//	public Burn() : base(ProcessType.Roast, ItemAmountPair.Empty)
//	{

//	}

//	public Burn(ItemAmountPair itemInfo) : base(ProcessType.Burn, itemInfo)
//	{

//	}

//	public override bool Equals(object obj)
//	{
//		Debug.Log("!!");
//		return obj is Burn r && r.info == info;
//	}

//	public override int GetHashCode()
//	{
//		return base.GetHashCode();
//	}
//}

//public class Roast : PreProcess
//{

//	public Roast() : base(ProcessType.Roast, ItemAmountPair.Empty)
//	{

//	}

//	public Roast(ItemAmountPair itemInfo) : base(ProcessType.Roast, itemInfo)
//	{

//	}

//	public override bool Equals(object obj)
//	{
//		Debug.Log("!!");
//		return obj is Roast r && r.info == info;
//	}

//	public override int GetHashCode()
//	{
//		return base.GetHashCode();
//	}
//}