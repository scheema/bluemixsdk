using MiniJSON;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Bluemix.SingleSignOn;

public class SingleSignOnTestDisplay : MonoBehaviour
{
	public Transform content;
	public Text template;
	
	private void Awake()
	{
		this.gameObject.GetComponent<SingleSignOnTest>().onAuthenticated += this.DisplayAuthentication;
	}
	
	private void DisplayAuthentication(string s)
	{				
		Text text = GameObject.Instantiate<Text>(this.template);
		text.gameObject.SetActive(true);
		text.transform.SetParent(this.content);
		text.text = s;
	}
}
