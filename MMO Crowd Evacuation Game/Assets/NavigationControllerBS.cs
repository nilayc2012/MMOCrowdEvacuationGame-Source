using UnityEngine;
using System.Collections;

public class NavigationControllerBS : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    public GameObject detectedBomb;
    public bool isHere;
    private Animator refPlayerAnim;

    // Use this for initialization
    void Start () {
        refPlayerAnim = GetComponent<Animator>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
       // agent.ResetPath();
        isHere = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        this.GetComponent<PlayerController1BS>().enabled = false;

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

        foreach(GameObject bomb in bombs)
        {
            if(bomb.GetComponent<BombDetector>().detected)
            {
                detectedBomb = bomb;
                break;
            }
        }

        Vector3 temp = new Vector3(detectedBomb.transform.position.x, transform.position.y, detectedBomb.transform.position.z);

        if (Vector3.Distance(transform.position,detectedBomb.transform.position)>40f)
        {
//            agent.Resume();
            agent.SetDestination(detectedBomb.transform.GetChild(0).position);
            refPlayerAnim.SetFloat("Speed", agent.velocity.magnitude);
            //refPlayerAnim.SetFloat("Direction", agent.velocity.z * 180f);

        }
        else
        {
            this.gameObject.GetComponent<RayPointTracker>().enabled = true;
            agent.Stop();
            agent.ResetPath();
            this.gameObject.GetComponent<NavigationControllerBS>().enabled= false;
            refPlayerAnim.SetFloat("Speed", 0);
            isHere = true;
        }

	}
}
