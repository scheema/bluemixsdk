using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudantTest))]
public class CloudantTestEditor : Editor
{
	public override void OnInspectorGUI()
	{		
		CloudantTest ccTarget = (CloudantTest) this.target;
		
		EditorGUILayout.Space();
		
		ccTarget.databaseName = EditorGUILayout.TextField("Database Name", ccTarget.databaseName);
		
		if (GUILayout.Button("Create Database"))
		{
			ccTarget.CreateDatabase();
		}

		if (GUILayout.Button("Read Database"))
		{
			ccTarget.ReadDatabase();
		}

		if (GUILayout.Button("Get Databases"))
		{
			ccTarget.GetDatabases();
		}

		if (GUILayout.Button("Get Documents In Database"))
		{
			ccTarget.GetDocumentsInDatabase();
		}

		if (GUILayout.Button("Get Changes In Database"))
		{
			ccTarget.GetChangesInDatabase();
		}
		
		if (GUILayout.Button("Delete Database"))
		{
			ccTarget.DeleteDatabase();
		}
		
		EditorGUILayout.Space();
		
		ccTarget.documentID = EditorGUILayout.TextField("Document ID", ccTarget.documentID);
		ccTarget.jsonDocument = EditorGUILayout.TextField("JSON Document", ccTarget.jsonDocument);
		ccTarget.latestRevision = EditorGUILayout.TextField("Latest Revision", ccTarget.latestRevision);
		
		if (GUILayout.Button("Create Document"))
		{
			ccTarget.CreateDocument();
		}

		if (GUILayout.Button("Read Document"))
		{
			ccTarget.ReadDocument();
		}

		if (GUILayout.Button("Update Document"))
		{
			ccTarget.UpdateDocument();
		}
		
		if (GUILayout.Button("Delete Document"))
		{
			ccTarget.DeleteDocument();
		}
	}
}
