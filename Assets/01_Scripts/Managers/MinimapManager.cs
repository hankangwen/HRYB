using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
	public List<Sprite> typeImage;
	public const int MINIMAPTEXQUALITY = 512;

	MinimapShower shower;
	Texture2D mapInfos;

	List<MinimapTarget> targs;

	private void Awake()
	{
		shower = GameObject.Find("MinimapContent").GetComponent<MinimapShower>();
		mapInfos= new Texture2D(MINIMAPTEXQUALITY, MINIMAPTEXQUALITY);
		shower.SetInfo(mapInfos);
		targs = new List<MinimapTarget>();
	}

	public void DoRender()
	{
		for (int k = 0; k < targs.Count; k++)
		{
			float angleDiffY = targs[k].transform.eulerAngles.y;
			for (int i = 0; i < targs[k].myRendered.texture.width; i++)
			{
				for (int j = 0; j < targs[k].myRendered.texture.height; j++)
				{
					
				}
			}
		}
		
	}

	public void AddRender(MinimapTarget targ)
	{
		targs.Add(targ);
	}
}
