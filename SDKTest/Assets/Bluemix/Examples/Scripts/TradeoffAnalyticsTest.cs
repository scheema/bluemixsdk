using System;
using UnityEngine;
using Bluemix.TradeoffAnalytics;

public class TradeoffAnalyticsTest : MonoBehaviour
{
	public TextAsset document;
	
	public event Action<Dilemma> onDilemmaGenerated;
	
	#region Dilemmas
	public void Dilemmas()
	{
		TradeoffAnalytics.Dilemmas(this.document.text, (Dilemma d, Exception e) =>
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
				return;
			}
			
			if (this.onDilemmaGenerated != null)
			{
				this.onDilemmaGenerated(d);
			}
		});
	}
	#endregion
}
