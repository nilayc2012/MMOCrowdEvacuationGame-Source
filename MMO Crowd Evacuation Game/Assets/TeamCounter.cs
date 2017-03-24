using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamCounter : NetworkBehaviour {

    [SyncVar]
    public int ballcount1;

    [SyncVar]
    public int ballcount2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
