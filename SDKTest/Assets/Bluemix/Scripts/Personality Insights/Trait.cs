using MiniJSON;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bluemix
{
	namespace PersonalityInsights
	{
		/// <summary>
		/// The Trait model, returned as part of the Profile returned from the Watson Personality Insights Profile service.
		/// </summary>
		public class Trait
		{
			/// <summary>
			/// The id of the characteristic, globally unique.
			/// </summary>
			public string id;
			
			/// <summary>
			/// The user-displayable name of the characteristic.
			/// </summary>
			public string name;
			
			/// <summary>
			/// The category of the characteristic: personality, needs, values, or behavior (for temporal data).
			/// </summary>
			public string category;
			
			/// <summary>
			/// For personality, needs, and values characterisitics, the normalized percentile score for the characteristic, from 0-1.
			/// For temporal behavior characteristics, the percentage of timestamped data that occurred during that day or hour.
			/// </summary>
			public float? percentage;
			
			/// <summary>
			/// Indicates the sampling error of the percentage based on the number of words in the input, from 0-1.
			/// The number defines a 95% confidence interval around the percentage.
			/// </summary>
			public float? samplingError;
			
			/// <summary>
			/// For personality, needs, and values characterisitics, the raw score for the characteristic.
			/// A positive or negative score indicates more or less of the characteristic; zero indicates neutrality or no evidence for a score.
			/// </summary>
			public float? rawScore;
			
			/// <summary>
			/// Indicates the sampling error of the raw score based on the number of words in the input; the practical range is 0-1.
			/// The number defines a 95% confidence interval around the raw score.
			/// </summary>
			public float? rawSamplingError;
			
			/// <summary>
			/// Recursive list of characteristics inferred from the input text.
			/// </summary>
			public List<Trait> children;
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Trait"/> class.
			/// </summary>
			public Trait()
			{
			
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Trait"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Trait.</param>
			public Trait(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{
			
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Trait"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Trait.</param>
			public Trait(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "id":
							this.id = value as string;
							break;
							
						case "name":
							this.name = value as string;
							break;
							
						case "category":
							this.category = value as string;
							break;
						
						case "percentage":
							this.percentage = Convert.ToSingle(value);
							break;
							
						case "sampling_error":
							this.samplingError = Convert.ToSingle(value);
							break;
							
						case "raw_score":
							this.rawScore = Convert.ToSingle(value);
							break;
							
						case "raw_sampling_error":
							this.rawSamplingError = Convert.ToSingle(value);
							break;
							
						case "children":
							List<object> children = value as List<object>;							
							this.children = new List<Trait>();					
							for (int i = 0; i < children.Count; i++)
							{
								this.children.Add(new Trait(children[i] as Dictionary<string, object>));
							}
							break;
							
						default:
							Debug.LogWarning("Unsupported Trait key in json response: " + key);
							break;
					}
				}
			}
			
			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.PersonalityInsights.Trait"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.PersonalityInsights.Trait"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				
				if (this.id != null)
				{
					stringBuilder.Append("\"id\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.id);
					stringBuilder.Append("\"");
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
				
				if (this.category != null)
				{
					stringBuilder.Append("\"category\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.category);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.percentage != null)
				{
					stringBuilder.Append("\"percentage\":");
					stringBuilder.Append(this.percentage.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.samplingError != null)
				{
					stringBuilder.Append("\"sampling_error\":");
					stringBuilder.Append(this.samplingError.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.rawScore != null)
				{
					stringBuilder.Append("\"raw_score\":");
					stringBuilder.Append(this.rawScore.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.rawSamplingError != null)
				{
					stringBuilder.Append("\"raw_sampling_error\":");
					stringBuilder.Append(this.rawSamplingError.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.children != null && this.children.Count > 0)
				{
					stringBuilder.Append("\"children\":[");
					for (int i = 0; i < this.children.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.children[i].ToString());
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