using MiniJSON;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Bluemix.PersonalityInsights;

public class PersonalityInsightsTestDisplay : MonoBehaviour
{
	public Transform content;
	public Text template;
	
	private void Awake()
	{
		this.gameObject.GetComponent<PersonalityInsightsTest>().onProfileGenerated += this.DisplayProfile;
	}
	
	private void DisplayProfile(Profile p)
	{				
		List<Trait> traits = p.tree.children;

		for (int i = 0; i < traits.Count; i++)
		{
			this.AddDataDisplay(traits[i], this.content);
		}
	}
	
	private void AddDataDisplay(Trait t, Transform parent)
	{
		Text text = GameObject.Instantiate<Text>(this.template);
		text.gameObject.SetActive(true);
		text.transform.SetParent(parent);
		
		StringBuilder stringBuilder = new StringBuilder();
		if (t.name != null)
		{
			stringBuilder.Append(t.name);
			if (t.percentage != null)
			{
				stringBuilder.Append(": ");
				stringBuilder.Append(t.percentage);
			}
			text.text = stringBuilder.ToString();
		}
		else
		{
			text.text = "UNKNOWN";
		}
		
		if (t.children != null)
		{
			for (int i = 0; i < t.children.Count; i++)
			{
				this.AddDataDisplay(t.children[i], text.transform);
			}
		}
	}
}
