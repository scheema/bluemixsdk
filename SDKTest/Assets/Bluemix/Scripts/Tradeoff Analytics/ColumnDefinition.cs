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
		public class ColumnDefinition
		{
			/// <summary>
			/// An identifier for the column that is unique among all columns for the problem (required).
			/// </summary>
			public string key;

			/// <summary>
			/// An indication of whether the column is specified as a numeric value, a date and time value, a categorical value, or as text.
			/// </summary>
			public string type; //enum?

			/// <summary>
			/// The direction for the column.
			/// </summary>
			public string goal; //enum?

			/// <summary>
			/// An indication of whether the column is an objective for the problem.
			/// </summary>
			public bool? isObjective;

			/// <summary>
			/// The range of valid values for the column.
			/// </summary>
			public IRange range;

			/// <summary>
			/// For columns whose type is numeric or datetime, specifies how the value is to be displayed.
			/// </summary>
			public string format;

			/// <summary>
			/// For columns whose type is categorical, specifies an array of strings that is a preferred subset of the strings in the column's range.
			/// </summary>
			public List<string> preference;

			/// <summary>
			/// A value that is considered a significant gain for the objective.
			/// The value can range from 0 to 1.
			/// </summary>
			public float? significantGain;

			/// <summary>
			/// A value that is considered a significant loss for the objective.
			/// The value can range from 0 to 1.
			/// </summary>
			public float? significantLoss;

			/// <summary>
			/// A value that is considered an insignificant loss for the objective.
			/// The value can range from 0 to 1.
			/// </summary>
			public float? insignificantLoss;

			/// <summary>
			/// A short description of the column.
			/// </summary>
			public string fullName;

			/// <summary>
			/// A long description of the column.
			/// </summary>
			public string description;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ColumnDefinition"/> class.
			/// </summary>
			public ColumnDefinition()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ColumnDefinition"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Column Definition.</param>
			public ColumnDefinition(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ColumnDefinition"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Column Definition.</param>
			public ColumnDefinition(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "key":
							this.key = value as string;
							break;
							
						case "type":
							this.type = value as string;
							break;
							
						case "goal":
							this.goal = value as string;
							break;
							
						case "is_objective":
							this.isObjective = Convert.ToBoolean(value);
							break;
							
						case "range":
							switch (jsonObject ["type"] as string)
							{
								case "text":
									break;

								case "numeric":
									this.range = new ValueRange(value as Dictionary<string, object>);
									break;

								case "datetime":
									this.range = new DateRange(value as Dictionary<string, object>);
									break;

								case "categorical":
									this.range = new CategoricalRange(value as List<object>);
									break;

								default:
									Debug.LogWarning("Unsupported Range Type key in json response: " + jsonObject ["type"]);
									break;
							}
							break;
						
						case "format":
							this.format = value as string;
							break;
							
						case "preference":
							List<object> preference = value as List<object>;							
							this.preference = new List<string>();						
							for (int i = 0; i < preference.Count; i++)
							{
								this.preference.Add(preference[i] as string);
							}
							break;
						
						case "significant_gain":
							this.significantGain = Convert.ToSingle(value);
							break;
							
						case "significant_loss":
							this.significantLoss = Convert.ToSingle(value);
							break;
							
						case "insignificant_loss":
							this.insignificantLoss = Convert.ToSingle(value);
							break;
							
						case "full_name":
							this.fullName = value as string;
							break;
							
						case "description":
							this.description = value as string;
							break;
							
						default:
							Debug.LogWarning("Unsupported Column Definition key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.ColumnDefinition"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.ColumnDefinition"/>.</returns>
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

				if (this.type != null)
				{
					stringBuilder.Append("\"type\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.type);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.goal != null)
				{
					stringBuilder.Append("\"goal\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.goal);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.isObjective != null)
				{
					stringBuilder.Append("\"is_objective\":");
					stringBuilder.Append(this.isObjective.ToString());
					stringBuilder.Append(",");
				}

				if (this.range != null)
				{
					stringBuilder.Append("\"range\":");
					stringBuilder.Append(this.range.ToString());
					stringBuilder.Append(",");
				}

				if (this.format != null)
				{
					stringBuilder.Append("\"format\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.format);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.preference != null && this.preference.Count > 0)
				{
					stringBuilder.Append("\"preference\":[");
					for (int i = 0; i < this.preference.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(this.preference[i]);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.significantGain != null)
				{
					stringBuilder.Append("\"significant_gain\":");
					stringBuilder.Append(this.significantGain.ToString());
					stringBuilder.Append(",");
				}

				if (this.significantLoss != null)
				{
					stringBuilder.Append("\"significant_loss\":");
					stringBuilder.Append(this.significantLoss.ToString());
					stringBuilder.Append(",");
				}

				if (this.insignificantLoss != null)
				{
					stringBuilder.Append("\"insignificant_loss\":");
					stringBuilder.Append(this.insignificantLoss.ToString());
					stringBuilder.Append(",");
				}

				if (this.fullName != null)
				{
					stringBuilder.Append("\"full_name\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.fullName);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.description != null)
				{
					stringBuilder.Append("\"description\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.description);
					stringBuilder.Append("\"");
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