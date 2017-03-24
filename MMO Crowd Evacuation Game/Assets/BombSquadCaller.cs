using UnityEngine;
using System.Collections;

public class BombSquadCaller : MonoBehaviour {

    public GameObject GodCam;
    public GameObject panel;
    public GameObject distantPanel;

    public GameObject approachPanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CallBombSquad()
    {
        panel.SetActive(false);
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach(GameObject soldier in soldiers)
        {
            soldier.GetComponent<NavigationControllerBS>().enabled = true;
        }

        GameObject[] drones = GameObject.FindGameObjectsWithTag("drone");

        GameObject activedrone=null;

        foreach(GameObject drone in drones)
        {
            if(drone.GetComponent<DistanceChecker>().enabled)
            {
                activedrone = drone;
                drone.GetComponent<DistanceChecker>().enabled = false;
                distantPanel.SetActive(false);
                drone.GetComponent<HeliControl>().enabled = false;
                drone.transform.FindChild("Main Camera").gameObject.SetActive(false);
                break;
            }
        }

        BombDiffuser.helicopter = activedrone;
        approachPanel.SetActive(true);
        approachPanel.GetComponent<SoldierApproach>().enabled = true;
        GodCam.transform.position = new Vector3(activedrone.transform.position.x, GodCam.transform.position.y, activedrone.transform.position.z);
        GodCam.SetActive(true);
        //BombDiffuser.helicopter.GetComponent<HeliControl>().helicam.SetActive(false);
    }
}
