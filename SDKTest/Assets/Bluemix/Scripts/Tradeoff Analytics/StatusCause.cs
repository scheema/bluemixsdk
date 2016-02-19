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
		/// Status cause returned as part of the Watson Tradeoff Analytics Dilemmas service.
		/// </summary>
		public class StatusCause
		{
			/// <summary>
			/// A textual description of the cause for the option's status.
			/// </summary>
			public string message;

			/// <summary>
			/// List of values used to describe the cause for the option's status.
			/// </summary>
			public List<string> tokens;

			/// <summary>
			/// An error code that specifies the cause of the option's status.
			/// </summary>
			public string errorCode; //enum?

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.StatusCause"/> class.
			/// </summary>
			public StatusCause()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.StatusCause"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Status Cause.</param>
			public StatusCause(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.StatusCause"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Status Cause.</param>
			public StatusCause(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "message":
							this.message = value as string;
							break;

						case "tokens":
							List<object> tokens = value as List<object>;							
							this.tokens = new List<string>();						
							for (int i = 0; i < tokens.Count; i++)
							{
								this.tokens.Add(tokens[i] as string);
							}
							break;

						case "error_code":
							this.errorCode = value as string;
							break;

						default:
							Debug.LogWarning("Unsupported Status Cause key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.StatusCause"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.StatusCause"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.message != null)
				{
					stringBuilder.Append("\"message\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.message);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.tokens != null && this.tokens.Count > 0)
				{
					stringBuilder.Append("\"tokens\":[");
					for (int i = 0; i < this.tokens.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(this.tokens[i]);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.errorCode != null)
				{
					stringBuilder.Append("\"error_code\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.errorCode);
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