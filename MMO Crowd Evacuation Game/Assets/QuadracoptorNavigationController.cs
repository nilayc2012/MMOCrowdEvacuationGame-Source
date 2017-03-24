using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class QuadracoptorNavigationController : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    RaycastHit hitInfo;


    // Use this for initialization
    void Start () {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        hitInfo = new RaycastHit();
    }
	
	// Update is called once per frame
	void Update () {

        if (this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected) // && !this.gameObject.GetComponent<QuadCopterController>().agentchosen)
        {
            if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject() || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)||Input.GetKey(KeyCode.LeftAlt)||Input.GetKey(KeyCode.RightAlt)|| Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 10000))
                {
                    if (!hitInfo.transform.gameObject.tag.Equals("drone")|| !hitInfo.transform.gameObject.tag.Equals("soldier"))
                    {
                        agent.SetDestination(hitInfo.point);

                    }

                }
            }
        }
                    
	}
}
