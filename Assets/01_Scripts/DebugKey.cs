using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKey : MonoBehaviour
{
    
    void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			GameManager.instance.pinven.AddItem(Item.GetItem("인삼"), 1);
			GameManager.instance.pinven.AddItem(Item.GetItem("밧줄"), 1);
			GameManager.instance.pinven.AddItem(Item.GetItem("녹각"), 1);
		}
	}
}
