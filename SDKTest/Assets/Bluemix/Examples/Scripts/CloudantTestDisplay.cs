using MiniJSON;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CloudantTestDisplay : MonoBehaviour
{
	public InputField inputField;
	
	private void Awake()
	{
		this.gameObject.GetComponent<CloudantTest>().onRequestComplete += this.DisplayJSONResponse;
	}
	
	private void DisplayJSONResponse(string jsonResponse)
	{
		this.inputField.text = jsonResponse;
	}
}
