using MiniJSON;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bluemix
{
	namespace Cloudant
	{
		/// <summary>
		/// Provides Database utilities to connect and interact with IBM's Cloudant NoSQL Database service.
		/// </summary>
		public static partial class Cloudant
		{							
			/// <summary>
			/// Validates a database name.
			/// </summary>
			/// <returns><c>true</c>, if database name is valid, <c>false</c> otherwise.</returns>
			/// <param name="databaseName">Database name.</param>
			private static bool ValidateDatabaseName(string databaseName)
			{
				return databaseName.Length <= 128 && Regex.IsMatch(databaseName, @"^[a-z][a-z0-9_$()+\-\/]+$");
			}
			
			/// <summary>
			/// Creates a database with the specified name.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void CreateDatabase(string databaseName, Action<Database, Exception> callback)
			{		
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.PUT(BluemixConnection.Instance.appURL + "/api/cloudant/create_database", postData, headers, (WWW www) =>
				{						
					Database db = null;
					Exception e = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);					
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						db = new Database();
						db.dbName = databaseName;
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(db, e);
					}
				}));
			}

			/// <summary>
			/// Reads details about the specified Database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void ReadDatabase(string databaseName, Action<Database, Exception> callback)
			{
				//Build the url string from the app path and query parameters
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(BluemixConnection.Instance.appURL);
				stringBuilder.Append("/api/cloudant/read_database?dbName=");
				stringBuilder.Append(databaseName);
				string url = stringBuilder.ToString();	

				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(url, null, null, (WWW www) =>
				{						
					Database db = null;
					Exception e = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);						
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						db = new Database(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(db, e);
					}
				}));
			}

			/// <summary>
			/// Lists all databases for this account.
			/// </summary>
			/// <param name="callback">Request complete callback.</param>
			public static void GetDatabases(Action<List<Database>, Exception> callback)
			{				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(BluemixConnection.Instance.appURL + "/api/cloudant/get_databases", null, null, (WWW www) =>
				{
					List<Database> dbs = null;
					Exception e = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						List<object> jsonObject = Json.Deserialize(www.text) as List<object>;
						dbs = new List<Database>();
						foreach (object o in jsonObject)
						{
							Database db = new Database();
							db.dbName = o as string;
							dbs.Add(db);
						}
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(dbs, e);
					}
				}));
			}

			/// <summary>
			/// Gets the documents in the database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void GetDocumentsInDatabase(string databaseName, Action<DocumentQueryResponse, Exception> callback)
			{
				//Build the url string from the app path and query parameters
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(BluemixConnection.Instance.appURL);
				stringBuilder.Append("/api/cloudant/get_documents_in_database?dbName=");
				stringBuilder.Append(databaseName);
				string url = stringBuilder.ToString();	

				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(url, null, null, (WWW www) =>
				{												
					DocumentQueryResponse dqr = null;
					Exception e = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						dqr = new DocumentQueryResponse(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(dqr, e);
					}
				}));
			}

			/// <summary>
			/// Gets the changes to documents in the database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void GetChangesInDatabase(string databaseName, Action<ChangesQueryResponse, Exception> callback)
			{
				//Build the url string from the username and all dbs path
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(BluemixConnection.Instance.appURL);
				stringBuilder.Append("/api/cloudant/get_changes_in_database?dbName=");
				stringBuilder.Append(databaseName);
				string url = stringBuilder.ToString();	
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(url, null, null, (WWW www) =>
				{						
					Exception e = null;
					ChangesQueryResponse cqr = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);						
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						Dictionary<string, object> jsonObject = Json.Deserialize(www.text) as Dictionary<string, object>;
						cqr = new ChangesQueryResponse(jsonObject);
						Debug.Log(www.text);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(cqr, e);
					}
				}));
			}
			
			/// <summary>
			/// Deletes the database with name databaseName.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="authenticationCookie">Authentication cookie.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void DeleteDatabase(string databaseName, Action<Database, Exception> callback)
			{
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.DELETE(BluemixConnection.Instance.appURL + "/api/cloudant/delete_database", postData, headers, (WWW www) =>
				{						
					Database db = null;
					Exception e = null;

					//Check for errors in the response
					if (!string.IsNullOrEmpty(www.error))
					{
						StringBuilder messageBuilder = new StringBuilder();
						messageBuilder.Append(www.error);					
						e = new CloudantException(messageBuilder.ToString());
					}
					else
					{
						db = new Database();
						db.dbName = databaseName;
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(db, e);
					}
				}));
			}
		}
	}
}