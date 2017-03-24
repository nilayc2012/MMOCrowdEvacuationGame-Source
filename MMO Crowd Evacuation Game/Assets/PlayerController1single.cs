using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(Rigidbody))]

public class PlayerController1single : MonoBehaviour
{

    public float animSpeed = 1.5f;              // animation speed
    public bool curve;                          // curves to fix collider during jump

    private Animator refPlayerAnim;                         // reference player animator
    private AnimatorStateInfo refAnimState;         // reference animator state
    private CapsuleCollider refCollider;                    // reference collider

    static int checkMovement = Animator.StringToHash("Base Layer.Movement");
    static int checkJump = Animator.StringToHash("Base Layer.Jump");

    public Renderer shirtcolor;

    public Text text;

    //public GameObject winnerpanel;


    public bool userend;

    public Pos startpos, endpos;

    public int score;

    public string scorerType;

    public string scoretype;

    void Start()
    {
        startpos = new Pos(transform.position.x, transform.position.z);
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        userend = false;
            refPlayerAnim = GetComponent<Animator>();
            refCollider = GetComponent<CapsuleCollider>();
            if (refPlayerAnim.layerCount == 2)
                refPlayerAnim.SetLayerWeight(1, 1);

        
    }


    void FixedUpdate()
    {

     

        if(userend)
        {
            refPlayerAnim.SetFloat("Speed", 0.0f);
            refPlayerAnim.SetFloat("Direction", 0.0f);

            return;
        }

            Camera.main.transform.position = this.transform.position - this.transform.forward * 60 + this.transform.up * 40;
            Camera.main.transform.LookAt(this.transform.position + this.transform.forward * 10 + this.transform.up * 15);
            //Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
            Camera.main.transform.parent = this.transform;

            float horiz = Input.GetAxis("Horizontal");              // horizontal input axis
            float vert = Input.GetAxis("Vertical");

            refAnimState = refPlayerAnim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation
                                                                         //Debug.Log(refAnimState.tagHash);
            if (refAnimState.IsName("WalkRun"))
            {
                if (Input.GetKey(KeyCode.LeftShift) && vert > 0)
                {
                    vert = 6.005136f + vert;
                }
            }
            else if (vert < 0)
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
            //refPlayerAnim.speed = animSpeed;                                // animation speed variable
        }

        

    

}