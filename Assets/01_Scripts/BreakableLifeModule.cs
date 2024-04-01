using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLifeModule : LifeModule
{
	public string destroyedEff;
	public float fadeSec;

	Material mat;

	public override void Awake()
	{
		base.Awake();
		mat = GetComponent<Renderer>().material;
	}

	public override void OnDead()
	{
		StartCoroutine(DelFader());
		PoolManager.GetObject(destroyedEff, transform.position, Quaternion.identity, 5f);
	}

	IEnumerator DelFader()
	{
		float t = 0;
		while(t <= fadeSec)
		{
			t += Time.deltaTime;
			mat.SetFloat("_Alpha", 1 - (t / fadeSec));
			yield return null;
		}
		gameObject.SetActive(false);
	}
}
