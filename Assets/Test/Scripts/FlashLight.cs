using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour {

	public Light flashlight;
	public AudioSource audioSource;
	public AudioClip flashlight_on;
	public AudioClip flashlight_off;

    public Text lightText;

    private bool isActive;

	// Use this for initialization
	void Start () {
        flashlight.enabled = false;
		isActive = false;

        Invoke("destroyText", 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			if (isActive == false) {
				flashlight.enabled = true;	
				isActive = true;

				audioSource.PlayOneShot (flashlight_on);
			} else {
				flashlight.enabled = false;
				isActive = false;

				audioSource.PlayOneShot (flashlight_off);			}
		}
	}

    void destroyText()
    {
        lightText.enabled = false;
    }
}
