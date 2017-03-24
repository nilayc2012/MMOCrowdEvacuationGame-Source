using UnityEngine;
using System.Collections;

public class QuadBombSquadCaller : MonoBehaviour {

    public GameObject GodCam;
    public GameObject panel;

    public GameObject approachPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallBombSquad()
    {
        panel.SetActive(false);
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach (GameObject soldier in soldiers)
        {
            soldier.GetComponent<NavigationControllerBS>().enabled = true;
        }

        GameObject[] drones = GameObject.FindGameObjectsWithTag("drone");

        GameObject activedrone = null;

        foreach(GameObject drone in drones)
        {
            if(drone.GetComponent<QuadracopterRayPointTracker>().selected)
            {
                activedrone = drone;
                break;
            }
        }

        approachPanel.SetActive(true);
        approachPanel.GetComponent<SoldierApproach>().enabled = true;
        GodCam.transform.position = new Vector3(activedrone.transform.position.x, GodCam.transform.position.y, activedrone.transform.position.z);
        GodCam.SetActive(true);

    }
}
