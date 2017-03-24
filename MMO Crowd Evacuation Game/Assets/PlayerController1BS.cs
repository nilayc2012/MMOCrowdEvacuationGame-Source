using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController1BS : MonoBehaviour
{
    

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

    public GameObject soldiercam;
    public GameObject maincam;

    //public GameObject winnerpanel;


    public bool userend;

    void Start()
    {
        userend = false;
            refPlayerAnim = GetComponent<Animator>();
            refCollider = GetComponent<CapsuleCollider>();
            if (refPlayerAnim.layerCount == 2)
                refPlayerAnim.SetLayerWeight(1, 1);

        
    }


    void FixedUpdate()
    {
        this.GetComponent<NavigationControllerBS>().enabled = false;
        maincam.SetActive(false);
        soldiercam.SetActive(true);


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