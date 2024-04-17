using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProduction : MonoBehaviour
{
	CinemachineVirtualCamera _cam;
	public CinemachineBasicMultiChannelPerlin _shakes;

	private void Awake()
	{
		_cam = GetComponentInChildren<CinemachineVirtualCamera>();
		_shakes = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		
	}

	public void Begin()
	{
		_cam.Priority = 100;
		GameManager.instance.camManager.RegisterSkillCam(this);
	}

	public void End()
	{
		_cam.Priority = 0;
		StartCoroutine(ReturnLate());
	}


	IEnumerator ReturnLate()
	{
		yield return new WaitForSeconds(3f);

		PoolManager.ReturnObject(gameObject);
	}



}
