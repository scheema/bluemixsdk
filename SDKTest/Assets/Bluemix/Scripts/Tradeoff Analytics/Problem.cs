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
		/// Problem model used as input to the Watson Tradeoff Aanlytics Dilemmas service.
		/// </summary>
		public class Problem
		{
			/// <summary>
			/// The name of the decision problem.
			/// </summary>
			public string subject;

			/// <summary>
			/// A list of possible objectives for the decision problem.
			/// </summary>
			public List<ColumnDefinition> columns;

			/// <summary>
			/// A list of options for the decision problem.
			/// </summary>
			public List<Option> options;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Problem"/> class.
			/// </summary>
			public Problem()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Problem"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Problem.</param>
			public Problem(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Problem"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Problem.</param>
			public Problem(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "subject":
							this.subject = value as string;
							break;
							
						case "columns":
							List<object> columns = value as List<object>;							
							this.columns = new List<ColumnDefinition>();							
							for (int i = 0; i < columns.Count; i++)
							{
								this.columns.Add(new ColumnDefinition(columns[i] as Dictionary<string, object>));
							}
							break;
							
						case "options":
							List<object> options = value as List<object>;							
							this.options = new List<Option>();						
							for (int i = 0; i < options.Count; i++)
							{
								this.options.Add(new Option(options[i] as Dictionary<string, object>));
							}
							break;
							
						default:
							Debug.LogWarning("Unsupported Problem key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Problem"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Problem"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.subject != null)
				{
					stringBuilder.Append("\"subject\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.subject);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.columns != null && this.columns.Count > 0)
				{
					stringBuilder.Append("\"columns\":[");
					for (int i = 0; i < this.columns.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.columns[i].ToString());
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.options != null && this.options.Count > 0)
				{
					stringBuilder.Append("\"options\":[");
					for (int i = 0; i < this.options.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.options[i].ToString());
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