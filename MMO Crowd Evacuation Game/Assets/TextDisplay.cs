﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextDisplay : MonoBehaviour {

    public GameObject Lobman;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.GetComponent<Text>().text = Lobman.GetComponent<Prototype.NetworkLobby.LobbyManager>().maxPlayers.ToString();
		
	}
}
