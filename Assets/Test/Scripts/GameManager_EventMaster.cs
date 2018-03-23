using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_EventMaster : MonoBehaviour {

	public delegate void GeneralEvent();
	public event GeneralEvent myGeneralEvent;

	/*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

	public void CallMyGeneralEvent()
	{
		if (myGeneralEvent != null) 
		{
			myGeneralEvent ();
		}
	}
}
