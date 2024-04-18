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
}
