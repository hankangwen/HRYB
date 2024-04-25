using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooFarHPFar : MonoBehaviour
{
	[Header("HP바 감지범위")]
	public int Range;

	GameObject obj;

	[Header("감지할 대상의 레이어")]
	public LayerMask layer;

	private SphereCollider Collider;

	private LifeModule lf;

	private void Awake()
	{
		obj = this.gameObject;
		Collider = GetComponent<SphereCollider>();
		lf = GetComponent<LifeModule>();
	}

	private void Start()
	{
		//obj = GameManager.instance.bHPManager.HideHP(this.transform);
		Collider.radius = Range;
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.layer == 7)
		{
			obj.SetActive(true);	
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 7)
		{
			obj.SetActive(false);
		}
	}
}
