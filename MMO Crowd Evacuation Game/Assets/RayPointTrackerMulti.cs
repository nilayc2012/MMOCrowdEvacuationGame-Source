using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPointTrackerMulti : MonoBehaviour {

    RaycastHit hitInfo;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

        hitInfo = new RaycastHit();
        hit = new RaycastHit();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {

                if (hitInfo.transform.gameObject == this.gameObject)
                {
                    if (this.gameObject.GetComponent<PlayerControllerBSMulti>() != null)
                    {
                        this.gameObject.GetComponent<PlayerControllerBSMulti>().enabled = true;
                    }
                    else if (this.gameObject.GetComponent<HeliControlMulti>() != null)
                    {
                        this.gameObject.GetComponent<HeliControlMulti>().enabled = true;
                        this.gameObject.GetComponent<DistanceCheckerMulti>().enabled = true;

                    }
                }
                else
                {
                    if (hitInfo.transform.gameObject.tag.Equals("soldier") || hitInfo.transform.gameObject.tag.Equals("drone"))
                    {
                        if (this.gameObject.GetComponent<PlayerControllerBSMulti>() != null)
                        {
                            this.gameObject.GetComponent<PlayerControllerBSMulti>().enabled = false;
                        }
                        else if (this.gameObject.GetComponent<HeliControlMulti>() != null)
                        {
                            this.gameObject.GetComponent<HeliControlMulti>().enabled = false;
                            this.gameObject.GetComponent<DistanceCheckerMulti>().enabled = false;

                        }
                    }
                }

            }
        }
    }
}
