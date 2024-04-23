using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gpm.Ui.MultiLayout;

public abstract class AttackBase : Leaf
{
	public float damageMult;
	public string relateTrmName;
	

	protected Transform relatedTransform;

	[Header("YYInfo")]
	public YYInfo _baseInfo = YYInfo.None;

	List<StatusEffectApplyData> originalEff = new List<StatusEffectApplyData>();
	protected virtual void Awake()
	{
		relatedTransform = GameObject.Find(relateTrmName)?.transform;
		originalEff = new List<StatusEffectApplyData>(statEff);
		if (relatedTransform == null)
		{
			Debug.Log($"NO TRANSFORM FOUND IN SUCH NAME : {relateTrmName}");
		}
		else
		{
			Debug.Log($"NO PROBLEM WITH {name}");
		}
	}

	public override void Operate(Actor self) // 실행시
	{

	}


	protected virtual void DoDamage(Actor to, Actor by, Vector3 vec = new Vector3(), YYInfo info = YYInfo.None)
	{
		Vector3 pos = to.transform.position;
		
		if (vec != new Vector3())
		{
			pos = vec;
		}
		
		float white = by.atk.Damage.white * damageMult;
		float black = by.atk.Damage.black * damageMult;
		

		//Debug.Log($"[데미지] {to.gameObject.name} 에게 데미지 : {by.atk.initDamage} * {damageMult} = {(by.atk.initDamage * damageMult)}");
		
		
		if ((by.atk.Damage * damageMult).white > 0 && (info == YYInfo.None || info == YYInfo.White))
		{
			to.life.DamageYY(0, white, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, white, YYInfo.White);
		}
		if ((by.atk.Damage * damageMult).black > 0&& (info == YYInfo.None || info == YYInfo.Black))
		{
			to.life.DamageYY(black, 0, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, black, YYInfo.Black);
		}

		for (int i = 0; i < statEff.Count; i++)
		{
			StatusEffects.ApplyStat(to, by, statEff[i].id, statEff[i].duration, statEff[i].power);
		}

		statEff = new List<StatusEffectApplyData>( originalEff);
	}
	
	
	protected virtual void DoDamage(Actor to, Actor by, float value , Vector3 vec = new Vector3(), YYInfo info = YYInfo.None)
	{
		Vector3 pos = to.transform.position;
		
		if (vec != new Vector3())
		{
			pos = vec;
		}
		
		if (value == 0f)
			value = 1;

		float white = by.atk.Damage.white * damageMult;
		float black = by.atk.Damage.black * damageMult;

		if ((by.atk.Damage * damageMult).white > 0 && (info == YYInfo.None || info == YYInfo.White))
		{
			to.life.DamageYY(0, white, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, white, YYInfo.White);
		}
		if ((by.atk.Damage * damageMult).black > 0 && (info == YYInfo.None || info == YYInfo.Black))
		{
			to.life.DamageYY(black, 0, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, black, YYInfo.Black);
		}


		for (int i = 0; i < statEff.Count; i++)
		{
			StatusEffects.ApplyStat(to, by, statEff[i].id, statEff[i].duration, statEff[i].power);
		}

		statEff = originalEff;
	}
	
}
