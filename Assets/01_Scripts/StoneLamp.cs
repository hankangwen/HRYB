using System.Collections;
using UnityEngine;

public class StoneLamp : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Material material;

	public int StoneLampIdx;

	private bool isEffect;
	private bool lightEnabled = false;

	TitleLoader loader;
	private GameObject effectObj;

	[Header("석등이 켜지는 거리")]
	[SerializeField]
	private float pDistance = 3;

	[Header("석등 켜지는 시간")]
	[SerializeField]
	private float lightTime = 3;

	[Header("출력 텍스트")]
	public string text;

	private Light pointLight;
	private float pointLightIntensity;

	const string EmissionRatio = "_Emission_Ratio";

	private void Awake()
	{
		pointLight = GetComponentInChildren<Light>();
		if (pointLight)
		{
			pointLightIntensity = pointLight.intensity;

		}

		meshRenderer = GetComponentInChildren<MeshRenderer>();
		if (meshRenderer)
		{
			material = new Material(meshRenderer.material);
			meshRenderer.material = material;
		}
	}

	void Start()
	{
		//text = "[세이브 포인트 활성화]";
		loader = GameObject.Find("TitleLoad").GetComponent<TitleLoader>();

		lightEnabled = false;
		material.SetFloat(EmissionRatio, 0);
		pointLight.intensity = 0;

		isEffect = false;
	}

	private void Update()
	{
		//플레이어가 접근 했을 때
		if (Vector3.Distance(GameManager.instance.player.transform.position, this.transform.position) < pDistance)
		{
			if (!lightEnabled && !loader.isFade)
			{
				GameManager.instance.lastSave = StoneLampIdx;
				loader.FadeInOut(text, 0.5f);
				StartCoroutine(LightOn());
			}

			isEffect = true;
			StartCoroutine(Heal());
		}
		else
		{
			isEffect = false;
		}

	}

	private IEnumerator LightOn()
	{
		GameManager.instance.audioPlayer.PlayPoint("LightOn", transform.position);
		WaitForSeconds wait = new WaitForSeconds(0.005f);
		WaitForSeconds lateWait = new WaitForSeconds(0.1f);
		lightEnabled = true;

		float time = 0;
		while (time < lightTime)
		{
			float value = time / lightTime;
			material.SetFloat(EmissionRatio, value);

			time += Time.deltaTime;
			pointLight.intensity = Mathf.Lerp(0, pointLightIntensity, value);

			if (time < 0.1f)
			{
				yield return wait;
				time += 0.005f;
			}
			else
			{
				yield return lateWait;
				time += 0.1f;
			}
		}
	}
	private IEnumerator Heal()
	{
		WaitForSeconds wait = new WaitForSeconds(1.0f);

		while (true)
		{
			if (isEffect == false)
			{
				if (effectObj != null)
				{
					PoolManager.ReturnObject(effectObj);
				}

				break;
			}
			else
			{
				//플레이어가 체력이 없을때
				if (!((GameManager.instance.pActor.life.yy.white + GameManager.instance.pActor.life.initYinYang.white * 0.02f) >= GameManager.instance.pActor.life.initYinYang.white))
				{
					GameManager.instance.pActor.life.yy.white += GameManager.instance.pActor.life.initYinYang.white * 0.02f;

					if (effectObj == null)
					{
						effectObj = PoolManager.GetObject($"Heal", transform);
					}

					effectObj.transform.position = GameManager.instance.player.transform.position;

					GameManager.instance.audioPlayer.PlayPoint("LightHeal", transform.position);
				}
			}


			yield return wait;
		}

	}
}