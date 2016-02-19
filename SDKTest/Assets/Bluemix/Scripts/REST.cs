using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Execute static asynchronous RESTful API calls using Unity's WWW class.
/// </summary>
public class REST
{		
	/// <summary>
	/// Build a header for basic authentication.
	/// </summary>
	/// <returns>The authentication header value.</returns>
	/// <param name="username">Credentials username.</param>
	/// <param name="password">Credentials password.</param>
	public static string BasicAuthenticationHeader(string username, string password)
	{
		//Concatenate the username, a colon, and password into a single string
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(username);
		stringBuilder.Append(":");
		stringBuilder.Append(password);
		string value = stringBuilder.ToString();
		
		//Encode the concatenated string into a base64 string
		byte[] bytes = Encoding.UTF8.GetBytes(value);
		value = System.Convert.ToBase64String(bytes);
		
		//Add the authentication scheme type to the encoded value
		stringBuilder.Remove(0, stringBuilder.Length);
		stringBuilder.Append("Basic ");
		stringBuilder.Append(value);
		value = stringBuilder.ToString();
		
		//Return the header used for basic authentication on each request
		return value;
	}
	
	public static IEnumerator GET(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{
		if (data != null)
		{
			data = null;
			Debug.LogWarning("GET requests should not include post data; setting the post data to null");
		}
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	public static IEnumerator HEAD(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{
		data = data ?? new Byte[1];
		
		headers = headers ?? new Dictionary<string, string>();
		headers.Add("X-HTTP-Method-Override", "HEAD");
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	public static IEnumerator POST(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{		
		data = data ?? new Byte[1];
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	public static IEnumerator PUT(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{
		data = data ?? new Byte[1];
		
		headers = headers ?? new Dictionary<string, string>();
		headers.Add("X-HTTP-Method-Override", "PUT");
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	public static IEnumerator DELETE(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{		
		data = data ?? new Byte[1];
		
		headers = headers ?? new Dictionary<string, string>();
		headers.Add("X-HTTP-Method-Override", "DELETE");
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	public static IEnumerator COPY(string url, byte[] data = null, Dictionary<string, string> headers = null, Action<WWW> requestCompleteHandler = null)
	{
		data = data ?? new Byte[1];
		
		headers = headers ?? new Dictionary<string, string>();
		headers.Add("X-HTTP-Method-Override", "COPY");
		
		IEnumerator request = Request(url, data, headers, requestCompleteHandler);
		
		while (request.MoveNext())
		{
			yield return null;
		}
	}
	
	private static IEnumerator Request(string url, byte[] data, Dictionary<string, string> headers, Action<WWW> requestCompleteHandler)
	{		
		WWW www = new WWW(url, data, headers);
		
		IEnumerator requestWait = WaitForRequest(www, requestCompleteHandler);
		
		while (requestWait.MoveNext())
		{
			yield return null;
		}
	}
	
	private static IEnumerator WaitForRequest(WWW www, Action<WWW> requestCompleteHandler)
	{				
		while (!www.isDone)
		{
			yield return null;
		}
		
		if (requestCompleteHandler != null)
		{
			requestCompleteHandler(www);
		}
	}
}