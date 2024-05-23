using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKey : MonoBehaviour
{
    
    void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			GameManager.instance.pinven.AddItem(Item.GetItem <YinyangItem> ("고사리"), 1);
			GameManager.instance.pinven.AddItem(Item.GetItem <YinyangItem> ("녹각"), 1);
		}
	}
}
