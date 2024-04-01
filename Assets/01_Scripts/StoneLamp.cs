using UnityEngine;

public class StoneLamp : MonoBehaviour
{
	MeshRenderer meshRenderer;
	Vector3 distance;
	Color color;
	float k = 0;

	public int stoneLampNum;

	bool isEffect;
	bool isText = false;

	TitleLoader loader;
	GameObject obj;

	[Header("석등이 켜지는 거리")]
	[SerializeField]
	private float pDistance = 3;

	[Header("석등 켜지는 시간")]
	[SerializeField]
	private float lightTime = 3;

	[Header("출력 텍스트")]
	public string text;

	float time;
	Light l;

	Material material;

	private void Awake()
	{
		l = GetComponentInChildren<Light>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();
		material = new Material(meshRenderer.material);

		meshRenderer.material = material;

		color = material.GetColor("_EmissionColor");
	}

	void Start()
    {
		text = "[세이브 포인트 활성화]";
		loader = GameObject.Find("TitleLoad").GetComponent<TitleLoader>();
		l.enabled = false;
		pDistance = 5;
		isText = false;
		material.DisableKeyword("_EMISSION");
		
	}

	private void Update()
	{
		if(Vector3.Distance(GameManager.instance.player.transform.position, this.transform.position) < pDistance)
		{
			if(!isText && !loader.isFade)
			{
				GameManager.instance.lastSave = stoneLampNum;
				loader.FadeInOut(text, 1f);
				isText = true;
				GameManager.instance.audioPlayer.PlayPoint("LightOn", transform.position);
			}
			
			l.enabled = true;
			material.EnableKeyword("_EMISSION");
			k= Mathf.Clamp(k + Time.deltaTime / lightTime,-2, 3);
			//Debug.Log(color * Mathf.Pow(2, Mathf.Lerp(-2, 3, k)));
			material.SetColor("_EmissionColor", color * Mathf.Pow(2, Mathf.Lerp(-2, 3, k)));

			time += Time.deltaTime;
			
			if (time > 1)
			{
				if(!((GameManager.instance.pActor.life.yy.white + GameManager.instance.pActor.life.initYinYang.white * 0.02f) > GameManager.instance.pActor.life.initYinYang.white))
				{
					GameManager.instance.pActor.life.yy.white += GameManager.instance.pActor.life.initYinYang.white;

					if (!isEffect)
					{
						obj = PoolManager.GetObject($"Heal", transform);
						GameManager.instance.audioPlayer.PlayPoint("LightHeal", transform.position);
						isEffect = true;
					}
					obj.transform.position = GameManager.instance.player.transform.position;
				}
				time = 0;
			}
		}
		else
		{
			isEffect = false;

			if(obj != null)
				PoolManager.ReturnObject(obj);
		}

	}	
}
