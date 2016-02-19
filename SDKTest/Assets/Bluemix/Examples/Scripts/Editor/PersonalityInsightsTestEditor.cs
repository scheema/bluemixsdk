using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PersonalityInsightsTest))]
public class PersonalityInsightsTestEditor : Editor
{
	public override void OnInspectorGUI()
	{
		PersonalityInsightsTest piTarget = (PersonalityInsightsTest) this.target;
		piTarget.document = (TextAsset) EditorGUILayout.ObjectField("Document", piTarget.document, typeof(TextAsset), false);
		
		if(GUILayout.Button("Profile"))
		{
			piTarget.Profile();
		}
	}
}