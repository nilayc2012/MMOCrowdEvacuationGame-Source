using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayPointTracker : MonoBehaviour {

    RaycastHit hitInfo;
    RaycastHit hit;
    
    // Use this for initialization
    void Start () {

        hitInfo = new RaycastHit();
        hit = new RaycastHit();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {

                if (hitInfo.transform.gameObject == this.gameObject) {
                    if (this.gameObject.GetComponent<PlayerController1BS>() != null)
                    {
                        this.gameObject.GetComponent<PlayerController1BS>().enabled = true;
                    }
                    else if (this.gameObject.GetComponent<HeliControl>() != null)
                    {
                        this.gameObject.GetComponent<HeliControl>().enabled = true;
                        this.gameObject.GetComponent<DistanceChecker>().enabled = true;
                        
                    }
                }
                else
                {
                    if (hitInfo.transform.gameObject.tag.Equals("soldier") || hitInfo.transform.gameObject.tag.Equals("drone"))
                    {
                        if (this.gameObject.GetComponent<PlayerController1BS>() != null)
                        {
                            this.gameObject.GetComponent<PlayerController1BS>().enabled = false;
                        }
                        else if (this.gameObject.GetComponent<HeliControl>() != null)
                        {
                            this.gameObject.GetComponent<HeliControl>().enabled = false;
                            this.gameObject.GetComponent<DistanceChecker>().enabled = false;
                   
                        }
                    }
                }

            }
        }
    }
}
