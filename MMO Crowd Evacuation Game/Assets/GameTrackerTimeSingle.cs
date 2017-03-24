using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrackerTimeSingle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("GameController").GetComponent<GameControllerSingleTime>().ballcount++;
        other.gameObject.transform.position = other.gameObject.GetComponent<PrizeCounterSingle>().startpos;
    }
}
