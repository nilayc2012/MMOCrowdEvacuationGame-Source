using UnityEngine;
using System.Collections;

public class QuadBombDefuser : MonoBehaviour {

    public GameObject panel;
    public GameObject diffusedPanel;
    public GameObject panel2;
    public Vector3 initialpos;

    public float intialRot;

    public bool returning;

    // Use this for initialization
    void Start()
    {
        initialpos = this.transform.position;
        returning = false;
        intialRot = this.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {


        if(!this.GetComponent<QuadracopterRayPointTracker>().selected)
        {

            if (Vector3.Distance(transform.position, initialpos) <= 1.0f && returning)
            {
                //transform.position = new Vector3(transform.position.x, initialpos.y, transform.position.z);
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity=Vector3.zero;
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                this.GetComponent<Animator>().SetFloat("Speed", 0);
                this.transform.eulerAngles=new Vector3(this.transform.rotation.x, intialRot, this.transform.rotation.z);

                if(Vector3.Distance(transform.position, initialpos) > 0.1f)
                {
                    transform.position = Vector3.Slerp(transform.position, initialpos, Time.deltaTime * 5);
                }
                returning = false;
            }
            else if(Vector3.Distance(transform.position, initialpos) > 1.0f)
            {
                returning = true;
                //transform.position = new Vector3(transform.position.x, initialpos.y, transform.position.z);
                //this.GetComponent<UnityEngine.AI.NavMeshAgent>().Resume();
                //this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(initialpos);
                }


        }

    }

    public void DiffuseBomb()
    {
        panel.SetActive(false);

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

        GameObject detectedbomb = null;
        foreach (GameObject bomb in bombs)
        {
            if (bomb.GetComponent<QuadBombDetector>().found)
            {
                detectedbomb = bomb;
                break;
            }
        }

        detectedbomb.GetComponent<QuadBombDetector>().found = false;
        detectedbomb.GetComponent<QuadBombDetector>().detected = false;
        detectedbomb.GetComponent<QuadBombDetector>().isDiffused = true;
        detectedbomb.SetActive(false);
        diffusedPanel.SetActive(true);
        diffusedPanel.GetComponent<DiffuseCompletion>().enabled = true;
        diffusedPanel.GetComponent<DiffuseCompletion>().complete = true;
    }

    public void ResumeSearch()
    {
        panel2.SetActive(false);
        GameObject.Find("GameController").GetComponent<DroneCameraSwitchActivator>().closePanel = false;
        GameObject[] Soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach (GameObject soldier in Soldiers)
        {
            if(soldier.GetComponent<ArmyRayPointTracker>().enabled)
            {
                soldier.GetComponent< UnityEngine.AI.NavMeshAgent>().enabled = false;
                soldier.GetComponent<QuadraPlayerController>().enabled = false;
                soldier.GetComponent<ArmyRayPointTracker>().enabled = false;
                soldier.GetComponent<ArmyUnit>().enabled = true;
                soldier.GetComponent<ArmyNavigationController>().enabled = true;
                soldier.GetComponent<ArmyNavigationController>().reached = false;
                soldier.GetComponent<QuadracopterRayPointTracker>().enabled = true;
                soldier.GetComponent<QuadracopterRayPointTracker>().selected = false;
                soldier.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

            }

        }

        GameObject[] drones = GameObject.FindGameObjectsWithTag("drone");

        foreach(GameObject drone in drones)
        {
            if(drone.GetComponent<QuadracopterRayPointTracker>().selected)
            {
                drone.GetComponent<Unit>().enabled = true;
                drone.GetComponent<QuadracoptorNavigationController>().enabled = true;
                drone.GetComponent<QuadracopterRayPointTracker>().enabled = true;
                drone.GetComponent<QuadracopterRayPointTracker>().selected = false;
            }
                
        }
        Camera.main.gameObject.SetActive(false);
        GameObject.Find("GameController").GetComponent<GameControllerBS>().maincam.SetActive(true);


    }
}
