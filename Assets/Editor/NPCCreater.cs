using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCCreater : EditorWindow
{
	public const string CHARACTERPATH = "Assets/01_Scripts/NPCs/Characters/";

	Character curInfo;
	GameObject targetObj;
	TalkModule curActor;
	string actorName;

	List<string> convs;
	int curSel;

	static GUIStyle errorStyle;

    [MenuItem("NPC/캐릭터 생성하기")]
	public static void ShowWindow()
	{
		errorStyle = new GUIStyle();
		errorStyle.fontSize = 25;
		errorStyle.normal.textColor = Color.red;
		errorStyle.fontStyle = FontStyle.Bold;
		EditorWindow.GetWindow(typeof(NPCCreater));
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		actorName = EditorGUILayout.TextField("오브젝트 이름 : ", actorName);
		targetObj = GameObject.Find(actorName);
		EditorGUILayout.EndHorizontal();
		string errMsg = "";
		if(targetObj == null)
		{
			EditorGUILayout.BeginHorizontal();
			errMsg = "해당 오브젝트는 존재하지 않습니다.";
			GUILayout.Label(errMsg, errorStyle);
			if (GUILayout.Button("만들기"))
			{
				targetObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				targetObj.name = actorName;
				targetObj.layer = GameManager.INTERABLELAYER;
				targetObj.AddComponent<Animator>();
			}
			EditorGUILayout.EndHorizontal();
			return;
		}
		else if((curActor = targetObj.GetComponent<TalkModule>()) == null)
		{
			EditorGUILayout.BeginHorizontal();
			errMsg = "해당 오브젝트와 대화할 수 없습니다.";
			GUILayout.Label(errMsg, errorStyle);
			if (GUILayout.Button("수정하기"))
			{
				curActor = targetObj.AddComponent<TalkModule>();
			}
			EditorGUILayout.EndHorizontal();
			return;
		}

		if(curInfo == null)
			curInfo = CreateInstance<Character>();
		
		
		EditorGUILayout.BeginHorizontal();

		curInfo.baseName = EditorGUILayout.TextField("이름 : ", curInfo.baseName);
		curInfo.subName = EditorGUILayout.TextField("칭호 : ", curInfo.subName);

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

		convs = new List<string>( DialogueLoader.ConversationDialoguePair.Keys);
		curSel = EditorGUILayout.Popup(curSel, convs.ToArray());
		curInfo.initDia = DialogueLoader.ConversationDialoguePair[convs[curSel]];

		EditorGUILayout.EndHorizontal();


		if (GUILayout.Button("생성하기"))
		{
			EditorUtility.SetDirty(curInfo);

			curActor.charInfo = curInfo;

			AssetDatabase.CreateAsset(curInfo, $"{CHARACTERPATH}NPC_{actorName}.asset");
			curInfo.DoInit();

			Close();
		}


		EditorGUILayout.EndVertical();

	}
}
