using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Human/벼락내리기")]
public class HumanLighting : AttackBase
{

	public override int ListValue()
	{
		return 2;
	}

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

	public override void OnAnimationEvent(Actor self, AnimationEvent evt)
	{
		GameObject obj = PoolManager.GetObject("HumanLightingAttack", self.transform);
		if (obj.TryGetComponent<ColliderCast>(out ColliderCast _cols))
		{
			_cols.Now(self.transform, (_life) =>
			{

				GameObject obj2 = PoolManager.GetObject("YohoNormalAttack", _life.transform);
				if (obj2.TryGetComponent<ColliderCast>(out ColliderCast _cols2))
				{
					_cols2.Now(self.transform, (_life) =>
					{

						DoDamage(_life.GetActor(), self, _dmgs[0], _life.transform.position);
					}, 
					(tls, module) => 
					{
						DoDamage(module.GetActor(), self, _dmgs[1], module.transform.position);
						CameraManager.instance.ShakeCamFor(0.08f, 2, 2);
						
							
						GameObject eff = PoolManager.GetObject("LightingEffectHuman", _life.transform);
						if (eff.TryGetComponent<EffectObject>(out EffectObject eff2))
						{
							eff2.Begin();
						}


					}, default, default, 0.4f);
				}
			}, null, default, default, 1f);
		}
	}

	public override void OnAnimationStop(Actor self, AnimationEvent evt)
	{
		GameManager.instance.EnableCtrl();
	}


}
