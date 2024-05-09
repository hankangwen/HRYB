using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(AttackModule))]
[RequireComponent(typeof(MoveModule))]
[RequireComponent(typeof(LifeModule))]
[RequireComponent(typeof(SightModule))]
[RequireComponent(typeof(CastModule))]
[RequireComponent(typeof(AnimModule))]
public class Actor : MonoBehaviour
{
	public AttackModule atk;
	public MoveModule move;
	public LifeModule life;
	public SightModule sight;
	public CastModule cast;
	public AnimModule anim;
	public TalkModule talk;

	public Action<Actor> updateActs;
	public AISetter _ai;

	public AISetter AI
	{
		get
		{
			if(_ai == null && TryGetComponent(out AISetter value))
			{
				_ai = value;
			}
			else if (_ai == null)
			{
				return null;	
			}

			return _ai;
		}
	}

	private void Awake()
	{
		atk = GetComponent<AttackModule>();
		move = GetComponent<MoveModule>();
		life = GetComponent<LifeModule>();
		sight = GetComponent<SightModule>();
		cast = GetComponent<CastModule>();
		anim = GetComponent<AnimModule>();

		talk = GetComponent<TalkModule>();
	}
	void Start()
	{
		Respawn();
	}

	private void Update()
	{
		updateActs?.Invoke(this);
	}

	public virtual void Respawn()
	{
		_ai?.ResetStatus();
		atk.ResetStatus();
		move.ResetStatus();
		life.ResetStatus();
		sight.ResetStatus();
		cast.ResetStatus();
		anim.ResetStatus();
		talk?.ResetStatus();
	}

	public void AddStat(float amt, StatUpgradeType type)
	{
		switch (type)
		{
			case StatUpgradeType.White:
				life.initYinYang.white += amt;
				life.yy.white += amt;
				break;
			case StatUpgradeType.Black:
				life.initYinYang.black += amt;
				life.yy.black += amt;
				break;
			case StatUpgradeType.WhiteAtk:
				atk.initDamage.white += amt;
				atk.Damage.white += amt;
				break;
			case StatUpgradeType.BlackAtk:
				atk.initDamage.black += amt;
				atk.Damage.black += amt;
				break;
			case StatUpgradeType.MoveSpeed:
				move.moveModuleStat.HandleSpeed(-amt, ModuleController.SpeedMode.Slow); //공식이 있나?
				break;
			case StatUpgradeType.CooldownRdc:
				cast.cooldownModuleStat.HandleSpeed(amt, ModuleController.SpeedMode.Slow);
				break;
			case StatUpgradeType.Callback:
				break;
			default:
				break;
		}
	}
}
