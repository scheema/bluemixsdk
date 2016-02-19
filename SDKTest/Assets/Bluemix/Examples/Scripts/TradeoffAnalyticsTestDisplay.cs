using UnityEngine;
using UnityEngine.UI;
using Bluemix.TradeoffAnalytics;

public class TradeoffAnalyticsTestDisplay : MonoBehaviour
{
	public Transform content;
	public Text template;
	
	private void Awake()
	{
		this.gameObject.GetComponent<TradeoffAnalyticsTest>().onDilemmaGenerated += this.DisplayDilemma;
	}
	
	private void DisplayDilemma(Dilemma d)
	{		
		foreach (SolutionPerspective sp in d.resolution.solutions)
		{
			Text text = GameObject.Instantiate<Text>(this.template);
			text.gameObject.SetActive(true);
			text.transform.SetParent(this.content);
			text.text = sp.solutionRef + ": " + sp.status;
		}
		
		/*
		Dictionary<string, object> dilemmas = Json.Deserialize(jsonResponse) as Dictionary<string, object>;
		
		List<object> children = (profile["tree"] as Dictionary<string, object>)["children"] as List<object>;
		
		for (int i = 0; i < children.Count; i++)
		{
			this.AddDataDisplay(children[i], this.content);
		}
		*/
	}
	
	/*
	private void AddDataDisplay(object data, Transform parent)
	{
		Dictionary<string, object> dict = data as Dictionary<string, object>;
		
		Text text = GameObject.Instantiate<Text>(this.template);
		text.gameObject.SetActive(true);
		text.transform.SetParent(parent);
		
		StringBuilder stringBuilder = new StringBuilder();
		if (dict.ContainsKey("name"))
		{
			stringBuilder.Append(dict["name"].ToString());
			if (dict.ContainsKey("percentage"))
			{
				stringBuilder.Append(": ");
				stringBuilder.Append(dict["percentage"].ToString());
			}
			text.text = stringBuilder.ToString();
		}
		else
		{
			text.text = "UNKNOWN";
		}
		
		if (dict.ContainsKey("children"))
		{
			List<object> children = dict["children"] as List<object>;
			for (int i = 0; i < children.Count; i++)
			{
				this.AddDataDisplay(children[i], text.transform);
			}
		}
	}
	*/
}