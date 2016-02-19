using System;
using UnityEngine;
using MiniJSON;
using Bluemix.PersonalityInsights;

public class PersonalityInsightsTest : MonoBehaviour
{
	public TextAsset document;
	
	public event Action<Profile> onProfileGenerated;
	
	#region Profile
	public void Profile()
	{		
		PersonalityInsights.Profile(this.document.text, ContentLanguage.en, AcceptLanguage.en, false, (Profile p, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onProfileGenerated != null)
			{
				this.onProfileGenerated(p);
			}
		});
	}
	#endregion
}
