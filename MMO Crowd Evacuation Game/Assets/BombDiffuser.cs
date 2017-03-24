using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BombDiffuser : MonoBehaviour {

    public GameObject panel;
    public GameObject diffusedPanel;
    public static GameObject helicopter;
    public GameObject panel2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

        int count = 0;
        foreach (GameObject bomb in bombs)
        {
            Vector3 temp = new Vector3(bomb.transform.GetChild(0).position.x, transform.position.y, bomb.transform.GetChild(0).position.z);
            if (Vector3.Distance(transform.position, temp) <= 3.0f && !bomb.GetComponent<BombDetector>().isDiffused && bomb.GetComponent<BombDetector>().detected)
            {
                panel.SetActive(true);
                break;
            }
            count++;
        }

    /*    if(count==bombs.Length)
        {
            panel.SetActive(false);
        }*/
	}

    public void DiffuseBomb()
    {
        panel.SetActive(false);

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

        GameObject detectedbomb = null;
        foreach (GameObject bomb in bombs)
        {
            if (bomb.GetComponent<BombDetector>().detected)
            {
                detectedbomb = bomb;
                break;
            }
        }

        detectedbomb.GetComponent<BombDetector>().detected = false;
        detectedbomb.GetComponent<BombDetector>().isDiffused = true;
        detectedbomb.SetActive(false);

        GameObject[] Soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach (GameObject soldier in Soldiers)
        {
            soldier.GetComponent<RayPointTracker>().enabled = false;
        }

        diffusedPanel.SetActive(true);
        diffusedPanel.GetComponent<DiffuseCompletion>().enabled = true;
        diffusedPanel.GetComponent<DiffuseCompletion>().complete = true;
    }

    public void ResumeSearch()
    {
        panel2.SetActive(false);
        GameObject[] Soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach(GameObject soldier in Soldiers)
        {
            soldier.GetComponent<PlayerController1BS>().enabled = false;
            soldier.GetComponent<NavigationControllerBS>().enabled = false;
        }

        GameObject[] drones = GameObject.FindGameObjectsWithTag("drone");

        foreach (GameObject drone in drones)
        {
            drone.GetComponent<RayPointTracker>().enabled = false;
        }


        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,30f,Camera.main.transform.position.z);
        helicopter.GetComponent<HeliControl>().enabled = true;
        helicopter.GetComponent<DistanceChecker>().enabled = true;
    }
}
    