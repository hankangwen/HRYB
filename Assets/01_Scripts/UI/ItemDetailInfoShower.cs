using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailInfoShower : MonoBehaviour
{
    Image fry;
    Image stir;
    Image burn;
    Image mash;

	private void Awake()
	{
		fry = transform.Find("Fry").GetComponent<Image>();
		stir = transform.Find("Stir").GetComponent<Image>();
		burn = transform.Find("Burn").GetComponent<Image>();
		mash = transform.Find("Mash").GetComponent<Image>();
	}

	public void SetInfo(HashSet<ProcessType> processes)
	{
		fry.enabled = processes.Contains(ProcessType.Fry);
		stir.enabled = processes.Contains(ProcessType.Stir);
		burn.enabled = processes.Contains(ProcessType.Burn);
		mash.enabled = processes.Contains(ProcessType.Mash);
		
	}

	public void ResetInfo()
	{
		fry.enabled = false;
		stir.enabled = false;
		burn.enabled = false;
		mash.enabled = false;
	}
}
