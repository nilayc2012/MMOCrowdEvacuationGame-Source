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

    [SyncVar]
    public GameObject soldierObj;

    [SyncVar]
    public GameObject detectedBomb;

    public bool userend;

    public GameObject soldier;

    public Pos startpos, endpos;

    public int score;

    public string scorerType;

    public string scoretype;

    // Use this for initialization
    void Start()
    {
        startpos = new Pos(transform.position.x, transform.position.z);
        GameObject soldierobjt = Instantiate(soldier);
        GameObject planeobj = GameObject.Find("soldierplane");
        float x = UnityEngine.Random.Range(planeobj.transform.position.x - planeobj.transform.localScale.x * 10 / 2, planeobj.transform.position.x + planeobj.transform.localScale.x * 10 / 2);
        float z = UnityEngine.Random.Range(planeobj.transform.position.z - planeobj.transform.localScale.z * 10 / 2, planeobj.transform.position.z + planeobj.transform.localScale.z * 10 / 2);
        soldierobjt.transform.position = new Vector3(x, 0.1f, z);
        soldierobjt.SetActive(true);

        this.GetComponent<HeliControlMulti>().soldierObj = soldierobjt;
        soldierobjt.GetComponent<BombDefuserMulti>().helicopter = this.gameObject;

        userend = false;
        if (isLocalPlayer)
        {
            localplayer = true;
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

    // Update is called once per frame
    void FixedUpdate()
    {
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

        if (transform.position.y<2)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }

        if (!isLocalPlayer)
        {
            if(this.transform.Find("Main Camera")!=null)
            this.transform.Find("Main Camera").gameObject.SetActive(false);
            return;
        }

        if(Camera.main.gameObject.GetComponent<PlayerControllerBSMulti>()==null)
        {
            if(this.transform.Find("Main Camera")!= null)
            this.transform.Find("Main Camera").gameObject.SetActive(true);
        }

        this.GetComponentInChildren<Camera>().gameObject.SetActive(true);

        if (Input.GetKey(KeyCode.Space))
        {
            this.gameObject.GetComponent<AH_AnimationHelper>().engineOn = true;

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
}
