using System;
using UnityEngine;

public class TradeoffAnalyticsException : Exception
{
	public TradeoffAnalyticsException()
	{
	
	}
	
	public TradeoffAnalyticsException(string message) : base(message)
	{
	
	}
	
	public TradeoffAnalyticsException(string message, Exception inner) : base(message, inner)
	{
	
	}
}