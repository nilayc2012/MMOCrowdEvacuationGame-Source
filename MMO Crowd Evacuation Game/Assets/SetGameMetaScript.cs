using UnityEngine;
using System;
using System.Collections;
using Prototype.NetworkLobby;

public class SetGameMetaScript : MonoBehaviour {

    void Awake()
    {
        GameMetaScript srcmeta = GameObject.Find("GameMeta").GetComponent<GameMetaScript>();
        LobbyManager lmanage = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lmanage.maxPlayers = Int32.Parse(srcmeta.maxp);
        lmanage.minPlayers = Int32.Parse(srcmeta.minp);

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
