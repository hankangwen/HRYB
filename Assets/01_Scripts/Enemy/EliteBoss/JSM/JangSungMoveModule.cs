using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JangSungMoveModule : MoveModule
{

	Actor _target;
	UnityEngine.AI.NavMeshAgent agent;

	[Header("FIND")][SerializeField] private float _findPlayerRange = 30;

	[Header("Stat")]
	[SerializeField] private float _normalSpeed = 3.5f;
	[SerializeField] private float _fallDownMoveSpeed = 9f;

	private bool _isMove = false;
	private bool _moveDecalOnShot = false;
	public override float Speed { get => base.Speed; set { base.Speed = value; agent.speed = base.Speed; } }

	public override void Awake()
	{
		base.Awake();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.speed = Speed;
	}

	public override void Move()
	{


	}

	public void SetTarget(Actor _target)
	{
		this._target = _target;
	}

	public void PowerUp()
	{
		_normalSpeed *= 2;
		_fallDownMoveSpeed *= 2;
	}

	public float FindPlayer()
	{
		return _findPlayerRange;
	}

	public override void FixedUpdate()
	{

	}
	private void Update()
	{
		if (_isMove)
		{

			if (_target != null)
			{
				UnityEngine.AI.NavMesh.SamplePosition(_target.transform.position, out UnityEngine.AI.NavMeshHit hit, 15f, UnityEngine.AI.NavMesh.AllAreas);
				if (agent.enabled)
				{
					agent.speed = _fallDownMoveSpeed;
					agent.SetDestination(hit.position);
				}

				//Debug.LogError(hit.position);
				//			Debug.LogWarning(_target.transform.position);
				//GetActor().anim.SetMoveState();
			}
		}
	}

	//private GameObject decal;

	public void FallDownAttack()
	{
		_isMove = true;
		_moveDecalOnShot = true;
		if (_moveDecalOnShot)
		{
			_moveDecalOnShot = false;
			GameObject obj = PoolManager.GetObject("MiddleBoxDecal", transform);
			//Debug.LogError("1111");

			if (obj.TryGetComponent<BoxDecal>(out BoxDecal box))
			{
				box.SetUpDecal(transform, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
				box.StartDecal(2f);
			}
		}

		//		Debug.LogWarning("AAAAAAZAAAAAA");

		//Debug.LogError("실행됨11");
	}


	public void NormalMoveAttack()
	{
		if (_target != null)
		{
			UnityEngine.AI.NavMeshHit hit;

			Vector3 vec = (_target.transform.position - transform.position);
			vec.y = 0;


			vec = vec.normalized * 8 + transform.position;

			if (Vector3.Distance(vec, _target.transform.position) < 5)
			{

				UnityEngine.AI.NavMesh.SamplePosition(_target.transform.position, out hit, 8f, UnityEngine.AI.NavMesh.AllAreas);
				agent.speed = _normalSpeed;
				agent.SetDestination(hit.position);
			}
			else
			{
				UnityEngine.AI.NavMesh.SamplePosition(vec, out hit, 8, UnityEngine.AI.NavMesh.AllAreas);
				agent.speed = _normalSpeed;
				agent.SetDestination(hit.position);

			}

			GameObject obj = PoolManager.GetObject("MiddleBoxDecal", transform);

			if (obj.TryGetComponent<BoxDecal>(out BoxDecal box))
			{
				box.SetUpDecal(new Vector3(0, 0, 7), transform.rotation, new Vector3(0.55f, 0.55f, 0.55f), Vector3.zero, Vector3.one);
				box.StartDecal(1f);
			}
			else
			{
				Debug.LogError("프리팹 없음?");
			}

			//Debug.LogError("실행됨22");




			//GetActor().anim.SetMoveState();
		}
	}

	public void ResetDest()
	{
		_isMove = false;

		agent.speed = 0;
		agent.velocity = new Vector3(0, 0, 0);
		UnityEngine.AI.NavMesh.SamplePosition(transform.position, out UnityEngine.AI.NavMeshHit hit, 15f, UnityEngine.AI.NavMesh.AllAreas);
		agent.SetDestination(hit.position);

	}
}
