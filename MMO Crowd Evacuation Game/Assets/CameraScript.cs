using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// For first person camera movement
public class CameraScript : MonoBehaviour {


    List<GameObject> cameraList;
    int listcounter;
	// Use this for initialization
	void Start () {

        GameObject[] cameras = GameObject.FindGameObjectsWithTag("subcamera");

        cameraList = new List<GameObject>();
        listcounter = 0;

        cameraList.Add(Camera.main.gameObject);

        foreach(GameObject camera in cameras)
        {
            cameraList.Add(camera);
        }
        

	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (listcounter == cameraList.Count-1)
            {
                cameraList[listcounter].GetComponent<Camera>().enabled = false;
                cameraList[listcounter].tag = "subcamera";
                listcounter = 0;
            }
            else
            {
                cameraList[listcounter].GetComponent<Camera>().enabled = false;
                cameraList[listcounter++].tag = "subcamera";
            }

            cameraList[listcounter].GetComponent<Camera>().enabled = true;
            cameraList[listcounter].tag = "MainCamera";
        }
	
	}
}
