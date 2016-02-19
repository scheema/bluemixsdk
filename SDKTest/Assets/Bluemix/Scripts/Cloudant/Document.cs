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
		public class Document
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
			/// The contents of the document retrieved from the database.
			/// </summary>
			public Dictionary<string, object> data;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Document"/> class.
			/// </summary>
			public Document()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Document"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Document.</param>
			public Document(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Document"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Document.</param>
			public Document(Dictionary<string, object> jsonObject)
			{
				this.data = new Dictionary<string, object>();

				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "_id":
							this.id = value as string;
							this.data.Add(key, value);
							break;

						case "_rev":
							this.rev = value as string;
							this.data.Add(key, value);
							break;

						default:
							this.data.Add(key, value);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.Document"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.Document"/>.</returns>
			public override string ToString()
			{
				return Json.Serialize(this.data);
			}
		}
	}
}