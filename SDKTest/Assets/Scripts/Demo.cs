using MiniJSON;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bluemix.Cloudant;
using Bluemix.TradeoffAnalytics;

public class Demo : MonoBehaviour
{
	public TextAsset textDocument;
	private string id;
	private Document document;

	public void DoDemoOne()
	{
		Document d = new Document(this.textDocument.ToString());
		Cloudant.CreateDocument("finance", d, (DocumentStub ds, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
			else
			{
				this.id = ds.id;
				Debug.Log(this.id);
			}
		});
	}

	public void DoDemoTwo()
	{
		Cloudant.ReadDocument("finance", this.id, (Document d, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
			else
			{
				this.document = d;
				Debug.Log(d.ToString());
			}
		});
	}

	public void DoDemoThree()
	{
		//Dictionary<string, object> dic = Json.Deserialize(this.document.ToString()) as Dictionary<string, object>;
		//dic.Remove("_id");
		//dic.Remove("_rev");
		//Problem p = new Problem(Json.Serialize(dic));
		/*
		Problem p = new Problem(this.textDocument.ToString());
		TradeoffAnalytics.Dilemmas(p, (Dilemma d, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
			else
			{
				Debug.Log(d.ToString());
			}
		});
		*/

		TradeoffAnalytics.Dilemmas(this.document.ToString(), (Dilemma d, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
			else
			{
				foreach (SolutionPerspective s in d.resolution.solutions)
				{
					if (s.shadowMe == null || s.shadowMe.Count == 0)
					{
						Debug.Log(s.solutionRef);
					}
				}
			}
		});
	}
}