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
	public override void Awake()
	{
		base.Awake();
		_currentGrogeValue = _grogeInitValue;
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
		OutJeungGi(black, white);
		base.DamageYY(black, white, type, dur, tick, attacker, channel);
	}

	public override void DamageYY(YinYang data, DamageType type, float dur = 0, float tick = 0, Actor attacker = null, DamageChannel channel = DamageChannel.None)
	{

		OutJeungGi(data.black, data.white);
		base.DamageYY(data, type, dur, tick, attacker, channel);
	}

	public void OutJeungGi(float black, float white)
	{
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
		}
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


