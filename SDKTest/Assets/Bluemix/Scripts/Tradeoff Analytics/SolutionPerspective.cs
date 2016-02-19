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
		/// Solution Perspective returned as part of the Watson Tradeoff Analytics Dilemmas service.
		/// </summary>
		public class SolutionPerspective
		{
			/// <summary>
			/// The key that uniquely identifies the option in the decision problem.
			/// </summary>
			public string solutionRef;

			/// <summary>
			/// The status of the option for the problem resolution.
			/// </summary>
			public string status; //enum?

			/// <summary>
			/// If the status of the option is incomplete or does_not_meet_preference, this provides more information about the cause of the status.
			/// </summary>
			public StatusCause statusCause;

			/// <summary>
			/// A list of references to the keys of solutions that are shadowed by this solution.
			/// </summary>
			public List<string> shadows;

			/// <summary>
			/// A list of references to the keys of solutions that shadow this solution.
			/// </summary>
			public List<string> shadowMe;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.SolutionPerspective"/> class.
			/// </summary>
			public SolutionPerspective()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.SolutionPerspective"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Solution Perspective.</param>
			public SolutionPerspective(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.SolutionPerspective"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Solution Perspective.</param>
			public SolutionPerspective(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "solution_ref":
							this.solutionRef = value as string;
							break;

						case "status":
							this.status = value as string;
							break;

						case "status_cause":
							this.statusCause = new StatusCause(value as Dictionary<string, object>);
							break;

						case "shadows":
							List<object> shadows = value as List<object>;							
							this.shadows = new List<string>();						
							for (int i = 0; i < shadows.Count; i++)
							{
								this.shadows.Add(shadows[i] as string);
							}
							break;

						case "shadow_me":
							List<object> shadowMe = value as List<object>;							
							this.shadowMe = new List<string>();						
							for (int i = 0; i < shadowMe.Count; i++)
							{
								this.shadowMe.Add(shadowMe[i] as string);
							}
							break;

						default:
							Debug.LogWarning("Unsupported Solution Perspective key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.SolutionPerspective"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.SolutionPerspective"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.solutionRef != null)
				{
					stringBuilder.Append("\"solution_ref\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.solutionRef);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.status != null)
				{
					stringBuilder.Append("\"status\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.status);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.statusCause != null)
				{
					stringBuilder.Append("\"status_cause\":");
					stringBuilder.Append(this.statusCause.ToString());
					stringBuilder.Append(",");
				}

				if (this.shadows != null && this.shadows.Count > 0)
				{
					stringBuilder.Append("\"shadows\":[");
					for (int i = 0; i < this.shadows.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(this.shadows[i]);
						stringBuilder.Append("\"");
					}
					stringBuilder.Append("]");
					stringBuilder.Append(",");
				}

				if (this.shadowMe != null && this.shadowMe.Count > 0)
				{
					stringBuilder.Append("\"shadow_me\":[");
					for (int i = 0; i < this.shadowMe.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(this.shadowMe[i]);
						stringBuilder.Append("\"");
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