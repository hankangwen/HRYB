using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuestCreater : EditorWindow
{
	string loadName;

	QuestInfo info;

	const string QUESTINFOPATH = "Quest/AllQuests/";
	const string ASSETPATH = "Assets/Resources/";

	[MenuItem("퀘스트 만들기/퀘스트 정보")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(QuestCreater));
	}

	void OnGUI()
	{
		GUILayout.Label("퀘스트 정보 설정", EditorStyles.boldLabel);
		Rect r = EditorGUILayout.BeginHorizontal();
		loadName = EditorGUILayout.TextField("로드/저장할 파일 이름", loadName);

		if(GUILayout.Button("확인하기"))
		{
			info = Resources.Load<QuestInfo>($"{QUESTINFOPATH}{loadName}");
			Debug.Log("LOAD COMPLETED");
			if(info == null)
			{
				info = CreateInstance<QuestInfo>();
				AssetDatabase.CreateAsset(info, $"{ASSETPATH}{QUESTINFOPATH}{loadName}.asset");
				Debug.Log("CREATE COMPLETED");
			}
			
			if(info == null)
			{
				Debug.LogError("CREATE FAILED");
			}
		}
		EditorGUILayout.EndHorizontal();
		
	}
}
