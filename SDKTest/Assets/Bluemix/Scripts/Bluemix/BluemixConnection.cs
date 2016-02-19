using UnityEngine;
using System.Collections;

namespace Bluemix
{
	public class BluemixConnection : MonoBehaviour
	{
		/// <summary>
		/// The app URL endpoint for RESTful communication.
		/// </summary>
		public string appURL;

		/// <summary>
		/// The singleton instance of Bluemix Connection.
		/// </summary>
		private static BluemixConnection _instance;

		/// <summary>
		/// Gets the singleton instance of Bluemix Connection.
		/// </summary>
		/// <value>The instance.</value>
		public static BluemixConnection Instance
		{
			get
			{
				return BluemixConnection._instance;
			}
		}

		/// <summary>
		/// Awake this instance of Bluemix Connection as the singleton instance or otherwise destroy it.
		/// </summary>
		private void Awake()
		{		
			//Check Instance Status
			if (BluemixConnection._instance == null) 
			{
				//Make this instance the singleton instance and make it persist across scenes
				BluemixConnection._instance = this;
				DontDestroyOnLoad(this.gameObject);
			}		
			else 
			{
				//This is not the singleton instance; destroy it
				Destroy(this.gameObject);
			}
		}
	}
}