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
		/// Resolution returned as part of the Watson Tradeoff Analytics Dilemmas service.
		/// </summary>
		public class Resolution
		{
			/// <summary>
			/// List of Solution Perspective objects that contains the analytical data prepared by the service for each option of the decision problem.
			/// </summary>
			public List<SolutionPerspective> solutions;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Resolution"/> class.
			/// </summary>
			public Resolution()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Resolution"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Resolution.</param>
			public Resolution(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Resolution"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Resolution.</param>
			public Resolution(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "solutions":
							List<object> solutions = value as List<object>;							
							this.solutions = new List<SolutionPerspective>();						
							for (int i = 0; i < solutions.Count; i++)
							{
								this.solutions.Add(new SolutionPerspective(solutions[i] as Dictionary<string, object>));
							}
							break;

						default:
							Debug.LogWarning("Unsupported Resolution key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Resolution"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Resolution"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.solutions != null && this.solutions.Count > 0)
				{
					stringBuilder.Append("\"solutions\":[");
					for (int i = 0; i < this.solutions.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.solutions[i].ToString());
					}
					stringBuilder.Append("]");
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