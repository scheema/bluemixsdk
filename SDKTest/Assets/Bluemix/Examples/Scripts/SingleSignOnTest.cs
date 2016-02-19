using System;
using UnityEngine;
using MiniJSON;
using Bluemix.SingleSignOn;

public class SingleSignOnTest : MonoBehaviour
{
	public event Action<string> onAuthenticated;
	
	public void Authenticate()
	{		
		SingleSignOn.Authenticate((string s, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onAuthenticated != null)
			{
				this.onAuthenticated(s);
			}
		});
	}
}
