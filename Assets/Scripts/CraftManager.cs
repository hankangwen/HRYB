using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public Crafter crafter;

	private void Awake()
	{
		crafter = new Crafter();
	}

	public void CraftWithName(string name)
	{
		if (crafter.Craft(new ItemAmountPair(name)))
		{
			Debug.Log("�߸���");
		}
		else
		{
			Debug.Log("����");
		}
	}

	public void SetCurMethod(int mtd)
	{
		crafter.CurMethod = (CraftMethod)mtd;
	}



}
