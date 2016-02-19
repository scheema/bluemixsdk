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
		/// Representation of a Database in IBM's Cloudant NoSQL Database service.
		/// </summary>
		public class Database
		{
			/// <summary>
			/// Set to true if the database compaction routine is operating on this database.
			/// </summary>
			public bool? compactRunning;

			/// <summary>
			/// The name of the db.
			/// </summary>
			public string dbName;

			/// <summary>
			/// The version of the physical format used for the data when it is stored on disk.
			/// </summary>
			public int? diskFormatVersion;

			/// <summary>
			/// Size in bytes of the data as stored on the disk.
			/// </summary>
			public int? diskSize;

			/// <summary>
			/// A count of the documents in the specified database.
			/// </summary>
			public int? docCount;

			/// <summary>
			/// Number of deleted documents.
			/// </summary>
			public int? docDelCount;

			/// <summary>
			/// Instance start time (should always be 0).
			/// </summary>
			public string instanceStartTime;

			/// <summary>
			/// The number of purge operations on the database.
			/// </summary>
			public int? purgeSeq;

			/// <summary>
			/// An opaque string describing the state of the database.
			/// </summary>
			public string updateSeq;

			/// <summary>
			/// Data size.
			/// </summary>
			public int? dataSize;

			/// <summary>
			/// File size.
			/// </summary>
			public int? fileSize;

			/// <summary>
			/// External size.
			/// </summary>
			public int? externalSize;

			/// <summary>
			/// Active size.
			/// </summary>
			public int? activeSize;

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Database"/> class.
			/// </summary>
			public Database()
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Database"/> class.
			/// </summary>
			/// <param name="jsonString">JSON string representation of the Database.</param>
			public Database(string jsonString) : this(Json.Deserialize(jsonString) as Dictionary<string, object>)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Bluemix.Cloudant.Database"/> class.
			/// </summary>
			/// <param name="jsonObject">JSON string deserialized as a Dictionary representation of the Database.</param>
			public Database(Dictionary<string, object> jsonObject)
			{
				foreach (string key in jsonObject.Keys)
				{
					object value = jsonObject[key];
					
					switch (key)
					{
						case "compact_running":
							this.compactRunning = Convert.ToBoolean(value);
							break;

						case "db_name":
							this.dbName = value as string;
							break;

						case "disk_format_version":
							this.diskFormatVersion = Convert.ToInt32(value);
							break;

						case "disk_size":
							this.diskSize = Convert.ToInt32(value);
							break;

						case "doc_count":
							this.docCount = Convert.ToInt32(value);
							break;

						case "doc_del_count":
							this.docDelCount = Convert.ToInt32(value);
							break;

						case "instance_start_time":
							this.instanceStartTime = value as string;
							break;

						case "purge_seq":
							this.purgeSeq = Convert.ToInt32(value);
							break;

						case "update_seq":
							this.updateSeq = value as string;
							break;

						case "other":
							Dictionary<string, object> other = value as Dictionary<string, object>;
							this.dataSize = Convert.ToInt32(other["data_size"]);
							break;

						case "sizes":
							Dictionary<string, object> sizes = value as Dictionary<string, object>;
							this.fileSize = Convert.ToInt32(sizes["file"]);
							this.externalSize = Convert.ToInt32(sizes["external"]);
							this.activeSize = Convert.ToInt32(sizes["active"]);
							break;
							
						default:
							Debug.LogWarning("Unsupported Database key in json response: " + key);
							break;
					}
				}
			}

			/// <summary>
			/// Returns a JSON string representation of the <see cref="Bluemix.Cloudant.Database"/>.
			/// </summary>
			/// <returns>A JSON string representation of the <see cref="Bluemix.Cloudant.Database"/>.</returns>
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");

				if (this.compactRunning != null)
				{
					stringBuilder.Append("\"compact_running\":");
					stringBuilder.Append(this.compactRunning.ToString());
					stringBuilder.Append(",");
				}

				if (this.dbName != null)
				{
					stringBuilder.Append("\"db_name\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.dbName);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.diskFormatVersion != null)
				{
					stringBuilder.Append("\"disk_format_version\":");
					stringBuilder.Append(this.diskFormatVersion.ToString());
					stringBuilder.Append(",");
				}

				if (this.diskSize != null)
				{
					stringBuilder.Append("\"disk_size\":");
					stringBuilder.Append(this.diskSize.ToString());
					stringBuilder.Append(",");
				}

				if (this.docCount != null)
				{
					stringBuilder.Append("\"doc_count\":");
					stringBuilder.Append(this.docCount.ToString());
					stringBuilder.Append(",");
				}

				if (this.docDelCount != null)
				{
					stringBuilder.Append("\"doc_del_count\":");
					stringBuilder.Append(this.docDelCount.ToString());
					stringBuilder.Append(",");
				}

				if (this.instanceStartTime != null)
				{
					stringBuilder.Append("\"instance_start_time\":");
					stringBuilder.Append("\"");
					stringBuilder.Append(this.instanceStartTime);
					stringBuilder.Append("\"");
					stringBuilder.Append(",");
				}

				if (this.purgeSeq != null)
				{
					stringBuilder.Append("\"purge_seq\":");
					stringBuilder.Append(this.purgeSeq.ToString());
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

				if (this.dataSize != null)
				{
					stringBuilder.Append("\"other\":{\"data_size\":");
					stringBuilder.Append(this.dataSize.ToString());
					stringBuilder.Append("},");
				}

				if (this.fileSize != null || this.externalSize != null || this.activeSize != null)
				{
					stringBuilder.Append("\"sizes\":{");

					if (this.fileSize != null)
					{
						stringBuilder.Append("\"file\":");
						stringBuilder.Append(this.fileSize.ToString());
						stringBuilder.Append(",");
					}

					if (this.externalSize != null)
					{
						stringBuilder.Append("\"external\":");
						stringBuilder.Append(this.externalSize.ToString());
						stringBuilder.Append(",");
					}

					if (this.activeSize != null)
					{
						stringBuilder.Append("\"active\":");
						stringBuilder.Append(this.activeSize.ToString());
						stringBuilder.Append(",");
					}

					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					stringBuilder.Append("},");
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