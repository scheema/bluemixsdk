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
		/// Content Item used as input to the Watson Personality Insights Profile service.
		/// </summary>
		public class ContentItem
		{
			/// <summary>
			/// Unique identifier for this content item (required).
			/// </summary>
			public string id;
			
			/// <summary>
			/// Unique identifier for the author of this content (required).
			/// </summary>
			public string userid;
			
			/// <summary>
			/// Identifier for the source of this content (required).
			/// </summary>
			public string sourceid;
			
			/// <summary>
			/// Timestamp that identifies when this content was created, in milliseconds since midnight 1/1/1970 UTC.
			/// </summary>
			public int? created;
			
			/// <summary>
			/// Timestamp that identifies when this content was last updated, in milliseconds since midnight 1/1/1970 UTC.
			/// </summary>
			public int? updated;
			
			/// <summary>
			/// MIME type of the content.
			/// </summary>
			public string contenttype;
			
			/// <summary>
			/// Language identifier (two-letter ISO 639-1 identifier) (required).
			/// </summary>
			public string language; //enum?
			
			/// <summary>
			/// Content to be analyzed.
			/// </summary>
			public string content;
			
			/// <summary>
			/// Unique id of the parent content item. Used to identify hierarchical relationships between posts/replies, messages/replies, etc.
			/// </summary>
			public string parentid;
			
			/// <summary>
			/// Indicates whether this content item is a reply to another content item.
			/// </summary>
			public bool? reply;
			
			/// <summary>
			/// Indicates whether this content item is a forwarded/copied version of another content item.
			/// </summary>
			public bool? forward;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentItem"/> class.
			/// </summary>
			public ContentItem()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentItem"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Content Item to be profiled.</param>
			public ContentItem(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{
				
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.PersonalityInsights.ContentItem"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string deserialized as a Dictionary representation of the Content Item to be profiled.</param>
			public ContentItem(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "id":
							this.id = value as string;
							break;
							
						case "userid":
							this.userid = value as string;
							break;
							
						case "sourceid":
							this.sourceid = value as string;
							break;
							
						case "created":
							this.created = Convert.ToInt32(value);
							break;
							
						case "updated":
							this.updated = Convert.ToInt32(value);
							break;
							
						case "contenttype":
							this.contenttype = value as string;
							break;
							
						case "language":
							this.language = value as string;
							break;
							
						case "content":
							this.content = value as string;
							break;
							
						case "parentid":
							this.parentid = value as string;
							break;
							
						case "reply":
							this.reply = Convert.ToBoolean(value);
							break;
							
						case "forward":
							this.forward = Convert.ToBoolean(value);
							break;
							
						default:
							Debug.LogWarning("Unsupported Content Item key in json response: " + key);
							break;
					}
				}
			}
			
			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.PersonalityInsights.ContentItem"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.PersonalityInsights.ContentItem"/>.</returns>
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
				
				if (this.userid != null)
				{
					stringBuilder.Append("\"userid\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.userid);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.sourceid != null)
				{
					stringBuilder.Append("\"sourceid\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.sourceid);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.created != null)
				{
					stringBuilder.Append("\"created\":");
					stringBuilder.Append(this.created.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.updated != null)
				{
					stringBuilder.Append("\"updated\":");
					stringBuilder.Append(this.updated.ToString());
					stringBuilder.Append(",");
				}
				
				if (this.contenttype != null)
				{
					stringBuilder.Append("\"contenttype\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.contenttype);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.language != null)
				{
					stringBuilder.Append("\"language\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.language);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.contenttype != null)
				{
					stringBuilder.Append("\"content\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.content);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.parentid != null)
				{
					stringBuilder.Append("\"parentid\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.parentid);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}
				
				if (this.reply != null)
				{
					stringBuilder.Append("\"reply\":");
					stringBuilder.Append(this.reply.ToString());
					stringBuilder.Append(",");
				}	
				
				if (this.forward != null)
				{
					stringBuilder.Append("\"forward\":");
					stringBuilder.Append(this.forward.ToString());
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