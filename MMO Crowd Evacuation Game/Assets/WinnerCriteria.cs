using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class WinnerCriteria : MonoBehaviour {

    GameObject[] agents,aiagents;

    public GameObject exitbutton;

    public GameObject finalPanel;
    bool once;
	// Use this for initialization
	void Start () {
        once = false;
        agents = GameObject.FindGameObjectsWithTag("multiplayer");
        aiagents = GameObject.FindGameObjectsWithTag("agent");

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!once)
        {
            int count = 0, count1 = 0;
            agents = GameObject.FindGameObjectsWithTag("multiplayer");
            foreach (GameObject agent in agents)
            {
                if (agent.GetComponent<PlayerController1>().userend)
                {
                    if(agent.GetComponent<PlayerController1>().localplayer)
                    {
                        finalPanel.SetActive(true);
                    }
                    count++;

                }
            }

          /*  foreach (GameObject agent in aiagents)
            {
                if (agent.GetComponent<CompleteChecker>().userend)
                {
                    count++;

                }
                count1++;
            }*/

            if (count == agents.Length)
            {
                exitbutton.SetActive(true);
                once = true;
            }
        }
	
	}

    public void exitGame()
    {
        //GameObject.Find("LobbyManager").GetComponent<LobbyManager>().GoBackButton();
        SceneManager.LoadScene("gameselector");

    }
}
