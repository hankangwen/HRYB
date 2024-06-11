using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
[ExecuteInEditMode]
public class BuildABAndPlay : Editor
{
	bool prevCompiling;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		
		if (prevCompiling && !EditorApplication.isCompiling)
		{
			AssetBundleBuilder.BuildAllAssetBundles();
			Debug.Log("자동빌드하였습니다^^^^^^^^^");
		}
		prevCompiling = EditorApplication.isCompiling;
	}

}
