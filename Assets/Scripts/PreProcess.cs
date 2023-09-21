using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProcessType
{
    None = -1,

    Trim,
    Grind,
    Roast,
    Pickle,
    Age,
    Steam,
}

public class PreProcess
{
    public ProcessType type;
    public int additionalInfo;
    public string prefix;

    public PreProcess(ProcessType t, int info = -1)
	{
        type = t;
        additionalInfo = info;
		switch (t)
		{
			case ProcessType.None:
				break;
			case ProcessType.Trim:
				prefix = "��";
				break;
			case ProcessType.Grind:
				prefix = "��";
				break;
			case ProcessType.Roast:
				int r = Random.Range(0, 2);
				if(r == 0)
				{
					prefix = "��";
				}
				else
				{
					prefix = "��";
				}
				break;
			case ProcessType.Pickle:
				prefix = $"{(Item.nameHashT[additionalInfo] as YinyangItem).nameAsChar}ħ";
				break;
			case ProcessType.Age:
				prefix = "��";
				break;
			case ProcessType.Steam:
				prefix = $"{(Item.nameHashT[additionalInfo] as YinyangItem).nameAsChar}��";
				break;
		}
	}
}
