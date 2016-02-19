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
		/// Column Definition used as input to the Watson Tradeoff Analytics Dilemmas service.
		/// </summary>
		public class Option
		{
			/// <summary>
			/// An identifier for the option that is unique among all options for the problem (required).
			/// </summary>
			public string key;

			/// <summary>
			/// A map of key-value pairs that specifies a value for each column.
			/// </summary>
			public Dictionary<string, object> values;

			/// <summary>
			/// The name of the option.
			/// </summary>
			public string name;

			/// <summary>
			/// A description of the option in HTML format.
			/// </summary>
			public string descriptionHTML;

			/// <summary>
			/// A map of key-value pairs in which the application can pass application-specific information.
			/// </summary>
			public Dictionary<string, string> appData;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Option"/> class.
			/// </summary>
			public Option()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Option"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Option.</param>
			public Option(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.Option"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Option.</param>
			public Option(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "key":
							this.key = value as string;
							break;
							
						case "values":
							Dictionary<string, object> values = value as Dictionary<string, object>;							
							this.values = new Dictionary<string, object>();						
							foreach (string valueKey in values.Keys)
							{
								this.values.Add(valueKey, values[valueKey]);
							}
							break;
							
						case "name":
							this.name = value as string;
							break;
							
						case "description_html":
							this.descriptionHTML = value as string;
							break;
							
						case "app_data":
							Dictionary<string, string> appData = value as Dictionary<string, string>;							
							this.appData = new Dictionary<string, string>();
							if (appData != null)
							{
								foreach (string appDataKey in appData.Keys)
								{
									this.appData.Add(appDataKey, appData[appDataKey]);
								}
							}		
							break;
							
						default:
							Debug.LogWarning("Unsupported Option key in json response: " + key);
							break;						
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Option"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.Option"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.key != null)
				{
					stringBuilder.Append("\"key\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.key);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.values != null && this.values.Count > 0)
				{
					stringBuilder.Append("\"values\":[");
					foreach (KeyValuePair<string, object> kvp in this.values)
					{
						stringBuilder.Append(kvp.ToString());
						stringBuilder.Append(",");
					}
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.name != null)
				{
					stringBuilder.Append("\"name\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.name);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.descriptionHTML != null)
				{
					stringBuilder.Append("\"description_html\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.descriptionHTML);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.appData != null && this.appData.Count > 0)
				{
					stringBuilder.Append("\"app_data\":[");
					foreach (KeyValuePair<string, string> kvp in this.appData)
					{
						stringBuilder.Append(kvp.ToString());
						stringBuilder.Append(",");
					}
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
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