using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Linq;

public class NodeCreater : EditorWindow
{
	PlayerNode node;
	List<Vector2Int> requirementVectors;

	static List<string> statUpgradeTypes;
	static List<int> statUpgradeTypesValue;
	

	[MenuItem("노드/노드 생성하기")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(NodeCreater));
	}

	void OnGUI()
	{
		if(statUpgradeTypes == null || statUpgradeTypesValue == null)
		{
			List<StatUpgradeType> lst = new List<StatUpgradeType>(System.Enum.GetValues(typeof(StatUpgradeType)).Cast<StatUpgradeType>());
			if (statUpgradeTypes == null)
			{
				statUpgradeTypes = new List<string>();
				for (int i = 0; i < lst.Count; i++)
				{
					statUpgradeTypes.Add(NodeUtility.ToStringKorean(lst[i]));
				}
			}
			if (statUpgradeTypesValue == null)
			{
				statUpgradeTypesValue = new List<int>();
				for (int i = 0; i < lst.Count; i++)
				{
					statUpgradeTypesValue.Add(((int)lst[i]));
				}
			}
		}

		if(requirementVectors == null)
		{
			requirementVectors = new List<Vector2Int>();
		}

		if (node == null)
		{
			if(GUILayout.Button("노드 생성"))
			{
				node = ScriptableObject.CreateInstance<PlayerNode>();
			}
		}

		if(node == null)
			return;

		EditorGUILayout.BeginVertical();

		node.circleIndex = EditorGUILayout.IntField("몇 번째 동심원에 존재하는가?", node.circleIndex);
		try
		{
			node.orderIndex = NodeUtility.LoadNodeData(node.circleIndex).Count;
		}
		catch
		{
			Debug.Log("새로운 동심원에 들어갈 예정임.");
			node.orderIndex = 0;
		}
		
		GUILayout.Label($"{node.orderIndex} 번째에 들어갈 예정. (시계방향)");

		


		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("노드 학습시 성장하는 스테이터스 : ");
		node.nodeType = (StatUpgradeType)EditorGUILayout.IntPopup((int)node.nodeType, statUpgradeTypes.ToArray(), statUpgradeTypesValue.ToArray());
		if(node.nodeType != StatUpgradeType.Callback)
		{
			EditorGUILayout.Space(10);
			node.amt = EditorGUILayout.FloatField("증가량 : ", node.amt);
		}
		EditorGUILayout.EndHorizontal();
		

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space(20);
		if(GUILayout.Button("연결노드 추가하기"))
		{
			requirementVectors.Add(Vector2Int.zero);
		}
		if(GUILayout.Button("연결노드 제거하기"))
		{
			if(node.requirements.Count > 0)
			{
				requirementVectors.RemoveAt(requirementVectors.Count - 1);
			}

		}
		EditorGUILayout.BeginVertical();

		if(requirementVectors.Count == 0)
		{
			GUILayout.Label("바닥노드 입니다.");
		}
		else
		{
			node.requirements.Clear();
			for (int i = 0; i < requirementVectors.Count; i++)
			{
				GUILayout.Space(3);
				EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1f), Color.black);
				GUILayout.Space(3);
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("배우기 위해 필요한 노드 : ");
				int cIdx = requirementVectors[i].x, oIdx = requirementVectors[i].y;
				cIdx = EditorGUILayout.IntField(cIdx);
				GUILayout.Label("번째 동심원의 ");
				oIdx = EditorGUILayout.IntField(oIdx);
				GUILayout.Label("번째 노드 (시계방향)");

				EditorGUILayout.EndHorizontal();

				requirementVectors[i] = new Vector2Int(cIdx, oIdx);

				PlayerNode req = NodeUtility.LoadNodeData(cIdx, oIdx);

				if (req != null)
				{
					node.requirements.Add(req);
				}
				else
				{
					node.requirements.Add(null);
					GUILayout.Label("위 정보에 해당하는 노드를 찾을 수 없습니다.");
					
				}
			}
			GUILayout.Space(3);
			EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1f), Color.black);
			GUILayout.Space(3);
		}

		

		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(20);
		GUILayout.Label($"파일 이름 미리보기 : {node.circleIndex}_{node.orderIndex}_{node.nodeType}{NodeUtility.NODEFILENAME}.asset");
		EditorGUILayout.EndVertical();
		GUILayout.Space(20);

		if (node.requirements.Contains(null))
		{
			GUIStyle warningStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 25, fontStyle = FontStyle.Bold };
			warningStyle.normal.textColor = Color.red;
			warningStyle.hover.textColor = Color.red;

			GUILayout.Label("연결 노드가 유효하지 않습니다.", warningStyle);
		}
		else
		{
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("노드 저장하기"))
			{
				AssetDatabase.CreateAsset(node, $"{QuestManager.ASSETPATH}{NodeUtility.NODEPATH}{node.circleIndex}_{node.orderIndex}_{node.nodeType}{NodeUtility.NODEFILENAME}.asset");
				node = null;
				requirementVectors.Clear();
			}
			if (GUILayout.Button("노드 저장하고 종료하기"))
			{
				AssetDatabase.CreateAsset(node, $"{QuestManager.ASSETPATH}{NodeUtility.NODEPATH}{node.circleIndex}_{node.orderIndex}_{node.nodeType}{NodeUtility.NODEFILENAME}.asset");
				node = null;
				requirementVectors.Clear();
				Close();
			}
			EditorGUILayout.EndHorizontal();
		}
		
	}
}
