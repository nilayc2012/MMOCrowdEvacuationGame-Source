using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationControllerBSMulti : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    public bool isHere;
    private Animator refPlayerAnim;

    // Use this for initialization
    void Start()
    {
        refPlayerAnim = GetComponent<Animator>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        // agent.ResetPath();
        isHere = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        this.GetComponent<PlayerControllerBSMulti>().enabled = false;
        GameObject detectedBomb = GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<HeliControlMulti>().detectedBomb;
        Vector3 temp = new Vector3(detectedBomb.transform.position.x, transform.position.y, detectedBomb.transform.position.z);

        if (Vector3.Distance(transform.position, detectedBomb.transform.position) > 40f)
        {
            //            agent.Resume();
            agent.SetDestination(detectedBomb.transform.GetChild(0).position);
            refPlayerAnim.SetFloat("Speed", agent.velocity.magnitude);
            //refPlayerAnim.SetFloat("Direction", agent.velocity.z * 180f);

        }
        else
        {
            this.gameObject.GetComponent<PlayerControllerBSMulti>().enabled = true;
            GameObject.Find("Panel (8)").GetComponentInChildren<Text>().text = "Find The Bomb";
            GameObject.Find("Panel (8)").SetActive(true);
            agent.Stop();
            agent.ResetPath();
            this.gameObject.GetComponent<NavigationControllerBSMulti>().enabled = false;
            refPlayerAnim.SetFloat("Speed", 0);
            isHere = true;
        }

    }
}
