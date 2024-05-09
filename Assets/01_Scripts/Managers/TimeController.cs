using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

	//public AnimationCurve curve;
	//float _endTime = 0;
	Coroutine _co;
	public void TimeSlow(Actor pl1, Actor pl2, EffectObject eff)
	{
		Time.timeScale = 1;
		_co = StartCoroutine(TiimeCO(pl1,pl2, eff));
	}

	IEnumerator TiimeCO(Actor pl1, Actor pl2, EffectObject eff)
	{
		pl1.anim.Animators.speed = 0.1f;
		pl2.anim.Animators.speed = 0.1f;
		float x = eff.Particle.time;
		eff.Particle.Pause();
		for(int i =0; i< 12; i++)
			yield return new WaitForEndOfFrame();

		pl1.anim.Animators.speed = 1;
		pl2.anim.Animators.speed = 1;
		eff.Particle.Simulate(x);
		eff.Particle.Play();
	}

}
