using MiniJSON;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bluemix
{
	namespace TradeoffAnalytics
	{
		/// <summary>
		/// Dilemma returned from the Watson Tradeoff Analytics Dilemmas service.
		/// </summary>
		public class Dilemma
		{
			/// <summary>
			/// The Problem object submitted in the call to the dilemmas API of the service.
			/// </summary>
			public Problem problem;

			/// <summary>
			/// The resolution of the decision problem sent to the service.
			/// </summary>
			public Resolution resolution;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Dilemma"/> class.
			/// </summary>
			public Dilemma()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Dilemma"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Dilemma.</param>
			public Dilemma(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Dilemma"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Dilemma.</param></param>
			public Dilemma(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "problem":
							this.problem = new Problem(value as Dictionary<string, object>);
							break;

						case "resolution":
							this.resolution = new Resolution(value as Dictionary<string, object>);
							break;

						default:
							Debug.LogWarning("Unsupported Dilemma key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Dilemma"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Dilemma"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.problem != null)
				{
					stringBuilder.Append("\"problem\":");
					stringBuilder.Append(this.problem.ToString());
					stringBuilder.Append(",");
				}

				if (this.resolution != null)
				{
					stringBuilder.Append("\"resolution\":");
					stringBuilder.Append(this.resolution.ToString());
					stringBuilder.Append(",");
				}

				if (stringBuilder.Length > 1)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				stringBuilder.Append("}");
				return stringBuilder.ToString();
			}
		}
	}
}