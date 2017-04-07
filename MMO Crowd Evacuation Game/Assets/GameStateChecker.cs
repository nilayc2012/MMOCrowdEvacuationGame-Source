using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStateChecker : NetworkBehaviour {

    //[SyncVar(hook = "OnCallSquad")]
    //public bool callSquad;

    //[SyncVar]
    //public bool findBomb;

   // [SyncVar(hook = "OnDefuseBomb")]
    //public bool defusedBomb;

    //[SyncVar]
    public float horiz,vert;

   // public Transform pos;

	// Use this for initialization
	void Start () {

        //callSquad = false;
        //findBomb = false;
        //defusedBomb = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    public void initiatePositionChange(float horiz,float vert)
    {
        if(isServer)
        {
            RpcPositionChange(horiz,vert);
        }
        else
        {
            CmdPositionChange(horiz, vert);
        }
    }

    [Command]
    public void CmdPositionChange(float horiz, float vert)
    {
        this.horiz = horiz;
        this.vert = vert;
    }

    [ClientRpc]
    public void RpcPositionChange(float horiz, float vert)
    {
        this.horiz = horiz;
        this.vert = vert;
    }

    public void initiateSquadCall(float posx,float posy,float posz,float rotx,float roty,float rotz,float speed)
    {
        if(isServer)
        {
            RpcCallSquad(posx, posy, posz, rotx, roty, rotz, speed);
        }
        else
        {
            CmdCallSquad(posx, posy, posz, rotx, roty, rotz, speed);
            
        }
    }

    public void initiateBombDefuse()
    {
        if (isServer)
        {
            RpcDefuseBomb();
        }
        else
        {

            CmdDefuseBomb();
        }
    }

    public void initiateBombDefuse(float bombposx, float bombposy, float bombposz)
    {
        if (isServer)
        {
            RpcDefuseBombTime(bombposx,bombposy,bombposz);
        }
        else
        {

            CmdDefuseBombTime(bombposx,bombposy,bombposz);
        }
    }

    [Command]
    public void CmdCallSquad(float posx, float posy, float posz, float rotx, float roty, float rotz, float speed)
    {
        if (!this.gameObject.GetComponent<HeliControlMulti>().localplayer)
        {
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.transform.position = new Vector3(posx, posy, posz);
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.transform.eulerAngles = new Vector3(rotx, roty, rotz);
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<Animator>().SetFloat("Speed", speed);
        }

    }
    [Command]
    public void CmdDefuseBomb()
    {
        //Network.Destroy(this.gameObject.GetComponent<HeliControlMulti>().detectedBomb);

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            this.gameObject.GetComponent<PrizeCounter>().ballcount++;
        }
        else if (gmc.ctypeid == "2")
        {
            if (this.gameObject.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
        }

        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().detected = false;
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.SetActive(false);

    }

    [Command]
    public void CmdDefuseBombTime(float bombposx, float bombposy, float bombposz)
    {
        //Network.Destroy(this.gameObject.GetComponent<HeliControlMulti>().detectedBomb);

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            this.gameObject.GetComponent<PrizeCounter>().ballcount++;
        }
        else if (gmc.ctypeid == "2")
        {
            if (this.gameObject.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
        }

        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().detected = false;
        // this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        // this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.SetActive(false);
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.transform.position = new Vector3(bombposx, this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.transform.position.y, bombposz);

    }

    [ClientRpc]
    public void RpcCallSquad(float posx, float posy, float posz, float rotx, float roty, float rotz, float speed)
    {
        if (!this.gameObject.GetComponent<HeliControlMulti>().localplayer)
        {
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.transform.position = new Vector3(posx, posy, posz);
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.transform.eulerAngles = new Vector3(rotx, roty, rotz);
            this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }

    [ClientRpc]
    public void RpcDefuseBomb()
    {

        //Network.Destroy(this.gameObject.GetComponent<HeliControlMulti>().detectedBomb);

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            this.gameObject.GetComponent<PrizeCounter>().ballcount++;
        }
        else if (gmc.ctypeid == "2")
        {
            if (this.gameObject.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
        }

        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().detected = false;
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.SetActive(false);

    }

    [ClientRpc]
    public void RpcDefuseBombTime(float bombposx, float bombposy, float bombposz)
    {

        //Network.Destroy(this.gameObject.GetComponent<HeliControlMulti>().detectedBomb);

        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            this.gameObject.GetComponent<PrizeCounter>().ballcount++;
        }
        else if (gmc.ctypeid == "2")
        {
            if (this.gameObject.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
        }

        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().detected = false;
        //this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.GetComponent<BombDetectorMulti>().isDiffused = true;
        //this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.SetActive(false);
        this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.transform.position = new Vector3(bombposx, this.gameObject.GetComponent<HeliControlMulti>().detectedBomb.transform.position.y, bombposz);

    }

    public void initiateFindBomb()
    {
        if(isServer)
        {
            RpcFindBomb();
        }
        else
        {
            CmdFindBomb();
        }
    }
    [Command]
    public void CmdFindBomb()
    {
        this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<PlayerControllerBSMulti>().enabled = true;
        this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<NavigationControllerBSMulti>().enabled = false;
    }

    [ClientRpc]
    public void RpcFindBomb()
    {
        this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<PlayerControllerBSMulti>().enabled = true;
        this.gameObject.GetComponent<HeliControlMulti>().soldierObj.GetComponent<NavigationControllerBSMulti>().enabled = false;
    }
}
