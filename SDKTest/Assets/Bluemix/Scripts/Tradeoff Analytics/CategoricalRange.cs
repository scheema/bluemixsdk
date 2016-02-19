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
		/// Categorical range for the Watson Tradeoff Analytics Column Definition object.
		/// </summary>
		public class CategoricalRange : IRange
		{
			/// <summary>
			/// Ordered list of keys that are valid for the column.
			/// </summary>
			public List<string> keys;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.CategoricalRange"/> class.
			/// </summary>
			public CategoricalRange()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.CategoricalRange"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Categorical Range.</param>
			public CategoricalRange(string jsonString) : this(Json.Deserialize(jsonString) as List<object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.TradeoffAnalytics.CategoricalRange"/> class.
			/// </summary>
			/// <param name="jsonObject">Json object.</param>
			public CategoricalRange(List<object> jsonObject)
			{
				this.keys = new List<string>();
				foreach (object key in jsonObject)
				{
					this.keys.Add(key as string);
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.CategoricalRange"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.TradeoffAnalytics.CategoricalRange"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.keys != null && this.keys.Count > 0)
				{
					stringBuilder.Append("\"preference\":[");
					for (int i = 0; i < this.keys.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append("\"");
						stringBuilder.Append(this.keys[i]);
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