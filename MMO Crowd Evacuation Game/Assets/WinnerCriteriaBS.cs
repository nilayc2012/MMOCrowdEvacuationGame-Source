using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class WinnerCriteriaBS : MonoBehaviour {

    GameObject[] agents, aiagents;

    public GameObject exitbutton;
    bool once;
    // Use this for initialization
    void Start()
    {
        once = false;
        agents = GameObject.FindGameObjectsWithTag("multiplayer");
        aiagents = GameObject.FindGameObjectsWithTag("agent");

    }

    // Update is called once per frame
    void Update()
    {

        if (!once)
        {
            int count = 0, count1 = 0;
            agents = GameObject.FindGameObjectsWithTag("drone");
            foreach (GameObject agent in agents)
            {
                if (agent.GetComponent<PlayerController1>().userend)
                {
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
