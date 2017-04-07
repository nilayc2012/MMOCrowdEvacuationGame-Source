using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeliControlMulti : NetworkBehaviour {

    public float speed = 10.0F;
    public Rigidbody rigidbody;
    public bool localplayer;

    [SyncVar]
    public string pname = "player";

    public GameObject soldierObj;

    public GameObject detectedBomb;

    public bool userend;

    public GameObject soldier;

    public Pos startpos, endpos;

    public int score;

    public string scorerType;

    public string scoretype;

    //[SyncVar(hook = "OnChangeEngineStatus")]
    //public bool engineOn;

    public GameObject maincam;
    void Awake()
    {
        if(GameObject.Find("GameController")==null)
        {
            GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

            foreach (GameObject g in objects)
            {
                if (g.name == "GameController")
                {
                    g.SetActive(true);
                    break;
                }
            }
        }

        if (GameObject.Find("StartPosGenerator") == null)
        {
            GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

            foreach (GameObject g in objects)
            {
                if (g.name == "StartPosGenerator")
                {
                    g.SetActive(true);
                    break;
                }
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        //engineOn = this.GetComponent<AH_AnimationHelper>().engineOn;
        startpos = new Pos(transform.position.x, transform.position.z);

        GameObject planeobj = GameObject.Find("soldierplane");
        float x = UnityEngine.Random.Range(planeobj.transform.position.x - planeobj.transform.localScale.x / 2, planeobj.transform.position.x + planeobj.transform.localScale.x / 2);
        float z = UnityEngine.Random.Range(planeobj.transform.position.z - planeobj.transform.localScale.z / 2, planeobj.transform.position.z + planeobj.transform.localScale.z / 2);

        /*       if (GameObject.Find("GameController").GetComponent<GameControllerBSMulti>() != null)
               {
                   soldierObj = Instantiate(GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().soldier, new Vector3(x, 0.1f, z),new Quaternion(0,0,0,0));
               }
               else if (GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>() != null)
               {
                   soldierObj = Instantiate(GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().soldier, new Vector3(x, 0.1f, z), new Quaternion(0, 0, 0, 0));
               }
       */
        soldierObj = Instantiate(soldier, new Vector3(x, 0.1f, z), new Quaternion(0, 0, 0, 0));

        soldierObj.SetActive(true);

        //NetworkServer.Spawn(soldierObj);

        soldierObj.GetComponent<BombDefuserMulti>().helicopter = this.gameObject;

        userend = false;
        if (isLocalPlayer)
        {
            localplayer = true;

            Camera.main.gameObject.SetActive(false);
            maincam.SetActive(true);
            GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

            if(gmc.gameoverid=="1")
            {
                if(GameObject.Find("GameController")!=null)
                GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj = this.gameObject;
            }
            else
            {
                if (GameObject.Find("GameController") != null)
                    GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj = this.gameObject;
            }
            

        }
        else
        {
            localplayer = false;
        }
            
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if (isLocalPlayer)
        {
            Camera.main.gameObject.SetActive(false);
            maincam.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (transform.position.y < 2)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }


        rigidbody = this.GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            localplayer = true;
            GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

            if (gmc.gameoverid == "1")
            {
                GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().localplayerobj = this.gameObject;
            }
            else
            {
                GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().localplayerobj = this.gameObject;
            }


        }



        if (!isLocalPlayer)
        {
            if (this.transform.Find("Main Camera")!=null)
            this.transform.Find("Main Camera").gameObject.SetActive(false);
            return;
        }

        if((GameObject.Find("GameController").GetComponent<FinalOutComeBSMultiTask>() != null && GameObject.Find("GameController").GetComponent<FinalOutComeBSMultiTask>().finish) || (GameObject.Find("GameController").GetComponent<FinalOutcomeBSMultiTime>() != null && GameObject.Find("GameController").GetComponent<FinalOutcomeBSMultiTime>().finish))
        {
            return;
        }

  /*      if(Camera.main.gameObject.GetComponent<PlayerControllerBSMulti>()==null)
        {
            if(this.transform.Find("Main Camera")!= null)
            this.transform.Find("Main Camera").gameObject.SetActive(true);
        }

        this.GetComponentInChildren<Camera>().gameObject.SetActive(true);*/

        if (Input.GetKey(KeyCode.Space))
        {
            if (isServer)
            {
                RpcChangeEngineStatus(true);
            }
            else
            {
                CmdChangeEngineStatus(true);
            }

            this.gameObject.GetComponent<AH_AnimationHelper>().engineOn = true;

                //engineOn = true;
            

            if (this.gameObject.GetComponent<AH_AnimationHelper>().currentRPM >= 0.8)
                transform.position += Vector3.up * 5.0f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (transform.position.y > 2)
            {
                transform.position += Vector3.down * 5.0f * Time.deltaTime;
            }
        }


        if (transform.position.y >= 3.0f)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveVertical < 0)
            {
                moveVertical = 0;
            }


            Vector3 movement = new Vector3(moveVertical * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 0.0f, moveVertical * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
            rigidbody.velocity = movement * speed;

            if (rigidbody.rotation.eulerAngles.y < 90 && rigidbody.rotation.eulerAngles.y > -90)
            {
                if (rigidbody.rotation.eulerAngles.y < 0 && rigidbody.rotation.eulerAngles.y > -90)
                    rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * 1.5f, rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x * 3.0f);
                else
                    rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z, rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x * -3.0f * Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * rigidbody.rotation.eulerAngles.y)));

            }
            else
            {
                rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * -1.5f * Mathf.Abs(1 / Mathf.Cos(Mathf.Deg2Rad * rigidbody.rotation.eulerAngles.y)), rigidbody.rotation.eulerAngles.y + 90 * moveHorizontal * Time.deltaTime, rigidbody.velocity.x);

            }

        }

    }

    [Command]
    public void CmdChangeEngineStatus(bool engineOn)
    {
        this.gameObject.GetComponent<AH_AnimationHelper>().engineOn = engineOn;
    }

    [ClientRpc]
    public void RpcChangeEngineStatus(bool engineOn)
    {
        this.gameObject.GetComponent<AH_AnimationHelper>().engineOn = engineOn;
    }
}
