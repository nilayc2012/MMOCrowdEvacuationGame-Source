using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DroneCameraSwitchActivator : MonoBehaviour {

    public List<GameObject> selecteddrones;
    public List<GameObject> selectedarmydrones;
    public GameObject dronepanel;
    public GameObject armybutton;
    public GameObject dronebutton;

    public bool closePanel;
    // Use this for initialization
    void Start () {

        closePanel = false;

        selecteddrones = new List<GameObject>();
        selectedarmydrones = new List<GameObject>();
        foreach (GameObject drone in GameObject.FindGameObjectsWithTag("drone"))
        {
            if (drone.GetComponent<QuadracopterRayPointTracker>().selected)
            {
                selecteddrones.Add(drone);
            }
        }

        foreach (GameObject army in GameObject.FindGameObjectsWithTag("soldier"))
        {
            if (army.GetComponent<QuadracopterRayPointTracker>().selected)
            {
                selectedarmydrones.Add(army);
            }
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        selecteddrones = new List<GameObject>();
        foreach (GameObject drone in GameObject.FindGameObjectsWithTag("drone"))
        {
            if (drone.GetComponent<QuadracopterRayPointTracker>().selected) // && !drone.GetComponent<QuadCopterController>().agentchosen)
            {
                selecteddrones.Add(drone);
            }
        }

        if(selecteddrones.Count>0)
        {
            dronepanel.SetActive(true);
            dronebutton.SetActive(true);

        }
        else
        {
            dronebutton.SetActive(false);

        }

        selectedarmydrones = new List<GameObject>();
        foreach (GameObject army in GameObject.FindGameObjectsWithTag("soldier"))
        {
            if (army.GetComponent<QuadracopterRayPointTracker>().selected)
            {
                selectedarmydrones.Add(army);
            }
        }

        if(selectedarmydrones.Count>0)
        {
            dronepanel.SetActive(true);
            armybutton.SetActive(true);
        }
        else
        {
            armybutton.SetActive(false);


        }

        if((selecteddrones.Count==0 && selectedarmydrones.Count == 0)||closePanel)
        {
            dronepanel.SetActive(false);
        }

    }
}
