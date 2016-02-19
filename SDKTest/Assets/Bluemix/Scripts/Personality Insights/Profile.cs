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
		/// Profile returned from the Watson Personality Insights Profile service.
		/// </summary>
		public class Profile
		{
			/// <summary>
			/// The unique identifier for which these characteristics were computed, from the "userid" field of the input Content Items.
			/// </summary>
			public string id;
			
			/// <summary>
			/// The source for which these characteristics were computed, from the "sourceid" field of the input Content Items.
			/// </summary>
			public string source;
			
			/// <summary>
			/// The number of words found in the input.
			/// </summary>
			public int? wordCount;
			
			/// <summary>
			/// A message indicating the number of words found and where that value falls in the range of required or suggested number of words when guidance is available.
			/// </summary>
			public string wordCountMessage;
			
			/// <summary>
			/// The language model that was used to process the input.
			/// </summary>
			public string processedLang; //enum?
			
			/// <summary>
			/// The trait model.
			/// </summary>
			public Trait tree;
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Profile"/> class.
			/// </summary>
			public Profile()
			{
			
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Profile"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Profile.</param>
			public Profile(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{
			
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.Profile"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Profile.</param>
			public Profile(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "id":
							this.id = value as string;
							break;
							
						case "source":
							this.source = value as string;
							break;
							
						case "word_count":
							this.wordCount = Convert.ToInt32(value);
							break;
							
						case "word_count_message":
							this.wordCountMessage = value as string;
							break;
							
						case "processed_lang":
							this.processedLang = value as string;
							break;
							
						case "tree":
							this.tree = new Trait(value as Dictionary<string, object>);
							break;
							
						default:
							Debug.LogWarning("Unsupported Profile key in json response: " + key);
							break;
					}
				}
			}
			
			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.PersonalityInsights.Profile"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.PersonalityInsights.Profile"/>.</returns>
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
				
				if (this.source != null)
				{
					stringBuilder.Append("\"source\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.source);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.wordCount != null)
				{
					stringBuilder.Append("\"word_count\":");
					stringBuilder.Append(this.wordCount.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.wordCountMessage != null)
				{
					stringBuilder.Append("\"word_count_message\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.wordCountMessage);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.processedLang != null)
				{
					stringBuilder.Append("\"processed_lang\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.processedLang);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.tree != null)
				{
					stringBuilder.Append("\"tree\":");
					stringBuilder.Append(this.tree.ToString());
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