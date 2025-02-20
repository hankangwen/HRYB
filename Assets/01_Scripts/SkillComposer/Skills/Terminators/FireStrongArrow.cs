using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Infos/FireStrongArrow")]
public class FireStrongArrow : AttackBase
{
	public string arrowPrefabName;
	public float angleY;

	public float scaleDiff;

	public ArrowMode aMode;

	public override int ListValue()
	{
		return 1;
	}

	public override void UpdateStatus()
	{
		//
	}

	internal override void MyDisoperation(Actor self)
	{
		//
	}

	internal override void MyOperation(Actor self)
	{
		//Debug.Log($"화살발사, {shootPos.position} : {shootPos.forward}");
		Arrow r = PoolManager.GetObject(arrowPrefabName, relatedTransform.position, relatedTransform.forward).GetComponent<Arrow>();
		r.transform.localScale *= scaleDiff;
		Vector3 localRot = r.transform.localEulerAngles;
		localRot.y += angleY;
		r.transform.localEulerAngles = localRot;
		//UnityEditor.EditorApplication.isPaused = true;
		//r.SetInfo(self.atk.Damage * _dmgs[0]._skillDamage, statEff, self);
		(self.atk as PlayerAttack).onNextUse?.Invoke(r.gameObject);
		(self.atk as PlayerAttack).onNextSkill?.Invoke(self, this);
		r.SetHitEff((self.atk as PlayerAttack).onNextHit);
		if (aMode == ArrowMode.Homing)
		{
			r.SetTarget(self.atk.target);
		}
		r.Shoot(aMode);
		GameManager.instance.audioPlayer.PlayPoint(audioClipName, self.transform.position);
	}
}
