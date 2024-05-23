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
[RequireComponent(typeof(WalletModule))]
public class Actor : MonoBehaviour
{
	public AttackModule atk;
	public MoveModule move;
	public LifeModule life;
	public SightModule sight;
	public CastModule cast;
	public AnimModule anim;
	public TalkModule talk;
	public WalletModule wallet;

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
		wallet = GetComponent<WalletModule>();
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
		wallet.ResetStatus();
	}

	public void AddStat(float amt, StatUpgradeType type)
	{
		switch (type)
		{
			case StatUpgradeType.White:
				life.yy.white.AddMod(amt);
				break;
			case StatUpgradeType.Black:
				life.yy.black.AddMod(amt);
				break;
			case StatUpgradeType.WhiteAtk:
				atk.Damage.black.AddMod(amt);
				break;
			case StatUpgradeType.BlackAtk:
				atk.Damage.black.AddMod(amt);
				break;
			case StatUpgradeType.MoveSpeed:
				move.runSpeed.AddMod( amt);
				move.walkSpeed.AddMod( amt);
				move.crouchSpeed.AddMod( amt);
				break;
			case StatUpgradeType.CooldownRdc: //없다고하빈다.
				//cast.cooldownModuleStat.HandleSpeed(-amt, ModuleController.SpeedMode.Slow);
				break;
			case StatUpgradeType.Callback:
				break;
			default:
				break;
		}
	}

	public void MultStat(float amt, StatUpgradeType type)
	{
		switch (type)
		{
			case StatUpgradeType.White:
				life.yy.white.MultMod(amt);
				break;
			case StatUpgradeType.Black:
				life.yy.black.MultMod(amt);
				break;
			case StatUpgradeType.WhiteAtk:
				atk.Damage.black.MultMod(amt);
				break;
			case StatUpgradeType.BlackAtk:
				atk.Damage.black.MultMod(amt);
				break;
			case StatUpgradeType.MoveSpeed:
				move.runSpeed.MultMod(amt);
				move.walkSpeed.MultMod(amt);
				move.crouchSpeed.MultMod(amt);
				break;
			case StatUpgradeType.CooldownRdc: //??????
				cast.cooldownModuleStat.HandleSpeed(-amt, ModuleController.SpeedMode.Slow);
				break;
			case StatUpgradeType.Callback:
				break;
			default:
				break;
		}
	}

	public void HandleStatus(StatUpgradeType type, float amtAdd, float amtMult, float timePeriod)
	{
		if(timePeriod == Mathf.Infinity || timePeriod < 0)
		{
			AddStat(amtAdd, type);
		}
		else
		{

		}
	}
}

public class UpgradableStatus
{
	float maxValue;
	float modValueAdd;
	float modValueMult;

	float value;

	int roundThreshold;

	public float Value
	{
		get =>value;
		set
		{
			this.value = value;
			this.value = Mathf.Clamp(this.value, 0, MaxValue);
		}
	}
	public float MaxValue
	{
		get => Mathf.Round(((maxValue + modValueAdd) * (1 + modValueMult)) * Mathf.Pow(10, roundThreshold)) / Mathf.Pow(10, roundThreshold);
	}

	public void AddMod(float amt)
	{
		modValueAdd += amt;

		value = Mathf.Clamp(value, 0, MaxValue);
	}

	public void MultMod(float amt)
	{
		modValueMult += amt;

		value = Mathf.Clamp(value, 0, MaxValue);
	}

	public void ResetCompletely()
	{
		modValueAdd = 0;
		modValueMult = 0;
		value = MaxValue;

	}

	public UpgradableStatus(int roundFrom, float initValue)
	{
		maxValue = initValue;
		modValueAdd= 0;
		modValueMult = 0;

		value = MaxValue;

		roundThreshold = roundFrom;
	}

	public UpgradableStatus(UpgradableStatus origin)
	{
		value = origin.value;
		modValueAdd = origin.modValueAdd;
		modValueMult = origin.modValueMult;
		maxValue = origin.maxValue;
		roundThreshold = origin.roundThreshold;
	}
}
