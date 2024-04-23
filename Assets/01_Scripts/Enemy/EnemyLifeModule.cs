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
}
