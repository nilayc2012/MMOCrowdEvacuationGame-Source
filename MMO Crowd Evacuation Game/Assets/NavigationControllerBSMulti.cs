using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NavigationControllerBSMulti : NetworkBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    public bool isHere;
    private Animator refPlayerAnim;
    public GameObject detectedBomb;
    public GameObject maincam;
    // Use this for initialization
    void Start()
    {
        refPlayerAnim = GetComponent<Animator>();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        // agent.ResetPath();
        isHere = false;
    }

    void OnEnable()
    {
        if (this.gameObject.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
        {
            Camera.main.gameObject.SetActive(false);
            maincam.SetActive(true);
        }

        //NetworkServer.SpawnWithClientAuthority(this.gameObject,connectionToClient);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().isLocalPlayer)
        {
            this.gameObject.GetComponent<BombDefuserMulti>().helicopter.GetComponent<GameStateChecker>().initiateSquadCall(transform.position.x, 0.1f, transform.position.z, transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z, agent.velocity.magnitude);
            this.GetComponent<PlayerControllerBSMulti>().enabled = false;
        }
            detectedBomb = this.gameObject.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().detectedBomb;
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
            //this.gameObject.GetComponent<PlayerControllerBSMulti>().enabled = true;

            if (this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
            {

                this.gameObject.GetComponent<PlayerControllerBSMulti>().enabled = true;


                GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

                foreach (GameObject g in objects)
                {
                    if (g.tag == "Panel (8)")
                    {
                        g.GetComponentInChildren<Text>().text = "Find The Bomb";
                        g.SetActive(true);
                        break;
                    }
                }
            }
                agent.Stop();
                agent.ResetPath();

                refPlayerAnim.SetFloat("Speed", 0);
                isHere = true;


            this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<GameStateChecker>().initiateFindBomb();

           /* if (this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
            {
                GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj.GetComponent<GameStateChecker>().findBomb = true;
            }*/
            this.gameObject.GetComponent<NavigationControllerBSMulti>().enabled = false;
            

        }
        
    }
}
