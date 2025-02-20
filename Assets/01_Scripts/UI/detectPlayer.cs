using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class detectPlayer : MonoBehaviour
{
	GameObject obj;
	public LayerMask layer;
	public string bossName;

	private LifeModule lf;

	private void Awake()
	{
		lf = GetComponent<LifeModule>();
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 7)
		{
			obj = GameManager.instance.bHPManager.makeHP(bossName, lf);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 7)
		{
			if(obj.CompareTag("Jansung"))
			{
				GameManager.instance.bHPManager.jangsungHP = false;
			}
			Destroy(obj);
		}	 
	}
}
