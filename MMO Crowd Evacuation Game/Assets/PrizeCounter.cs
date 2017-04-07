using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PrizeCounter : NetworkBehaviour {

    GameMetaScript gmc;
    public Vector3 startpos;

    [SyncVar]
    public int ballcount;

    [SyncVar]
    public int teamno;

    // Use this for initialization
    void Start () {
        ballcount = 0;
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {




    }
}
