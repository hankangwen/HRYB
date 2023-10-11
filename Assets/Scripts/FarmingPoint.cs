using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingPoint : MonoBehaviour, IInterable
{
	const float RECENTERINGTIME = 0.2f;


	public float interTime = 1.0f;
	public bool isInterable = true;
	public bool isDestroyed = true;

	public string resItem;
	public int amount;

	public bool IsInterable { get => isInterable; set => isInterable = value; }
	public float InterTime { get => interTime; set => interTime = value; }

	Renderer r;
	Material mat;

	private static readonly int GlowPowerHash = Shader.PropertyToID("_GlowPower");
	Coroutine ongoing;

	private void Awake()
	{
		r = GetComponent<Renderer>();
		mat = r.material;
		r.material = new Material(mat);
		mat = r.material;
		GlowOff();
	}

	public void GlowOn()
	{
		mat.SetFloat(GlowPowerHash, 0.5f);
		
	}

	public void GlowOff()
	{
		mat.SetFloat(GlowPowerHash, 0f);
	}

	public void InteractWith()
	{
		if(ongoing == null)
		{
			ongoing = GameManager.instance.StartCoroutine(DelInter()); //�ӽ�. Ǯ������ �����ϸ� �׳� ���⼭ �ϸ� ��/
		}
	}

	void Inter()
	{
		int leftovers = 0;
		if (Item.nameDataHashT.ContainsKey(resItem.GetHashCode()))
		{
			leftovers = (GameManager.instance.pinven.AddItem((Item.nameDataHashT[resItem.GetHashCode()] as Item), amount));
		}
		if(leftovers > 0)
		{ 
		Debug.Log("������ �����ڴ�.");
		}
		Debug.Log(transform.name);
		if (isDestroyed)
		{
			Destroy(gameObject); //�ӽ�. ���� Ǯ������ ����.
		}
	}

	IEnumerator DelInter()
	{
		GameManager.instance.pinp.DeactivateInput();
		float t = 0;
		while(t < InterTime)
		{
			t += Time.deltaTime;
			yield return null;
		}
		Inter();
		GameManager.instance.pinp.ActivateInput();
		
		ongoing = null;
	}
}
