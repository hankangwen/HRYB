using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Yoho/날카로운연격")]
public class YohoSharpnessSkill : AttackBase
{
	ColliderCast _cols = null;
	
	internal override void MyOperation(Actor self)
	{

	}

	internal override void MyDisoperation(Actor self)
	{

	}

	public override void UpdateStatus()
	{
	    
	}

	public override void OnAnimationStart(Actor self, AnimationEvent evt)
	{
		GameManager.instance.DisableCtrl(false);
	}

	public override void OnAnimationEvent(Actor self, AnimationEvent evt)
	{

		

		if (_cols != null)
		{
			_cols.End();
			_cols = null;
		}


		string[] tt = evt.stringParameter.Split("$");
		EffectObject eff=null;
		switch (tt[0])
		{
			case "1":
				{
					GameObject obj1 = PoolManager.GetObject("YusungSmithleft", self.transform);
					if (obj1.TryGetComponent<EffectObject>(out eff))
					{
						eff.Begin();
						self.StartCoroutine(DeleteObj(obj1));
					}

				}
				break;

			case "2":
				{
					GameObject obj2 = PoolManager.GetObject("YusungSmithright", self.transform);
					if (obj2.TryGetComponent<EffectObject>(out eff))
					{
						eff.Begin();
						self.StartCoroutine(DeleteObj(obj2));
					}
				}
				break;
			case "3":
				{
					GameObject obj1 = PoolManager.GetObject("YusungSmithleft", self.transform);
					if (obj1.TryGetComponent<EffectObject>(out eff))
					{
						eff.Begin();
						self.StartCoroutine(DeleteObj(obj1));
					}
					
				}
				break;

			case "4":
				{
					GameObject obj2 = PoolManager.GetObject("YusungSmithright", self.transform);
					if (obj2.TryGetComponent<EffectObject>(out eff))
					{
						eff.Begin();
						self.StartCoroutine(DeleteObj(obj2));
					}
					//GameManager.instance.audioPlayer.PlayPoint("CrawLow", self.transform.position);
				}
				break;
			default:
				{
					GameObject obj2 = PoolManager.GetObject("YusungSmithright", self.transform);
					if (obj2.TryGetComponent<EffectObject>(out eff))
					{
						eff.Begin();
						self.StartCoroutine(DeleteObj(obj2));
					}
					//GameManager.instance.audioPlayer.PlayPoint("CrawLow", self.transform.position);
				}
				break;
			case "Sound":
				{
					GameManager.instance.audioPlayer.PlayPoint("HitSound", self.transform.position);
				}
				break;
		}
		
		
		GameObject obj = PoolManager.GetObject("SkyBritghCollider", self.transform);
		if (obj.TryGetComponent<ColliderCast>(out _cols))
		{
			_cols.Now(self.transform, (_life) =>
			{
				CameraManager.instance.ShakeCamFor(0.08f, 2, 2);
				StatusEffects.ApplyStat(_life.GetActor(), self, StatEffID.Bleeding, 4f);
				DoDamage(_life.GetActor(), self, _dmgs[0], obj.transform.position);
				_life.GetActor().move.forceDir = self.transform.forward * 0.4f + new Vector3(0,0.5f,0);
			},
			(sans, enemy)=>
			{

				GameManager.instance.TimeManager.TimeSlow(self, enemy.GetActor(), eff);
				self.move.forceDir = self.transform.forward * 0.4f + new Vector3(0,0.5f,0);
			});
		}
		
	}

	IEnumerator DeleteObj(GameObject obj, float t = 1.0f)
	{
		yield return new WaitForSeconds(t);
		PoolManager.ReturnObject(obj);
	}
	
	public override void OnAnimationEnd(Actor self, AnimationEvent evt)
	{
		if (_cols != null)
		{
			_cols.End();
			//_cols = null;
		}
	}
	
	public override void OnAnimationStop(Actor self, AnimationEvent evt)
	{
		GameManager.instance.EnableCtrl();
	}

	public override int ListValue()
	{
		return 1;
	}
}