using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour {

    [SyncVar]
    public float time;
	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        time = time + Time.fixedDeltaTime;
	}
}
