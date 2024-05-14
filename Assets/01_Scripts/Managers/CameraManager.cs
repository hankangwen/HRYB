using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
	public List<CinemachineBasicMultiChannelPerlin> camShakers = new List<CinemachineBasicMultiChannelPerlin>();
	SkillProduction _skillProduct;
	public CamStatus curCamStat;
	
	public const int FORWARDCAM = 20;
	public const int BACKWARDCAM = 10;
	
	bool blinded = false;
	Volume v;
	Camera _main;
	
	public CinemachineFreeLook pCam;
	public CinemachineVirtualCamera aimCam;

	float originYSpeed;

	public static CameraManager instance;
	
	public Camera MainCam
	{
		get
		{
			if (_main == null)
			{
				_main = Camera.main;
			}
			
			return _main;
		}
	}

	MoveModule _playerModule;


	public void RegisterSkillCam(SkillProduction _sk)
	{
		if(_skillProduct != null)
			_skillProduct.End();

		_skillProduct = _sk;
	}


	private void Awake()
	{
		instance = this;
		_main = Camera.main;
		
		pCam = GetComponent<CinemachineFreeLook>();
		aimCam = GameObject.Find("AimCam").GetComponent<CinemachineVirtualCamera>();
		SwitchTo(CamStatus.Freelook);
		for (int i = 0; i < 3; i++)
		{
			camShakers.Add(pCam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
		}

		v = _main.GetComponentInChildren<Volume>();
		//Debug.LogError(camShakers.Count);
		for (int i = 0; i < camShakers.Count; i++)
		{
			camShakers[i].m_AmplitudeGain = 0;
			camShakers[i].m_FrequencyGain = 0;
		}

		originYSpeed = pCam.m_YAxis.m_MaxSpeed;
	}

	private void Start()
	{

		_playerModule = GameManager.instance.player.GetComponent<MoveModule>();
	}

	private void Update()
	{
		if(_playerModule.moveModuleStat.Paused == false && _skillProduct != null)
		{
			_skillProduct.End();
			_skillProduct = null;
		}
	}

	public void SwitchTo(CamStatus stat)
	{
		curCamStat = stat;
		switch (stat)
		{
			case CamStatus.Freelook:
				pCam.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
				pCam.m_XAxis.m_Wrap = true;
				pCam.Priority = FORWARDCAM;
				aimCam.Priority = BACKWARDCAM;
				break;
			case CamStatus.Aim:
				aimCam.Priority = FORWARDCAM;
				pCam.Priority = BACKWARDCAM;
				break;
			case CamStatus.Locked:
				pCam.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
				pCam.m_XAxis.m_Wrap = false;
				pCam.Priority = FORWARDCAM;
				aimCam.Priority = BACKWARDCAM;
				break;
			default:
				break;
		}
	}
	
	public void ShakeCamFor(float dur, float ampGain = 0.5f, float frqGain = 1f)
	{
		StartCoroutine(DelShakeCam(dur,ampGain, frqGain));
	}
	
	IEnumerator DelShakeCam(float dur, float ampGain = 0.5f, float frqGain = 1f)
	{
		ShakeCam(ampGain, frqGain);
		yield return new WaitForSeconds(dur);
		UnShakeCam(ampGain, frqGain);
	}

	public void FreezeCamX(bool useCurrentCamX = false)
	{
		if (useCurrentCamX)
		{
			pCam.m_XAxis.m_MinValue = pCam.m_XAxis.Value;
			pCam.m_XAxis.m_MaxValue = pCam.m_XAxis.Value;
		}
		pCam.m_XAxis.m_Wrap = false;
	}

	public void UnfreezeCamX()
	{
		pCam.m_XAxis.m_MinValue = 0;
		pCam.m_XAxis.m_MaxValue = 0;
		pCam.m_XAxis.m_Wrap = true;
	}

	public void FreezeCamY()
	{
		pCam.m_YAxis.m_MaxSpeed = 0;
	}

	public void UnfreezeCamY()
	{
		pCam.m_YAxis.m_MaxSpeed = originYSpeed;
	}

	public void ShakeCam(float ampGain, float frqGain)
	{
		//switch (curCamStat)
		//{
		//	case CamStatus.Freelook:
		//	case CamStatus.Locked:
		//		break;
		//	case CamStatus.Aim:
		//		camShaker.m_AmplitudeGain = ampGain;
		//		camShaker.m_FrequencyGain = frqGain;
		//		break;

		//	default:
		//		break;
		//}
		for (int i = 0; i < camShakers.Count; i++)
		{
			camShakers[i].m_AmplitudeGain += ampGain;
			camShakers[i].m_FrequencyGain += frqGain;
		}

		if (_skillProduct != null)
		{
			_skillProduct._shakes.m_AmplitudeGain = ampGain;
			_skillProduct._shakes.m_FrequencyGain = frqGain;

		}
	}

	
	public void UnShakeCam(float ampGain, float frqGain)
	{
		//switch (curCamStat)
		//{
		//	case CamStatus.Freelook:
		//	case CamStatus.Locked:
		//		break;
		//	case CamStatus.Aim:
		//		camShaker.m_AmplitudeGain = 0;
		//		camShaker.m_FrequencyGain = 0;
		//		break;

		//	default:
		//		break;
		//}
		for (int i = 0; i < camShakers.Count; i++)
		{
			camShakers[i].m_AmplitudeGain -= ampGain;
			camShakers[i].m_FrequencyGain -= frqGain;
		}
	}
	public void Blind(bool stat)
	{
		if(blinded != stat)
		{
			blinded = stat;
			v.gameObject.SetActive(stat);
		}
	}
}
