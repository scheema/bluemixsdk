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
		/// Value range for the Watson Tradeoff Analytics Column Definition object.
		/// </summary>
		public class ValueRange : IRange
		{
			/// <summary>
			/// Maximum value.
			/// </summary>
			public double? high;

			/// <summary>
			/// Minimum value.
			/// </summary>
			public double? low;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ValueRange"/> class.
			/// </summary>
			public ValueRange()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ValueRange"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Value Range.</param>
			public ValueRange(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.ValueRange"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Value Range.</param>
			public ValueRange(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];

					switch (key)
					{
						case "high":
							this.high = Convert.ToDouble(value);
							break;

						case "low":
							this.low = Convert.ToDouble(value);
							break;

						default:
							Debug.LogWarning("Unsupported Value Range key in json response: " + key);
							break;						
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.ValueRange"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.ValueRange"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.high != null)
				{
					stringBuilder.Append("\"high\":");
					stringBuilder.Append(this.high.ToString());
					stringBuilder.Append(",");
				}

				if (this.low != null)
				{
					stringBuilder.Append("\"low\":");
					stringBuilder.Append(this.low.ToString());
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