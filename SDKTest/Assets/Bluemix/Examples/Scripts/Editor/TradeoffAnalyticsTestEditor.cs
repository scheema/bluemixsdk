using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TradeoffAnalyticsTest))]
public class TradeoffAnalyticsTestEditor : Editor
{
	public override void OnInspectorGUI()
	{
		TradeoffAnalyticsTest taTarget = (TradeoffAnalyticsTest) this.target;
		taTarget.document = (TextAsset) EditorGUILayout.ObjectField("Document", taTarget.document, typeof(TextAsset), false);
		
		if(GUILayout.Button("Dilemmas"))
		{
			taTarget.Dilemmas();
		}
	}
}