using MiniJSON;
using System;
using System.Collections.Generic;
using UnityEngine;
using Bluemix.Cloudant;

public class CloudantTest : MonoBehaviour
{
	public string databaseName;
	
	public string documentID;
	public string jsonDocument;
	public string latestRevision;
	
	public event Action<string> onRequestComplete;
	
	#region Databases
	public void CreateDatabase()
	{
		Cloudant.CreateDatabase(this.databaseName, (Database db, Exception e) =>
		 {
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(db.dbName + " Created");
			}
		});
	}

	public void ReadDatabase()
	{
		Cloudant.ReadDatabase(this.databaseName, (Database db, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(db.ToString());
			}
		});
	}

	public void GetDatabases()
	{
		Cloudant.GetDatabases((List<Database> dbs, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}

			string s = "";
			foreach (Database db in dbs)
			{
				s += db.dbName + ", ";
			}
			if (s.Length > 2)
			{
				s = s.Substring(0, s.Length - 2);
			}
			if (string.IsNullOrEmpty(s))
			{
				s = "No databases found.";
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(s);
			}
		});
	}

	public void GetDocumentsInDatabase()
	{
		Cloudant.GetDocumentsInDatabase(this.databaseName, (DocumentQueryResponse dqr, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(dqr.ToString());
			}
		});
	}

	public void GetChangesInDatabase()
	{
		Cloudant.GetChangesInDatabase(this.databaseName, (ChangesQueryResponse cqr, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(cqr.ToString());
			}
		});
	}
	
	public void DeleteDatabase()
	{
		Cloudant.DeleteDatabase(this.databaseName, (Database db, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(db.dbName + " Deleted");
			}
		});
	}
	#endregion
	
	#region Documents
	public void CreateDocument()
	{
		Document d = new Document(Json.Deserialize(this.jsonDocument) as Dictionary<string, object>);

		Cloudant.CreateDocument(this.databaseName, d, (DocumentStub ds, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}

			this.documentID = ds.id;
			this.latestRevision = ds.rev;

			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(ds.ToString());
			}
		});
	}	

	public void ReadDocument()
	{
		Cloudant.ReadDocument(this.databaseName, this.documentID, (Document d, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}

			this.documentID = d.id;
			this.jsonDocument = d.ToString();
			this.latestRevision = d.rev;
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(d.ToString());
			}
		});
	}

	public void UpdateDocument()
	{
		Document d = new Document(Json.Deserialize(this.jsonDocument) as Dictionary<string, object>);
		d.data["_rev"] = this.latestRevision;

		Cloudant.UpdateDocument(this.databaseName, d, (DocumentStub ds, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}

			this.documentID = ds.id;
			this.latestRevision = ds.rev;
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(ds.ToString());
			}
		});
	}
	
	public void DeleteDocument()
	{
		DocumentStub documentStub = new DocumentStub();
		documentStub.id = this.documentID;
		documentStub.rev = this.latestRevision;

		Cloudant.DeleteDocument(this.databaseName, documentStub, (DocumentStub ds, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onRequestComplete != null)
			{
				this.onRequestComplete(ds.ToString());
			}
		});
	}
	#endregion
}