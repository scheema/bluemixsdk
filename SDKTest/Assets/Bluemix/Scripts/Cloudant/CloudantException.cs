using System;
using UnityEngine;

public class CloudantException : Exception
{
	public CloudantException()
	{
	
	}
	
	public CloudantException(string message) : base(message)
	{
	
	}
	
	public CloudantException(string message, Exception inner) : base(message, inner)
	{
	
	}
}