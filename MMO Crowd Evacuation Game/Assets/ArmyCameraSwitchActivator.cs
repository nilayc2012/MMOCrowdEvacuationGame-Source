using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCameraSwitchActivator : MonoBehaviour {

    public List<GameObject> selectedarmydrones;
    public GameObject dronepanel;
    public GameObject armybutton;
    public GameObject maincam;
    // Use this for initialization
    void Start()
    {

        selectedarmydrones = new List<GameObject>();

        foreach (GameObject army in GameObject.FindGameObjectsWithTag("soldier"))
        {
            if (army.GetComponent<NavigationControllerBS>().enabled)
            {
                selectedarmydrones.Add(army);
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        selectedarmydrones = new List<GameObject>();
        foreach (GameObject army in GameObject.FindGameObjectsWithTag("soldier"))
        {
            if (army.GetComponent<NavigationControllerBS>().enabled)
            {
                    selectedarmydrones.Add(army);
            }
            else
            {
                if (army.GetComponent<ArmyCameraChoice>().enabled)
                {
                    army.GetComponent<ArmyCameraChoice>().camera1.SetActive(false);
                    army.GetComponent<ArmyCameraChoice>().enabled = false;
                }
            }
        }

        if (selectedarmydrones.Count > 0)
        {
            dronepanel.SetActive(true);
            armybutton.SetActive(true);
        }
        else
        {
            armybutton.SetActive(false);


        }

        if (selectedarmydrones.Count == 0)
        {
            dronepanel.SetActive(false);
            foreach(GameObject cam in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if(!cam.Equals(maincam))
                {
                    cam.SetActive(false);
                }
            }
            maincam.SetActive(true);
        }

    }
}
