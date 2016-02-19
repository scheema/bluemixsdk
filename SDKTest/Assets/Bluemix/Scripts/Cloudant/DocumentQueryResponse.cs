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
		/// Response to a query to get Documents from a IBM Cloudant Database.
		/// </summary>
		public class DocumentQueryResponse
		{
			/// <summary>
			/// Offset where the document list started.
			/// </summary>
			public int? offset;

			/// <summary>
			/// Array of Document Stub objects.
			/// </summary>
			public List<DocumentStub> rows;

			/// <summary>
			/// Number of documents in the database/view matching the parameters of the query.
			/// </summary>
			public int? totalRows;

			/// <summary>
			/// Current update sequence for the database.
			/// </summary>
			public string updateSeq;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentQueryResponse"/> class.
			/// </summary>
			public DocumentQueryResponse()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentQueryResponse"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Document Query Response.</param>
			public DocumentQueryResponse(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentQueryResponse"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Document Query Response.</param>
			public DocumentQueryResponse(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "offset":
							this.offset = Convert.ToInt32(value);
							break;

						case "rows":
							List<object> rows = value as List<object>;
							this.rows = new List<DocumentStub>();							
							for (int i = 0; i < rows.Count; i++)
							{
								Dictionary<string, object> row = rows[i] as Dictionary<string, object>;
								DocumentStub d = new DocumentStub();
								d.id = row["id"] as string;
								d.rev = (row["value"] as Dictionary<string, object>)["rev"] as string;
								this.rows.Add(d);
							}
							break;

						case "total_rows":
							this.totalRows = Convert.ToInt32(value);
							break;

						case "update_seq":
							this.updateSeq = value as string;
							break;

						default:
							Debug.LogWarning("Unsupported Document Query Response key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.DocumentQueryResponse"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.DocumentQueryResponse"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.offset != null)
				{
					stringBuilder.Append("\"offset\":");
					stringBuilder.Append(this.offset.ToString());
					stringBuilder.Append(",");
				}

				if (this.rows != null && this.rows.Count > 0)
				{
					stringBuilder.Append("\"rows\":[");
					for (int i = 0; i < this.rows.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.rows[i].ToString());
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.totalRows != null)
				{
					stringBuilder.Append("\"total_rows\":");
					stringBuilder.Append(this.totalRows.ToString());
					stringBuilder.Append(",");
				}

				if (this.updateSeq != null)
				{
					stringBuilder.Append("\"update_seq\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.updateSeq);
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