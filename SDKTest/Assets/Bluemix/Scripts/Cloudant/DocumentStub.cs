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
		/// Representation of a Document in IBM's Cloudant NoSQL Database service.
		/// </summary>
		public class DocumentStub
		{
			/// <summary>
			/// Unique id field.
			/// </summary>
			public string id;

			/// <summary>
			/// Revision number.
			/// </summary>
			public string rev;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentStub"/> class.
			/// </summary>
			public DocumentStub()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentStub"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Document Stub.</param>
			public DocumentStub(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.DocumentStub"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Document Stub.</param>
			public DocumentStub(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "id":
							this.id = value as string;
							break;

						case "rev":
							this.rev = value as string;
							break;

						case "ok":
							break;

						default:
							Debug.LogWarning("Unsupported Document key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.DocumentStub"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.DocumentStub"/>.</returns>
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

				if (this.rev != null)
				{
					stringBuilder.Append("\"rev\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.rev);
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