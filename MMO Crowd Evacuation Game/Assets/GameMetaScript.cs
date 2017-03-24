using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class GameMetaScript : MonoBehaviour {

    public string gname, gid, envid, ruleid, minp, maxp,ownerId, game_desc, gameoverid, diffid, ctypeid;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
