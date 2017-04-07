using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombDetectorMulti : NetworkBehaviour
{


    public GameObject distantPanel;
    public GameObject panel;

    // [SyncVar]
    public bool detected;
    // [SyncVar]
    public GameObject detectedDrone;
    //[SyncVar]
    public bool isDiffused;

    public float regionx,regiony,regionz;
    // Use this for initialization
    void Start()
    {

        detected = false;

        isDiffused = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,0.1f,transform.position.z);

    }

    public void initateSetRegion(float regx,float regy,float regz)
    {
        if(isServer)
        {
            RpcSetRegion(regx, regy, regz);
        }
        else
        {
            CmdSetRegion(regx, regy, regz);
        }
    }


    [Command]
    void CmdSetRegion(float regx, float regy, float regz)
    {
        this.gameObject.transform.position = new Vector3(regx, regy, regz);
        regionx = regx;
        regiony = regy;
        regionz = regz;
        GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().bomPosis.Remove(new RegionPos(regionx,regiony,regionz));
    }

    [ClientRpc]
    void RpcSetRegion(float regx, float regy, float regz)
    {
        this.gameObject.transform.position = new Vector3(regx, regy, regz);
        regionx = regx;
        regiony = regy;
        regionz = regz;
        GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>().bomPosis.Remove(new RegionPos(regionx, regiony, regionz));
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("drone") && !detected)
        {
            if (other.gameObject.GetComponent<HeliControlMulti>().localplayer)
            {
                GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

                bool flag1 = false, flag2 = false;

                foreach (GameObject g in objects)
                {
                    if (g.name == "distancepanel")
                    {
                        g.SetActive(false);
                        flag1 = true;
                    }
                    if (g.name == "Panel (2)")
                    {
                        g.SetActive(true);
                        flag2 = true;
                    }
                    if (flag1 && flag2)
                    {
                        break;
                    }

                }
            }

                other.gameObject.GetComponent<HeliControlMulti>().enabled = false;
                other.gameObject.GetComponent<AH_AnimationHelper>().engineOn = false;
                other.gameObject.GetComponent<ParkDroneMulti>().enabled = true;
                detected = true;
                detectedDrone = other.gameObject;
                other.gameObject.GetComponent<HeliControlMulti>().detectedBomb = this.gameObject;
                other.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<NavigationControllerBSMulti>().detectedBomb = this.gameObject;


            }

        }
    }

