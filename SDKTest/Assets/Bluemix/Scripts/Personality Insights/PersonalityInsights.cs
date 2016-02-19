using MiniJSON;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluemix
{
	namespace PersonalityInsights
	{
		/// <summary>
		/// The content language of the request.
		/// </summary>
		public enum ContentLanguage
		{
			en,
			es
		}
		
		/// <summary>
		/// The desired language of the response. 
		/// </summary>
		public enum AcceptLanguage
		{
			en,
			es
		}
		
		/// <summary>
		/// The core class responsible for the Watson Personality Insights service.
		/// </summary>
		public static class PersonalityInsights
		{						
			/// <summary>
			/// Profile the specified Content List Container using the Watson Personality Insights service.
			/// </summary>
			/// <param name="clc">Content List Container to be profiled.</param>
			/// <param name="al">Accept language.</param>
			/// <param name="includeRaw">If set to <c>true</c> include raw scores.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void Profile(ContentListContainer clc, AcceptLanguage al, bool includeRaw, Action<Profile, Exception> callback)
			{
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("contentItems", clc.ToString());
				data.Add("accept_language", al.ToString());
				if (includeRaw)
				{
					data.Add("include_raw", "true");
				}				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.POST(BluemixConnection.Instance.appURL + "/api/personality_insights/profile", postData, headers, (WWW www) =>
				{									
					Profile p = null;
					Exception e = null;
					
					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);					
						e = new PersonalityInsightsException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						p = new Profile(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(p, e);
					}
				}));
			}
			
			/// <summary>
			/// Profile the specified plain text using the Watson Personality Insights service.
			/// </summary>
			/// <param name="plainText">Plain text to be profiled.</param>
			/// <param name="cl">Content language.</param>
			/// <param name="al">Accept language.</param>
			/// <param name="includeRaw">If set to <c>true</c> include raw scores.</param>
			/// <param name="callback">Request complete callback.</param>	
			public static void Profile(string plainText, ContentLanguage cl, AcceptLanguage al, bool includeRaw, Action<Profile, Exception> callback)
			{				
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("text", plainText);
				data.Add("language", cl.ToString());
				data.Add("accept_language", al.ToString());
				if (includeRaw)
				{
					data.Add("include_raw", "true");
				}
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.POST(BluemixConnection.Instance.appURL + "/api/personality_insights/profile", postData, headers, (WWW www) =>
				{									
					Profile p = null;
					Exception e = null;
					
					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);					
						e = new PersonalityInsightsException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						p = new Profile(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(p, e);
					}
				}));
			}
		}
	}
}