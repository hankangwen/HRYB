using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : LifeModule 
{
	public Transform spawnPoint;

	public override void Awake()
	{
		base.Awake();
	}

	public override void Update()
	{
		base.Update();
		if (regenOn)
		{
			GameManager.instance.uiManager.yinYangUI.RefreshValues();
		}
	}

	protected override void DamageYYBase(YinYang data)
	{
		base.DamageYYBase(data);
		GameManager.instance.uiManager.yinYangUI.RefreshValues();
	}


	public override void OnDead()
	{
		base.OnDead();
		GetActor().anim.SetDieTrigger();
		GetActor().move.moveDir = Vector3.zero;
		GetActor().move.forceDir = Vector3.zero;
		(GetActor().move as PlayerMove).ctrl.center = Vector3.up;
		(GetActor().move as PlayerMove).ctrl.height = 1;
		GetActor().Respawn();

		transform.position = spawnPoint.position;
		Debug.Log("Player dead");
	}
}
