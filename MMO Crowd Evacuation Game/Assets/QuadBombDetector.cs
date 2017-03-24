using UnityEngine;
using System.Collections;

public class QuadBombDetector : MonoBehaviour {

    public bool detected;

    public bool isDiffused;

    public GameObject area;

    public GameObject panel;

    public bool found;
    // Use this for initialization
    void Start()
    {
        found = false;
        detected = false;

        isDiffused = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] soldiers = GameObject.FindGameObjectsWithTag("soldier");

        foreach (GameObject soldier in soldiers)
        {
            Vector3 temp = new Vector3(this.transform.GetChild(0).position.x, soldier.transform.position.y, this.transform.GetChild(0).position.z);
            if (Vector3.Distance(soldier.transform.position, temp) <= 3.0f && !isDiffused && detected && !found && soldier.GetComponent<QuadraPlayerController>().enabled)
            {
                panel.SetActive(true);
                found = true;
                break;
            }
            else if (found && soldier.GetComponent<QuadraPlayerController>().enabled && Vector3.Distance(transform.position, temp) > 3.0f)
            {
                panel.SetActive(false);
                found = false;
                break;
            }

        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (!area.activeSelf)
        {
            if (other.gameObject.tag.Equals("drone")&&!detected)
            {
                detected = true;
                area.SetActive(true);
                // other.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = Vector3.zero;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                other.GetComponent<QuadracoptorNavigationController>().enabled = false;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

                foreach (GameObject soldier in GameObject.FindGameObjectsWithTag("soldier"))
                {
                    soldier.GetComponent<ArmyUnit>().enabled = true;
                    soldier.GetComponent<QuadracopterRayPointTracker>().enabled = true;
                }
            }
        }
        else
        {
            if (other.gameObject.tag.Equals("soldier")&&detected)
            {
                // Debug.Log("reached"+other.name);
                //other.transform.position = new Vector3(other.transform.position.x, 0.1f, other.transform.position.z);

                other.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = Vector3.zero;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                other.GetComponent<Animator>().SetFloat("Speed", 0);
                other.GetComponent<ArmyNavigationController>().reached = true;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                other.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                other.GetComponent<ArmyRayPointTracker>().enabled = true;
                other.GetComponent<ArmyUnit>().enabled = false;
                other.GetComponent<QuadracopterRayPointTracker>().enabled = false;


            }
        }

    }
}
