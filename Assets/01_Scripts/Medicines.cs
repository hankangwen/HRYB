using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MedicineType
{
	None = -1,

	Liquid,
	Ointment,
	Pill,
}

public enum ChangableStatus
{
	Black,
	BlackMax,
	White,
	WhiteMax,
	BlackAtk,
	WhiteAtk,

}

public enum StatusChangeMode
{
	Fixed,
	Percentage,

}

public class Medicines : YinyangItem
{
	public const float SOLID = 0.1f;
	public const float OINTMENT = 0.4f;
	public const float LIQUID = 1f;

	public MedicineType type;

	public List<ItemAmountPair> recipe;

	//public override float ApplySpeed
	//{
	//	get 
	//	{
	//		switch (type)
	//		{
	//			case MedicineType.Liquid:
	//				return applySpeed * 2f;
	//				break;
	//			case MedicineType.Powder:
	//				return applySpeed * 1f;
	//				break;
	//			case MedicineType.Pill:
	//				return applySpeed * 0.8f;
	//				break;
	//			default:
	//				return applySpeed;
	//				break;
	//		}
	//	}
	//} //식 필요

    public Medicines(string name, string desc, ItemType iType, int max, Specials used, bool isNewItem, int price, DetailAmount det, string ch = "")
		:base(name, desc, iType, max, used, isNewItem, price, det, true, "약")
	{
		if(detailParams[DetailParameter.Moist] <= SOLID)
		{
			type = MedicineType.Pill;
		}
		else if (detailParams[DetailParameter.Moist] <= OINTMENT)
		{
			type = MedicineType.Ointment;
		}
		else
		{
			type = MedicineType.Liquid;
		}
		switch (type)
		{
			case MedicineType.Liquid:
				icon =ImageManager.MedicineSprite;
				break;
			case MedicineType.Ointment:
				break;
			case MedicineType.Pill:
				break;
		}

	}


	public override void Use()
	{
		//GameManager.instance.pinven.RemoveItem(this);
		base.Use();


	}
}
