using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
	public bool test = false;

	public bool open = false;
	private bool hasOpenedCompletly;
    private bool hasClosedCompletly;
	public float door_Open_Angle = 90f;
	public float door_Close_Angle = 0f;

	public float smooth = 2f;

	public void ChangeDoorState(){
		open = !open; // for switching
	}

	// Use this for initialization
	void Start () {
        hasOpenedCompletly = false;
        hasClosedCompletly = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (open == true) {
            if(hasOpenedCompletly == false)
            {
                Quaternion targetRotationOpen = Quaternion.Euler(0, door_Open_Angle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);

                if (transform.localRotation == targetRotationOpen)
                {
                    hasOpenedCompletly = true;
                    hasClosedCompletly = false;
                }
            }
        } else if(open == false)
        {
            if (hasClosedCompletly == false)
            {
                Quaternion targetRotationClosed = Quaternion.Euler(0, door_Close_Angle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);

                if (transform.localRotation == targetRotationClosed)
                {
                    hasClosedCompletly = true;
                    hasOpenedCompletly = false;
                   
                }
            }

        }
    }
    /*
     void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player") ){
            
            ChangeDoorState(); 
        }
	}
    */

}
