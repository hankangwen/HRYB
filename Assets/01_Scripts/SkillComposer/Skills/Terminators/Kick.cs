using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Infos/Kick")]
public class Kick : AttackBase
{
	BoxColliderCast caster;



	protected override void Awake()
	{
		base.Awake();
		caster = relatedTransform.GetComponent<BoxColliderCast>();
		if(caster == null)
		{
			Debug.Log($"NO BOXCOLLIDERCAST FOUND IN : {relatedTransform.name}");
		}
	}

	public override void Operate(Actor self)
	{
		Debug.Log("!@!@!@!@!@!@");
		(self.atk as PlayerAttack).onNextUse?.Invoke(relatedTransform.gameObject);


	}

	public override void Disoperate(Actor self)
	{
		base.Disoperate(self);
	}

	public override void UpdateStatus()	// ?? 스킬 시전중
	{
		
	}

	internal override void MyDisoperation(Actor self) // 사라질 때
	{
		caster.End();
		//트레일지워주기
		Debug.Log("KICKENDER");
	}

	internal override void MyOperation(Actor self) // 애니메이션? 이밴트
	{
		GameManager.instance.audioPlayer.PlayPoint(audioClipName, self.transform.position);
		caster.Now( self.transform,life =>
		{
			Debug.Log("^^^^^^^^^^^^^^^^^^^^^^^^^^");
			if(life != null)
			{
				CameraManager.instance.ShakeCamFor(0.1f);
				//Debug.Log(hitEffs.Count);
				Vector3 effPos = life.transform.GetComponent<Collider>().ClosestPointOnBounds(caster.transform.position);
				(self.atk as PlayerAttack).onNextSkill?.Invoke(self, this);
				(self.atk as PlayerAttack).onNextHit?.Invoke(effPos);

				DoDamage(life.GetActor(), self);
				PoolManager.GetObject("Hit 26", effPos, -caster.transform.forward, 2.5f);
			}
			
		});
	}
	
}
