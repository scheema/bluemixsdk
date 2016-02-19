using MiniJSON;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bluemix
{
	namespace PersonalityInsights
	{
		/// <summary>
		/// Content List Container of Content Items used as input to the Watson Personality Insights Profile service.
		/// </summary>
		public class ContentListContainer
		{
			/// <summary>
			/// The list of Content Items to be profiled.
			/// </summary>
			public List<ContentItem> contentItems;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentListContainer"/> class.
			/// </summary>
			public ContentListContainer()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentListContainer"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Content List Container to be profiled.</param>
			public ContentListContainer(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{
								
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentListContainer"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation the Content List Container to be profiled.</param>
			public ContentListContainer(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "contentItems":
							List<object> contentItems = value as List<object>;							
							this.contentItems = new List<ContentItem>();							
							for (int i = 0; i < contentItems.Count; i++)
							{
								this.contentItems.Add(new ContentItem(contentItems[i] as Dictionary<string, object>));
							}
							break;
							
						default:
							Debug.LogWarning("Unsupported Content List Container key in json response: " + key);
							break;
					}
				}
			}
			
			/// <summary>
			/// Adds a Content Item to this Content List Container.
			/// </summary>
			/// <param name="contentItem">Content item.</param>
			public void AddContentItem(ContentItem contentItem)
			{
				if (this.contentItems == null)
				{
					this.contentItems = new List<ContentItem>();
				}
				
				this.contentItems.Add(contentItem);
			}
			
			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.PersonalityInsights.ContentListContainer"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.PersonalityInsights.ContentListContainer"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				
				if (this.contentItems != null && this.contentItems.Count > 0)
				{
					stringBuilder.Append("\"contentItems\":[");
					for (int i = 0; i < this.contentItems.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.contentItems[i].ToString());
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