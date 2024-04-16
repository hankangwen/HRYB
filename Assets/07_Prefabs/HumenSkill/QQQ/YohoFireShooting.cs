using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YohoFireShooting : AttackBase
{
	public override void UpdateStatus()
	{

	}

	internal override void MyDisoperation(Actor self)
	{

	}

	internal override void MyOperation(Actor self)
	{

	}

	public override void OnAnimationStart(Actor self, AnimationEvent evt)
	{
		GameManager.instance.DisableCtrl(false);
	}

}
