using MiniJSON;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bluemix
{
	namespace Cloudant
	{
		/// <summary>
		/// A document change entry, returned as the result of a query to get changes in a IBM Cloudant Database.
		/// </summary>
		public class DocumentChange
		{
			/// <summary>
			/// List of changes made to the specific document.
			/// </summary>
			public List<string> changes;

			/// <summary>
			/// Boolean indicating if the corresponding document was deleted.
			/// If present, it always has the value true.
			/// </summary>
			public bool? deleted;

			/// <summary>
			/// Document identifier.
			/// </summary>
			public string id;

			/// <summary>
			/// Update sequence identifier.
			/// </summary>
			public string seq;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentChange"/> class.
			/// </summary>
			public DocumentChange()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentChange"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Document Change.</param>
			public DocumentChange(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentChange"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Document Change.</param>
			public DocumentChange(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "changes":
							List<object> changes = value as List<object>;
							this.changes = new List<string>();							
							for (int i = 0; i < changes.Count; i++)
							{
								this.changes.Add((changes[i] as Dictionary<string, object>)["rev"] as string);
							}
							break;

						case "deleted":
							this.deleted = Convert.ToBoolean(value);
							break;

						case "id":
							this.id = value as string;
							break;

						case "seq":
							this.seq = value as string;
							break;

						default:
							Debug.LogWarning("Unsupported Document Change key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.DocumentChange"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.DocumentChange"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.changes != null && this.changes.Count > 0)
				{
					stringBuilder.Append("\"changes\":[");
					for (int i = 0; i < this.changes.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("{\"rev\":");
						stringBuilder.Append("\"");
						stringBuilder.Append(this.changes[i]);
						stringBuilder.Append("\"");
						stringBuilder.Append("}");
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.deleted != null)
				{
					stringBuilder.Append("\"deleted\":");
					stringBuilder.Append(this.deleted.ToString());
					stringBuilder.Append(",");
				}

				if (this.id != null)
				{
					stringBuilder.Append("\"id\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.id);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.seq != null)
				{
					stringBuilder.Append("\"seq\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.seq);
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