using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmyUnit : MonoBehaviour {

    //public GameObject droneCam;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Vector3 campos = Camera.main.WorldToScreenPoint(transform.position);
            campos.y = CameraOperator.InverseMouseY(campos.y);
            this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected = CameraOperator.selection.Contains(campos);

        }

   if(this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected) // && droneCam==null)
        {
            this.transform.FindChild("indicator").gameObject.SetActive(true);
          /*  foreach (GameObject drone in GameObject.FindGameObjectsWithTag("drone"))
            {
                if (!drone.GetComponent<QuadracopterRayPointTracker>().selected)
                {
                    droneCam = drone;
                    drone.GetComponent<QuadracopterRayPointTracker>().selected = true;
                    drone.GetComponent<QuadCopterController>().agentchosen = true;
                    drone.transform.parent = this.transform;
                    drone.transform.position = new Vector3(drone.transform.position.x, drone.transform.position.y + 7, drone.transform.position.z-5);
                    break;
                }
            }*/
        }
        else //if(!this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected)
        {
            this.transform.FindChild("indicator").gameObject.SetActive(false);
           /* if (droneCam != null)
            {
                droneCam.GetComponent<QuadracopterRayPointTracker>().selected = false;
                droneCam.GetComponent<QuadCopterController>().agentchosen = false ;
                droneCam.transform.parent = GameObject.Find("drones").transform;
            }
            droneCam = null;
            */
        
        }
    }
}
