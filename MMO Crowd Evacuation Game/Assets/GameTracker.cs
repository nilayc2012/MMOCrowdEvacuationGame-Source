using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameTracker : MonoBehaviour {

    public GameObject winnerrow;



    // Use this for initialization
    void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController1>()!=null && !other.gameObject.GetComponent<PlayerController1>().userend)
        {
            GameObject newrow = Instantiate(winnerrow);
            newrow.transform.parent = winnerrow.transform.parent;
            newrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
            winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
            newrow.transform.GetChild(1).GetComponentInChildren<Text>().text = other.gameObject.GetComponent<PlayerController1>().pname;
            other.gameObject.GetComponent<PlayerController1>().userend = true;
            newrow.transform.GetChild(2).GetComponentInChildren<Text>().text = GameObject.Find("GameController").GetComponent<TimeCounterMulti>().time.ToString();
            newrow.SetActive(true);
            other.gameObject.GetComponent<PlayerController1>().userend = true;

            if(other.gameObject.GetComponent<PlayerController1>().localplayer)
            {
                GameObject.Find("DataTracker").GetComponent<DataTracker>().end = true;
                other.gameObject.GetComponent<PlayerController1>().endpos = new Pos(other.gameObject.transform.position.x, other.gameObject.transform.position.z);
                other.gameObject.GetComponent<PlayerController1>().score = GameObject.Find("GameController").GetComponent<TimeCounterMulti>().time;
                other.gameObject.GetComponent<PlayerController1>().scorerType = "Team";
                other.gameObject.GetComponent<PlayerController1>().scoretype = "Evacuation Time";
                GameObject.Find("DataTracker").GetComponent<StoreScript>().createXML();
            }
            
        }
        else if(other.gameObject.GetComponent<CompleteChecker>()!=null && !other.gameObject.GetComponent<CompleteChecker>().userend)
        {
            GameObject newrow = Instantiate(winnerrow);
            newrow.transform.parent = winnerrow.transform.parent;
            newrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
            winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
            newrow.transform.GetChild(1).GetComponentInChildren<Text>().text = other.gameObject.name;
            other.gameObject.GetComponent<CompleteChecker>().userend = true;
            newrow.transform.GetChild(2).GetComponentInChildren<Text>().text = GameObject.Find("GameController").GetComponent<TimeCounterMulti>().time.ToString();
            newrow.SetActive(true);
            
        }


    }
}
