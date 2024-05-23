using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AISetter : MonoBehaviour
{
	protected Actor self;
	[SerializeField] Actor _player;
	[SerializeField] SkinnedMeshRenderer _skinned;

	public Actor player
	{
		get
		{
			if(_player == null)
			{
				_player = FindObjectOfType<PlayerInter>().GetComponent<Actor>();

			}
			return _player;
		}
	}
	
	protected Selecter head;
	
	protected bool stopped = false;
	float stopEnAble = 0f;


	public virtual void LookAt(Transform t)
	{
		Vector3 lookPos = t.position - transform.position;
		lookPos.y = transform.position.y;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 40);
		transform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);
	}

	public virtual void DieEvent()
	{
		_skinned.materials[0].SetInt("_IsDissolve", 1);
		_skinned.materials[0].SetFloat("_DissolveHeight", 5);
		StartCoroutine(DissolveMat());
	}

	IEnumerator DissolveMat()
	{
		float t = 0;
		while (t < 3)
		{
			t += Time.deltaTime;
			_skinned.materials[0].SetFloat("_DissolveHeight", Mathf.Lerp(5,-5, t / 3.0f));
			//Debug.LogError(_skinned.materials[0].GetInteger("_IsDissolve")	+ " + " +Mathf.Lerp(5,0, t / 3.0f) +" 돼잖앗 ㅣ발");
			yield return null;
		}
		Destroy(this.gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
	    //_skinned.materials[0].SetInteger("_IsDissolve", 0);
	    self = GetComponent<Actor>();
	    head = new Selecter();
		StartInvoke();
    }

    /// <summary>
    /// Same To Start
    /// </summary>
    protected abstract void StartInvoke();

    // Update is called once per frame
    protected virtual void Update()
    {
	    if (!stopped && head != null)
	    {
		    head.Examine();
			stopEnAble = 0;
	    }
		else
		{
			stopEnAble += Time.deltaTime;
		}

		//if(stopEnAble >= 0.5f)
		//{
		//	StartExamine();
		//}

	    UpdateInvoke();
    }

    /// <summary>
    /// Same to Update
    /// </summary>
    protected abstract void UpdateInvoke();

    public void StopExamine()
    {
	    stopped = true;
		stopEnAble = 0;

	}

    public virtual void StartExamine()
    {
	    stopped = false;
    }

	public virtual void ResetStatus()
	{

	}
}
