using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Skills/Humen/마스터스파크")]
public class HumenMasterSpark : AttackBase
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

		GameObject obj = PoolManager.GetObject("MasterSparkProduction", self.transform);
		if(obj.TryGetComponent<SkillProduction>(out SkillProduction _pro))
		{
			_pro.Begin();
		}
	}

	public override void OnAnimationEvent(Actor self, AnimationEvent evt)
	{
		string[] tt = evt.stringParameter.Split("$");
		if (tt[0] == "Effect")
		{
			EffectObject eff = PoolManager.GetEffect("MasterSparkEff", self.transform);
			eff.Begin();
		}
		else
		{
			GameObject obj = PoolManager.GetObject("MasterSparkCols", self.transform);
			
			if (obj.TryGetComponent<ColliderCast>(out ColliderCast _cols))
			{
				_cols.Now(self.transform, (_life) =>
				{
					DoDamage(_life.GetActor(), self, _life.transform.position, _baseInfo);
				}, (sans, enemy) =>
				{
					CameraManager.instance.ShakeCamFor(0.08f, 2, 2);
				}, default, default, 0.1F);
			}
		}
	}

	public override void OnAnimationEnd(Actor self, AnimationEvent evt)
	{
	}

	public override void OnAnimationStop(Actor self, AnimationEvent evt)
	{
		GameManager.instance.EnableCtrl();
	}
}
