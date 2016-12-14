using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class WinnerCriteria : MonoBehaviour {

    GameObject[] agents,aiagents;
    bool once;
	// Use this for initialization
	void Start () {
        once = false;
        agents = GameObject.FindGameObjectsWithTag("multiplayer");
        aiagents = GameObject.FindGameObjectsWithTag("agent");

	}
	
	// Update is called once per frame
	void Update () {

        if (!once)
        {
            int count = 0, count1 = 0;
            foreach (GameObject agent in agents)
            {
                if (agent.GetComponent<PlayerController1>().userend)
                {
                    count++;

                }
                count1++;
            }

            foreach (GameObject agent in aiagents)
            {
                if (agent.GetComponent<CompleteChecker>().userend)
                {
                    count++;

                }
                count1++;
            }

            if (count == count1)
            {
                GameObject[] objects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

                foreach (GameObject g in objects)
                {
                    if (g.name.Equals("exit"))
                    {
                        g.SetActive(true);
                        break;
                    }
                }
                once = true;
            }
        }
	
	}

    public void exitGame()
    {
         GameObject.Find("LobbyManager").GetComponent<LobbyManager>().GoBackButton();

    }
}
