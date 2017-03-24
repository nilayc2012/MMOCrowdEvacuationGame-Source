using UnityEngine;
using System.Collections;

public class QuadCopterController : MonoBehaviour {

    public GameObject propFL, propFR, propRL, propRR;
    public bool engineStart;
    float acceleration;
    public Vector3 initialPosition;

    UnityEngine.AI.NavMeshAgent agent;

    //public bool agentchosen;
	// Use this for initialization
	void Start () {

        //agentchosen = false;
        engineStart = false;
        acceleration = 0;
        initialPosition = transform.position;
        //this.gameObject.transform.position = new Vector3(this.transform.position.x,GameObject.FindGameObjectWithTag("upperlevel").transform.position.y,this.transform.position.z);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if(!this.gameObject.GetComponent<QuadracopterRayPointTracker>().selected )
        {
            if(Vector3.Distance(initialPosition,transform.position)<0.5f)
            {
                agent.ResetPath();
                agent.Stop();
            }
            else
            {
                agent.SetDestination(initialPosition);
            }
        }
        else
        {
            agent.avoidancePriority = 5;
        }

        transform.eulerAngles = new Vector3( -90f, 90f,transform.rotation.z);

        if (engineStart)
        {

            if (acceleration < 1)
            {
                acceleration = acceleration + 0.1f;
            }
            propFL.transform.Rotate(new Vector3(0, 0, 90* acceleration));
            propFR.transform.Rotate(new Vector3(0, 0, 90* acceleration));
            propRL.transform.Rotate(new Vector3(0, 0, 90* acceleration));
            propRR.transform.Rotate(new Vector3(0, 0, 90* acceleration));
        }
        else
        {
            acceleration = 0;
        }

    }
}
