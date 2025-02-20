using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moose_normalAttackModule : EnemyAttackModule
{
	
	public override void SetAttackRange(int idx)
	{

	}

	public override void ResetAttackRange(int idx)
	{

	}

	public override void OnAnimationStart()
	{

	}

	public override void OnAnimationMove()
	{

	}

	public override void OnAnimationEvent()
	{
		GameManager.instance.audioPlayer.PlayPoint("MooseAttack", transform.position);
		_nowCols.Now(transform,(_life) =>
		{
			_life.DamageYY(new YinYang(0,30), DamageType.DirectHit);
			// 기절 ++
			Vector3 vec = _life.transform.position - transform.position;
			vec.y = 0;
			vec.Normalize();

			
			
			_life.GetActor().move.forceDir = vec * 20 + new Vector3(0,8,0);
			//_life.GetActor().move.forceDir.y = 40;
			
			Debug.LogError("시발시발시발시발" + _life.GetActor().move.forceDir);
		});
		EffectObject eff =  PoolManager.GetEffect($"SandBoomb", transform);
		eff.Begin();
	}

	public override void OnAnimationEnd()
	{

		if (_nowCols != null)
		{
			_nowCols.End();
			_nowCols = null;
		}
	}

	public override void OnAnimationSound()
	{
		
	}

	public override void OnAnimationStop()
	{
		self.AI.StartExamine();
	}

	public override void Attack()
	{


		if (_nowCols != null)
		{
			_nowCols.End();
			_nowCols = null;
		}
		GameObject obj = PoolManager.GetObject($"Moose_noraml_Attack", transform);
		
		if (obj.TryGetComponent(out ColliderCast cols))
		{
			_nowCols = cols;
		}
		
		
		//GetActor().anim.SetAttackTrigger();
		
		GetActor().anim.Animators.SetTrigger(Animator.StringToHash($"Attack1"));

	}
}
