using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameTrackerSingle : MonoBehaviour {

    public GameObject winnerpanel;
    public Text scoremin;
    public Text scoresec;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        GameObject agent = GameObject.FindGameObjectWithTag("multiplayer");
        agent.GetComponent<PlayerController1single>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
        agent.GetComponent<PlayerController1single>().score = GameObject.Find("GameController").GetComponent<TimeCounter>().time;
        agent.GetComponent<PlayerController1single>().scorerType = "Solo";
        agent.GetComponent<PlayerController1single>().scoretype = "Time to exit";

        scoremin.text = (GameObject.Find("GameController").GetComponent<TimeCounter>().time/60).ToString();
        scoresec.text = (GameObject.Find("GameController").GetComponent<TimeCounter>().time % 60).ToString();
        GameObject.FindGameObjectWithTag("multiplayer").GetComponent<PlayerController1single>().userend = true;
        winnerpanel.SetActive(true);
        GameObject.Find("DataTracker").GetComponent<StoreSingleScript>().createXML();

    }
}
