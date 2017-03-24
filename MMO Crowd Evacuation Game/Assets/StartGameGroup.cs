using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class StartGameGroup : NetworkBehaviour {


    public Text totalusers;

    public Text team1users;

    public Text team2users;

    public GameObject scorepanel;

    public Button startbutton;

    public Dropdown teamlist;

    // Use this for initialization
    void Start () {

        totalusers.text= GameObject.FindGameObjectsWithTag("multiplayer").Length.ToString();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        totalusers.text = GameObject.FindGameObjectsWithTag("multiplayer").Length.ToString();

        int count = 0;
        int count1 = 0;
        foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
        {
            if (agent.GetComponent<PrizeCounter>().teamno == 1)
            {
                count++;
            }
            else if(agent.GetComponent<PrizeCounter>().teamno == 2)
            {
                count1++;
            }
        }

        team1users.text = count.ToString();
        team2users.text = count1.ToString();

        if(count== GameObject.FindGameObjectsWithTag("multiplayer").Length/2 && count1== GameObject.FindGameObjectsWithTag("multiplayer").Length/2)
        {
            startbutton.interactable = true;
        }
        else
        {
            startbutton.interactable = false;
        }

        foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
        {
            if (agent.GetComponent<PlayerController1>()!=null && agent.GetComponent<PlayerController1>().localplayer)
            {
                agent.GetComponent<PrizeCounter>().teamno = teamlist.value + 1;
            }
            else if (agent.GetComponent<HeliControlMulti>() != null && agent.GetComponent<HeliControlMulti>().localplayer)
            {
                agent.GetComponent<PrizeCounter>().teamno = teamlist.value + 1;
            }
        }

    }

    public void startGameGroup()
    {
        
        if(GameObject.Find("GameController").GetComponent<GameControllerOneOnOne>()!=null)
        {
         //   GameObject.Find("GameController").GetComponent<GameControllerOneOnOne>().start = true;
        }
        else if(GameObject.Find("GameController").GetComponent<GameControllerBSMulti>()!=null)
        {
            //GameObject.Find("GameController").GetComponent<GameControllerBSMulti>().start = true;
        }
       // GameObject.Find("GameController").GetComponent<GameControllerOneOnOne>().start = true;

        if (scorepanel!=null)
            scorepanel.SetActive(true);

        this.gameObject.SetActive(false);
    }

   /* public void setTeamNumber()
    {
        foreach(GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
        {
                agent.GetComponent<PrizeCounter>().teamno = teamlist.value + 1;
        }
    }*/
}
