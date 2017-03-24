using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeCounterSingle : MonoBehaviour {

    GameMetaScript gmc;
    public Vector3 startpos;

    public int ballcount;

    public int teamno;

    // Use this for initialization
    void Start()
    {
        ballcount=0;
        startpos = transform.position;
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
