using System;
using UnityEngine;

public class PersonalityInsightsException : Exception
{
	public PersonalityInsightsException()
	{
	
	}
	
	public PersonalityInsightsException(string message) : base(message)
	{
	
	}
	
	public PersonalityInsightsException(string message, Exception inner) : base(message, inner)
	{
	
	}
}