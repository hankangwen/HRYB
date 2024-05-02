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

	public void Begin(GameObject Follow = null, GameObject See = null)
	{
		_cam.Priority = 100;
		GameManager.instance.camManager.RegisterSkillCam(this);
		if(Follow != null)
			_cam.Follow = Follow.transform;
		if(See != null)
			_cam.LookAt = See.transform;
	}

	private void Update()
	{
		_shakes.m_AmplitudeGain = CameraManager.instance.camShakers[0].m_AmplitudeGain;
		_shakes.m_FrequencyGain = CameraManager.instance.camShakers[0].m_FrequencyGain;
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
