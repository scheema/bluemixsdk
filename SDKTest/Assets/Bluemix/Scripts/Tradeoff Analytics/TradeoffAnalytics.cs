using MiniJSON;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluemix
{
	namespace TradeoffAnalytics
	{
		/// <summary>
		/// The core class responsible for the Watson Tradeoff Analytics service.
		/// </summary>
		public static class TradeoffAnalytics
		{						
			/// <summary>
			/// Submit a decision problem contained in the specified Problem using the Watson Tradeoff Analytics service.
			/// </summary>
			/// <param name="body">The decision problem.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void Dilemmas(Problem p, Action<Dilemma, Exception> callback)
			{
				TradeoffAnalytics.Dilemmas(p.ToString(), callback);
			}

			/// <summary>
			/// Submit a decision problem contained in the specified plain text using the Watson Tradeoff Analytics service.
			/// </summary>
			/// <param name="body">Plain text containing the decision problem.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void Dilemmas(string plainText, Action<Dilemma, Exception> callback)
			{
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("problem", plainText);
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.POST(BluemixConnection.Instance.appURL + "/api/tradeoff_analytics/dilemmas", postData, headers, (WWW www) =>
				{									
					Dilemma d = null;
					Exception e = null;
					
					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);					
						e = new TradeoffAnalyticsException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						d = new Dilemma(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(d, e);
					}
				}));
			}
		}
	}
}