using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxColliderCast))]
public class YGArrow : MonoBehaviour
{
	float _lifeTime;
	Action<LifeModule> _Act = null;
	Action<Transform, LifeModule> _Act2 = null;
	Action<Vector3, Vector3, float> tls = null;
	ColliderCast _cols = null;
	Vector3 _pos;
	Actor _owner;
	Transform _target;

	bool _isFire = false;

	float _currentTime = 0;

	private void OnEnable()
	{
		_cols = gameObject.GetComponent<ColliderCast>();
		_isFire = false;
	}

	public void Ready(Actor owner, Vector3 pos, Action<Vector3, Vector3, float> moveAction = null, Action<LifeModule> act = null, Action<Transform, LifeModule> act2 = null, float duration = 3f)
	{
		_pos = pos;
		transform.position = _pos;
		_Act  = act;
		_Act2 = act2;
		_lifeTime = duration;
		tls = moveAction;
		_owner = owner;
	}

	public void Fire()
	{
		_cols.Now(transform ,_Act, _Act2);
		_pos = transform.position;
		transform.parent = null;
		_isFire = true;
		_currentTime = 0;
		if(_owner.atk.target != null)
		{
			SetTarget(_owner.atk.target.transform);

		}
	}


	public void SetTarget(Transform targ)
	{
		if (targ)
		{
			_target = targ.Find("Middle");
			if (!_target)
			{
				_target = targ;
			}
		}

	}

	public void Update()
	{
		if(_isFire)
		{
			_currentTime += Time.deltaTime;
			if(tls != null && _owner.atk.target != null)
			{
				tls.Invoke(_pos, _target.position, _currentTime);
			}
			else
			{
				transform.position += _owner.transform.forward.normalized * 8 * Time.deltaTime;
				//tls.Invoke(_pos, , _currentTime);
			}
		}
	}
}
