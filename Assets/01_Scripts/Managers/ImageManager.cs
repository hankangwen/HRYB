using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
	public static Sprite MedicineSprite;
	public const string LIQMEDICINE = "탕약";

	public ItemNameDictionary dictionary;
	public void DoLoad()
	{
		var icons = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "itemicons"));
		if(icons == null)
			Debug.LogError("LOAD FAIL");

		Sprite[] allIcons =  icons.LoadAllAssets<Sprite>();

		for (int i = 0; i < allIcons.Length; i++)
		{
			if (dictionary.Dict.ContainsKey(allIcons[i].name))
			{
				Debug.Log($"{allIcons[i].name} ==> {dictionary.Dict[allIcons[i].name]}");
				if (dictionary.Dict[allIcons[i].name] == LIQMEDICINE)
				{
					MedicineSprite = allIcons[i];
					Debug.Log($"아이템 스프라이트 설정 완료 : {allIcons[i].name} -> {LIQMEDICINE}");
				}
				else
				{
					if (Item.nameDataHashT.ContainsKey(dictionary.Dict[allIcons[i].name]))
					{
						(Item.nameDataHashT[(dictionary.Dict[allIcons[i].name])] as Item).icon = allIcons[i];
						Debug.Log($"아이템 스프라이트 설정 완료 : {allIcons[i].name} -> {(Item.nameDataHashT[dictionary.Dict[allIcons[i].name]] as Item).MyName}");
					}
					else
					{
						Debug.LogError($"아이템 스프라이트 설정 실패 : {allIcons[i].name}, 대상 아이템이 존재하지 않음.");
					}
				}
			}
			else
			{
				Debug.LogError($"아이템 스프라이트 설정 실패 : {allIcons[i].name}, 대상 아이템이 존재하지 않음.");
			}
		}
	}
}
