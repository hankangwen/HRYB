using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeModule : LifeModule
{
	[Header("Enemy")]
	public bool _isItGroge = false;
	public float _grogeInitValue = 100;
	public float _timePerGrogeAdd = 50;
	float _currentGrogeValue;
	public float GetGrogeValue => _currentGrogeValue;
	bool _isGroge;
	public bool IsGroge => _isGroge;
	[Header("JunGI")]
	[SerializeField] bool _66PercentBlack = false;
	[SerializeField] bool _66PercentWhite = false;
	[SerializeField] bool _33PercentBlack = false;
	[SerializeField] bool _33PercentWhite = false;

	public override void Awake()
	{
		base.Awake();
		_currentGrogeValue = _grogeInitValue;

		_dieEvent += OutJeungGi;
	}
	public override void Update()
	{
		base.Update();

		if (_isItGroge && _isGroge)
		{
			_currentGrogeValue += _timePerGrogeAdd * Time.deltaTime;
			if(_currentGrogeValue >= _grogeInitValue)
			{
				_currentGrogeValue = _grogeInitValue;
				_isGroge = false;
			}
		}


	}
	
	public void DoGrogeDamage(float value)
	{
		if (_isItGroge || _isGroge)
			return;

		_currentGrogeValue -= value;
		if (_currentGrogeValue <= 0)
		{
			_isGroge = true;
			_currentGrogeValue = 0;
		}
	}

	public override void DamageYY(float black, float white, DamageType type, float dur = 0, float tick = 0, Actor attacker = null, DamageChannel channel = DamageChannel.None)
	{
		base.DamageYY(black, white, type, dur, tick, attacker, channel);
		OutJeungGi();
	}

	public override void DamageYY(YinYang data, DamageType type, float dur = 0, float tick = 0, Actor attacker = null, DamageChannel channel = DamageChannel.None)
	{

		base.DamageYY(data, type, dur, tick, attacker, channel);
		OutJeungGi();
	}

	public void OutJeungGi()
	{
		if(initYinYang.white * 0.66f > yy.white && _66PercentWhite ==false)
		{
			_66PercentWhite = true;
			OutValue(initYinYang.white * 0.2f);
		}
		if (initYinYang.white * 0.33f > yy.white && _33PercentWhite == false)
		{
			_33PercentWhite = true;
			OutValue(initYinYang.white * 0.2f);
		}
		if(yy.white <= 0)
		{
			OutValue(initYinYang.white * 0.2f);
		}

		if (initYinYang.black * 0.66f > yy.black && _66PercentBlack == false)
		{
			_66PercentBlack = true;
			OutValue(initYinYang.black * 0.2f);
		}
		if (initYinYang.black * 0.33f > yy.black && _33PercentBlack == false)
		{
			_33PercentBlack = true;
			OutValue(initYinYang.black * 0.2f);
		}

		if (yy.black <= 0)
		{
			OutValue(initYinYang.black * 0.2f);
		}

		/*
		if (black > 0)
		{
			if (yy.black - black > 0)
			{
				OutValue(black / 10);
			}
			else if (yy.black - black <= 0)
			{
				OutValue(yy.black / 10);
			}
		}
		if (white > 0)
		{
			if (yy.white - white > 0)
			{
				OutValue(white / 10);
			}
			else if (yy.white - white <= 0)
			{
				OutValue(yy.white / 10);
			}
		}*/
	}

	void OutValue(float t)
	{
		float x = Random.Range(-2.0f, 2.0f);
		float y = Random.Range(0.4f, 0.6f);
		float z = Random.Range(-2.0f, 2.0f);
		Vector3 vec = transform.position + new Vector3(x, y, z);
		GameObject obj = PoolManager.GetObject("JunGI", vec, transform.rotation);
		obj.transform.parent = null;

		if(t < 0)
		{
			t *= -1;
		}

		if (obj.TryGetComponent<ColliderCast>(out ColliderCast cols))
		{
			cols.Now(transform, (_life) =>
			{
				if (_life.TryGetComponent<PlayerLife>(out PlayerLife pl))
				{
					pl.yy.black += t;
					cols.End();
				}
			});
		}
	}
}


