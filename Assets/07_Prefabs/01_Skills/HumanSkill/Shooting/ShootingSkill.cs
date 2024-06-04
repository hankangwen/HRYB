
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Human/Startdust")]

public class ShootingSkill : AttackBase
{
	public override int ListValue()
	{
		return 2;
	}

	public override void UpdateStatus()
	{
	}

	internal override void MyDisoperation(Actor self)
	{
	}

	internal override void MyOperation(Actor self)
	{
	}
	public override void OnAnimationStart(Actor self, AnimationEvent evt)
	{
		GameManager.instance.DisableCtrl(false);
		(self.anim as PlayerAnim).AnimAct.PlayerAfterImage(0.2f, 1.4f, 0.66f);
	}

	public override void OnAnimationEvent(Actor self, AnimationEvent evt)
	{
		string[] tt = evt.stringParameter.Split("$");

		self.move.forceDir = new Vector3(0, 0, 0);

		if (self.atk.target == null)
			return;

		if (tt[0] == "0")
		{
			BezierAttack(self, 0);
		}
		else if (tt[0] == "1")
		{
			BezierAttack(self, 1);
		}

		//if (tt[0] == "Bezier")
		//{
		//}

	}


	public void BezierAttack(Actor self, int t)
	{
		GameObject obj = PoolManager.GetObject("HumanBullet", self.transform);

		Vector3 vec = self.transform.position + new Vector3(0, 1.2f, 0);
		if (obj.TryGetComponent<YGArrow>(out YGArrow _yg))
		{
			GameManager.instance.StartCoroutine(Del(_yg.gameObject));
			bool b = false;
			List<Vector3> rands = new();
			_yg.Ready(self, vec,


			(_pos, enemy, time) =>
			{
				if (b == false)
				{
					b = true;
					rands = _yg.Bezier.RandomVector(self, enemy, 5);
				}

				if (enemy == null)
				{
					PoolManager.ReturnObject(_yg.gameObject);
				}

				obj.transform.position = _yg.Bezier.BezierPathCalculation(rands[0], rands[1], rands[2], rands[3], time);
				//obj.transform.position = Vector3.Lerp(_pos, enemy, time / 0.3f);
			},
			(_life) =>
			{
				DoDamage(_life.GetActor(), self, _dmgs[t], obj.transform.position);
			},
			(_trm, _life) =>
			{
				// null
			});

			_yg.transform.parent = null;
			_yg.Fire();
		}
	}

	IEnumerator Del(GameObject obj)
	{
		yield return new WaitForSeconds(1.3f);
		PoolManager.ReturnObject(obj.gameObject);
	}


	public override void OnAnimationEnd(Actor self, AnimationEvent evt)
	{
		GameManager.instance.EnableCtrl();
	}
}
