using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WXInfo
{
	None,
    Wood,
    Fire,
    Earth,
    Metal,
    Water,

    Max
}

public enum YYInfo
{
	None,
    Black,
    White,

    Max
}
[System.Serializable]
public class YinYang
{
    public UpgradableStatus black;
    public UpgradableStatus white;

    public YinYang(float blk, float wht)
	{
		//Debug.Log(blk + " : " + wht);
		black= new UpgradableStatus(2, blk);
		white = new UpgradableStatus(2, wht);
	}

	public YinYang(float v)
	{
		black = new UpgradableStatus(2,v);
		white = new UpgradableStatus(2, v);
	}

	public YinYang(YinYang origin)
	{
		black = new UpgradableStatus( origin.black);
		white = new UpgradableStatus( origin.white);
	}

	//public float GetBalanceRatio()
	//{
	//	return yinAmt > yangAmt ? yangAmt / yinAmt : yinAmt / yangAmt;
	//}

    public float this[int i]
	{
		get
		{
			switch (i)
			{
                case ((int)YYInfo.Black):
                    return black.Value;
                case ((int)YYInfo.White):
                    return white.Value;
                default:
                    return -1;
			}
		}
		set
		{
            switch (i)
            {
                case ((int)YYInfo.Black):
                    black.Value = value;
                    break;
                case ((int)YYInfo.White):
                    white.Value = value;
                    break;
                default:
                    break;
            }
        }
	}

	public override string ToString()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		sb.Append('(');
		sb.Append(black.Value);
		sb.Append('/');
		sb.Append(black.MaxValue);
		sb.Append(':');
		sb.Append(white.Value);
		sb.Append('/');
		sb.Append(white.MaxValue);
		sb.Append(')');
		return sb.ToString();
	}

	public static YinYang operator+(YinYang a, YinYang b)
	{
		return new YinYang(a.black.Value + b.black.Value, a.white.Value + b.white.Value);
	}

	public static YinYang operator -(YinYang a, YinYang b)
	{
		return new YinYang(a.black.Value - b.black.Value, a.white.Value - b.white.Value);
	}


	public static YinYang operator *(YinYang a, float b)
	{
		return new YinYang(a.black.Value * b, a.white.Value * b);
	}

	public static YinYang operator /(YinYang a, float b)
	{
		return new YinYang(a.black.Value / b, a.white.Value / b);
	}

	public static YinYang Zero
	{
		get=> new YinYang(0,  0);
	}
	public static YinYang One
	{
		get => new YinYang(1, 1);
	}
}
