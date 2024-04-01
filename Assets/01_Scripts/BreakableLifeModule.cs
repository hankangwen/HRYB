using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLifeModule : LifeModule
{
	public string destroyedEff;
	public override void OnDead()
	{
		gameObject.SetActive(false);
		PoolManager.GetObject(destroyedEff, transform.position, Quaternion.identity, 5f);
	}
}
