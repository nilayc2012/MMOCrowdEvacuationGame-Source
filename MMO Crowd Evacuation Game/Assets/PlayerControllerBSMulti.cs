using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerControllerBSMulti : NetworkBehaviour {

    public float animSpeed = 1.5f;              // animation speed
    public bool curve;                          // curves to fix collider during jump

    private Animator refPlayerAnim;                         // reference player animator
    private AnimatorStateInfo refAnimState;         // reference animator state
    private CapsuleCollider refCollider;                    // reference collider
    private Rigidbody rigidBody;

    static int checkMovement = Animator.StringToHash("Base Layer.Movement");
    static int checkJump = Animator.StringToHash("Base Layer.Jump");

    public Renderer shirtcolor;

    public Text text;

    //public GameObject soldiercam;
    //public GameObject maincam;

    //public GameObject winnerpanel;

    public bool localPlayer;

    public bool userend;

    public GameObject maincam;

    void Start()
    {
        userend = false;
        localPlayer = false;

        if(this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
        {
            localPlayer = true;

        }

        refPlayerAnim = GetComponent<Animator>();
        refCollider = GetComponent<CapsuleCollider>();
        if (refPlayerAnim.layerCount == 2)
            refPlayerAnim.SetLayerWeight(1, 1);

    }

    void OnEnable()
    {
        if (this.gameObject.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
        {
            Camera.main.gameObject.SetActive(false);
            maincam.SetActive(true);
        }

    }

    void FixedUpdate()
    {
        this.GetComponent<NavigationControllerBSMulti>().enabled = false;

        float horiz=0;
        float vert=0;

        if ((GameObject.Find("GameController").GetComponent<FinalOutComeBSMultiTask>() != null && GameObject.Find("GameController").GetComponent<FinalOutComeBSMultiTask>().finish) || (GameObject.Find("GameController").GetComponent<FinalOutcomeBSMultiTime>() != null && GameObject.Find("GameController").GetComponent<FinalOutcomeBSMultiTime>().finish))
        {
            // vertical input axis
            refPlayerAnim.SetFloat("Speed", 0);
            refPlayerAnim.SetFloat("Direction", 0);
            return;
        }

        if (this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<HeliControlMulti>().localplayer)
        {
            localPlayer = true;

            //maincam.SetActive(false);
            //soldiercam.SetActive(true);

            horiz = Input.GetAxis("Horizontal");              // horizontal input axis
            vert = Input.GetAxis("Vertical");


            refAnimState = refPlayerAnim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation
                                                                         //Debug.Log(refAnimState.tagHash);
            if (refAnimState.IsName("WalkRun"))
            {

                if (Input.GetKey(KeyCode.LeftShift) && vert > 0)
                {
                    vert = 6.005136f + vert;
                }
            }

            this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<GameStateChecker>().initiatePositionChange(horiz,vert);
        }

        else
        {
            refAnimState = refPlayerAnim.GetCurrentAnimatorStateInfo(0);
            horiz = this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<GameStateChecker>().horiz;              // horizontal input axis
            vert = this.GetComponent<BombDefuserMulti>().helicopter.GetComponent<GameStateChecker>().vert;
        }

        if (vert < 0)
        {
            refPlayerAnim.SetBool("B_StepBackTrigger", true);
        }
        else if (refAnimState.IsName("StepBack") && vert >= 0)
        {
            refPlayerAnim.SetBool("B_StepBackTrigger", false);
        }

            //this.GetComponent<Rigidbody>().AddForce(new Vector3(horiz, 0.0f, vert));
            // vertical input axis
            refPlayerAnim.SetFloat("Speed", vert);
            refPlayerAnim.SetFloat("Direction", horiz * 180f);

            float angle = Vector3.Angle(new Vector3(transform.eulerAngles.x + horiz * 90f, transform.eulerAngles.y, transform.eulerAngles.z), transform.eulerAngles);
            Vector3 cross = Vector3.Cross(new Vector3(transform.eulerAngles.x + horiz * 90f, transform.eulerAngles.y, transform.eulerAngles.z), transform.eulerAngles);

            if (cross.z < 0)
            {
                angle = -angle;
            }

            refPlayerAnim.SetFloat("AngularSpeed", angle / Time.fixedDeltaTime);
        transform.eulerAngles=new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 5*(horiz), transform.eulerAngles.z);
        //transform.RotateAround(Vector3.zero, Vector3.up, 90 * angle); 
            //refPlayerAnim.speed = animSpeed;                                // animation speed variable

        

    }
}
