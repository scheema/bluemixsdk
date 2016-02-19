using MiniJSON;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluemix
{
	namespace SingleSignOn
	{
		/// <summary>
		/// The core class responsible for the Single Sign On service.
		/// </summary>
		public static class SingleSignOn
		{
			public static void Authenticate(Action<string, Exception> callback)
			{				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(BluemixConnection.Instance.appURL + "/api/single_sign_on/authenticate", null, null, (WWW www) =>
				{									
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
						
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(www.text, e);
					}
				}));
			}
		}
	}
}