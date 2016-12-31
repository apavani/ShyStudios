using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //GetComponent<Camera>().aspect = 9 / 16; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
