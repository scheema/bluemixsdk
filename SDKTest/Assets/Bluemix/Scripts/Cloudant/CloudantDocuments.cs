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
		/// Provides Document utilities to connect and interact with IBM's Cloudant NoSQL Database service.
		/// </summary>
		public static partial class Cloudant
		{							
			/// <summary>
			/// Creates a document in the specified database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="jsonDocument">Json document.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void CreateDocument(string databaseName, Document document, Action<DocumentStub, Exception> callback)
			{
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);
				data.Add("jsonDocument", document.ToString());				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.POST(BluemixConnection.Instance.appURL + "/api/cloudant/create_document", postData, headers, (WWW www) =>
				{						
					DocumentStub ds = null;
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
						ds = new DocumentStub(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(ds, e);
					}
				}));
			}

			/// <summary>
			/// Reads the document with the specified id from the specified database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="documentID">Document ID.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void ReadDocument(string databaseName, string documentID, Action<Document, Exception> callback)
			{
				//Build the url string from the app path and query parameters
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(BluemixConnection.Instance.appURL);
				stringBuilder.Append("/api/cloudant/read_document?dbName=");
				stringBuilder.Append(databaseName);
				stringBuilder.Append("&documentID=");
				stringBuilder.Append(documentID);
				string url = stringBuilder.ToString();
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.GET(url, null, null, (WWW www) =>
				{						
					Document d = null;
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
						d = new Document(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(d, e);
					}
				}));
			}

			/// <summary>
			/// Updates the specified document in the specified database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="document">Document to be updated.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void UpdateDocument(string databaseName, Document document, Action<DocumentStub, Exception> callback)
			{
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);
				data.Add("jsonDocument", document.ToString());				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.PUT(BluemixConnection.Instance.appURL + "/api/cloudant/update_document", postData, headers, (WWW www) =>
				{						
					DocumentStub ds = null;
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
						ds = new DocumentStub(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(ds, e);
					}
				}));
			}
			
			/// <summary>
			/// Deletes the document from the specified database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="document">Document to be deleted.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void DeleteDocument(string databaseName, Document document, Action<DocumentStub, Exception> callback)
			{				
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);
				data.Add("documentID", document.id);
				data.Add("documentRev", document.rev);				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));
				
				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");
				
				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.DELETE(BluemixConnection.Instance.appURL + "/api/cloudant/delete_document", postData, headers, (WWW www) =>
				{						
					DocumentStub ds = null;
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
						ds = new DocumentStub(jsonObject);
					}
					
					//Call the request complete callback
					if (callback != null)
					{
						callback(ds, e);
					}
				}));
			}

			/// <summary>
			/// Deletes the document with information in the specified Document Stub from the specified database.
			/// </summary>
			/// <param name="databaseName">Database name.</param>
			/// <param name="documentStub">Document stub containing information for the document to be deleted.</param>
			/// <param name="callback">Request complete callback.</param>
			public static void DeleteDocument(string databaseName, DocumentStub documentStub, Action<DocumentStub, Exception> callback)
			{				
				//Add the form parameters for the request
				Dictionary<string, string> data = new Dictionary<string, string>();
				data.Add("dbName", databaseName);
				data.Add("documentID", documentStub.id);
				data.Add("documentRev", documentStub.rev);				
				byte[] postData = Encoding.UTF8.GetBytes(Json.Serialize(data));

				//Add the headers for the request
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type", "application/json");

				//Submit the request asynchronously
				BluemixConnection.Instance.StartCoroutine(REST.DELETE(BluemixConnection.Instance.appURL + "/api/cloudant/delete_document", postData, headers, (WWW www) =>
				{						
					DocumentStub ds = null;
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
						ds = new DocumentStub(jsonObject);
					}

					//Call the request complete callback
					if (callback != null)
					{
						callback(ds, e);
					}
				}));
			}
		}
	}
}