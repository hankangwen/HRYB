using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choist : MonoBehaviour
{
	public int choiseNum;

	public TMP_Text choiseTxt;

	private void Start()
	{
		choiseTxt = GetComponent<TMP_Text>();
	}



}
