using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameTrackerOnlyGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="multiplayer")
        {
            other.gameObject.GetComponent<PlayerController1>().userend = true;
        }
    /*    else if (other.gameObject.GetComponent<CompleteChecker>() != null && !other.gameObject.GetComponent<CompleteChecker>().userend)
        {
            other.gameObject.GetComponent<CompleteChecker>().userend = true;
        }*/
    }
}
