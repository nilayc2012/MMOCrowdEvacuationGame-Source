using UnityEngine;
using System.Collections;

public class ArmyRayPointTracker : MonoBehaviour {

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
                    this.gameObject.GetComponent<ArmyNavigationController>().enabled = false;
                    this.gameObject.GetComponent<QuadraPlayerController>().enabled = true;
                    this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

                }
                else
                {
                    if (hitInfo.transform.gameObject.tag.Equals("soldier"))
                    {
                        this.gameObject.GetComponent<QuadraPlayerController>().enabled = false;
                        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                        this.gameObject.GetComponent<ArmyNavigationController>().enabled = true;

                    }
                }

            }
        }
    }
}
