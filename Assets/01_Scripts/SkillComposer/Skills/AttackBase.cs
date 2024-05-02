using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gpm.Ui.MultiLayout;


[System.Serializable]
public class DamageSettings
{
	[Header("SkillDmg")]
	public float black = 1;
	public float white = 1;
	[Header("GrogeDmg")]
	public float _grogeDamage = 0;
	[Header("ComboDmg")]
	public float _comboDamage = 0;


}

public abstract class AttackBase : Leaf
{
	public string relateTrmName;

	public void OnValidate()
	{
		for (int i = _dmgs.Count; i < ListValue(); i++)
		{
			_dmgs.Add(new DamageSettings());
		}

		for (int i = _dmgs.Count -1; i >= ListValue(); i--)
		{
			_dmgs.RemoveAt(i);
		}

	}

	public abstract int ListValue();


	protected Transform relatedTransform;
	[Header("SkillDamage")]
	public List<DamageSettings> _dmgs = new();

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



	protected virtual void DoDamage(Actor to, Actor by, DamageSettings value, Vector3 vec = new Vector3())
	{
		Vector3 pos = to.transform.position;

		if (vec != new Vector3())
		{
			pos = vec;
		}


		float white = by.atk.Damage.white * value.white;
		float black = by.atk.Damage.black * value.black;

		Debug.LogError($"DMGS : {value.white} | {value.black}");

		if (white > 0)
		{
			to.life.DamageYY(0, white, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, white, YYInfo.White);
		}
		if (black > 0 )
		{
			to.life.DamageYY(black, 0, DamageType.DirectHit, 0, 0, by);
			GameManager.instance.shower.GenerateDamageText(pos, black, YYInfo.Black);
		}

		if (to.TryGetComponent<EnemyLifeModule>(out EnemyLifeModule eme))
		{
			eme.DoGrogeDamage(value._grogeDamage);
		}


		for (int i = 0; i < statEff.Count; i++)
		{
			StatusEffects.ApplyStat(to, by, statEff[i].id, statEff[i].duration, statEff[i].power);
		}

		statEff = originalEff;
	}

}
