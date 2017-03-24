using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameTrackerMulti : MonoBehaviour {

    public GameObject winnerrow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PrizeCounter>().ballcount++;
        this.transform.position = this.GetComponent<PrizeCounter>().startpos;
    }
}
