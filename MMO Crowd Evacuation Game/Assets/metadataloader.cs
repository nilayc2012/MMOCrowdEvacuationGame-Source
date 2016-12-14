using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class metadataloader : MonoBehaviour {

    public string gname, gid, envid, ruleid, minp, maxp, ownerId;

    void Awake()
    {

        GameObject.Find("playervalue").GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserDataScript>().gamername;
        GameObject.Find("GameName").GetComponent<Text>().text= GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().gname;
        LobbyManager lman= GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lman.minPlayers= Int32.Parse(GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().minp);
        lman.maxPlayers= Int32.Parse(GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().maxp);
        lman.playScene = "Scene" + GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().envid + GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid;

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
