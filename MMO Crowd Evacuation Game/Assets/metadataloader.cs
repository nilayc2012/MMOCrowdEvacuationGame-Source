using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class metadataloader : MonoBehaviour {

    public string gname, gid, envid, ruleid, minp, maxp, ownerId;
    public Text gameName;

    void Start()
    {
        gameName.text = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().gname;
        GameObject.Find("playervalue").GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserDataScript>().gamername;
        GameObject.Find("GameName").GetComponent<Text>().text= GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().gname;
        LobbyManager lman= GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lman.minPlayers = Int32.Parse(GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().minp);
        lman.maxPlayers = Int32.Parse(GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().maxp);
        
        if(GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid=="3" || GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid=="4")
        {
            lman.lobbyScene = "lobbyscene" + GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid;
        }


        lman.playScene = "Scene1" + GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid+ GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().gameoverid+ GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ctypeid;


    }
    // Use this for initialization

	
	// Update is called once per frame
	void FixedUpdate () {

        if (GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ctypeid != "2")
        {
            foreach (GameObject dropd in GameObject.FindGameObjectsWithTag("teamdropdown"))
            {
                dropd.SetActive(false);
            }
        }

        if (GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ctypeid=="2")
        {

            LobbyPlayer[] lobbyPlayers = GameObject.FindObjectsOfType<LobbyPlayer>();

            GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("teamdropdown");

            int count = 0, count1 = 0;

            foreach (GameObject dropdown in dropdowns)
            {
                if (dropdown.GetComponent<Dropdown>().value == 0)
                {
                    count++;
                }
                else
                {
                    count1++;
                }
            }

            if (count != lobbyPlayers.Length / 2 || count1 != lobbyPlayers.Length / 2)
            {
                foreach (GameObject jbutton in GameObject.FindGameObjectsWithTag("joinbutton"))
                {
                    jbutton.GetComponent<Button>().interactable = false;
                }
            }
            else
            {
                foreach (GameObject jbutton in GameObject.FindGameObjectsWithTag("joinbutton"))
                {
                    jbutton.GetComponent<Button>().interactable = true;
                }
            }
        }
	
	}
}
