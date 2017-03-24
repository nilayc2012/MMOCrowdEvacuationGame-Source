using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArmyNavigationController : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;

    private Animator refPlayerAnim;

    RaycastHit hitInfo;
    public bool reached;

    // Use this for initialization
    void Start()
    {
        reached = false;
        refPlayerAnim = GetComponent<Animator>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        hitInfo = new RaycastHit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (this.GetComponent<ArmyUnit>().droneCam != null)
        {
            this.GetComponent<ArmyUnit>().droneCam.transform.parent = this;

        }*/
        refPlayerAnim.SetFloat("Speed", agent.velocity.magnitude);

        if (this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected)
        {
            if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject() || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 10000))
                {
                    if (!hitInfo.transform.gameObject.tag.Equals("drone") || !hitInfo.transform.gameObject.tag.Equals("soldier"))
                    {
                        if (!reached)
                        {
                            if (Vector3.Distance(transform.position, new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z)) <= 0.5f)
                            {
                                //transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
                                //this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                                this.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = Vector3.zero;
                                this.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
                                this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                                this.GetComponent<Animator>().SetFloat("Speed", 0);
                                this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                                this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;


                            }
                            else
                            {

                                agent.SetDestination(hitInfo.point);

                            }
                        }
                        else
                        {
                            this.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = Vector3.zero;
                            Debug.Log(this.gameObject.name);
                            this.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
                            this.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
                            Debug.Log(this.gameObject.name);
                            this.GetComponent<Animator>().SetFloat("Speed", 0);
                            this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                            this.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                            
                        }
                    }

                }
            }
        }

    }
}
