using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public Crafter crafter;

	private void Start()
	{
		crafter = new Crafter();
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if(crafter.Craft(new ItemAmountPair("����")))
			{
				Debug.Log("�߸���");
			}
			else
			{
				Debug.Log("����");
			}

		}
	}

}
