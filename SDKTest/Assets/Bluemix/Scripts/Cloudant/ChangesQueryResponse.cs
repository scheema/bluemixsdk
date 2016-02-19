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
		/// Response to a query to get changes from a IBM Cloudant Database.
		/// </summary>
		public class ChangesQueryResponse
		{
			/// <summary>
			/// Identifier of the last of the sequence identifiers.
			/// </summary>
			public string lastSeq;

			/// <summary>
			/// Pending changes.
			/// </summary>
			public int? pending;

			/// <summary>
			/// Array of changes made to the database.
			/// </summary>
			public List<DocumentChange> results;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.ChangesQueryResponse"/> class.
			/// </summary>
			public ChangesQueryResponse()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.ChangesQueryResponse"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Changes Query Response.</param>
			public ChangesQueryResponse(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.ChangesQueryResponse"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Changes Query Response.</param>
			public ChangesQueryResponse(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "last_seq":
							this.lastSeq = value as string;
							break;

						case "pending":
							this.pending = Convert.ToInt32(value);
							break;

						case "results":
							List<object> results = value as List<object>;
							this.results = new List<DocumentChange>();							
							for (int i = 0; i < results.Count; i++)
							{
								this.results.Add(new DocumentChange(results[i] as Dictionary<string, object>));
							}
							break;

						default:
							Debug.LogWarning("Unsupported Changes Query Response key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.ChangesQueryResponse"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.ChangesQueryResponse"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.lastSeq != null)
				{
					stringBuilder.Append("\"last_seq\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.lastSeq);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.pending != null)
				{
					stringBuilder.Append("\"pending\":");
					stringBuilder.Append(this.pending.ToString());
					stringBuilder.Append(",");
				}

				if (this.results != null && this.results.Count > 0)
				{
					stringBuilder.Append("\"results\":[");
					for (int i = 0; i < this.results.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.results[i].ToString());
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