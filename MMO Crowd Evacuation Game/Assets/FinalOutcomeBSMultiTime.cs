using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalOutcomeBSMultiTime : MonoBehaviour {

    public GameObject winnerPanel;
    public GameObject winnerPanel5;
    public Text winner;
    public Text finalScore;


    public GameObject winnerrow;

    public GameObject diffusedPanel;
    public bool finish;

    GameMetaScript gmc;
    // Use this for initialization
    void Start()
    {
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        finish = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (this.GetComponent<GameControllerBSMultiTime>().time == 0)
        {
            diffusedPanel.SetActive(false);
            GameObject.Find("DataTracker").GetComponent<DataTracker>().end = true;

            if (gmc.ctypeid == "1")
            {
                int bestcount = 0;
                string winnername = "";
                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("drone"))
                {
                    //data tracker////////////////////////////
                    if (agent.GetComponent<HeliControlMulti>().localplayer)
                    {
                        agent.GetComponent<HeliControlMulti>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<HeliControlMulti>().score = agent.GetComponent<PrizeCounter>().ballcount;
                        agent.GetComponent<HeliControlMulti>().scorerType = "Solo";
                        agent.GetComponent<HeliControlMulti>().scoretype = "Bombs Defused";
                    }
                    //data tracker///////////////////////////
                    if (agent.GetComponent<PrizeCounter>().ballcount > bestcount)
                    {
                        bestcount = agent.GetComponent<PrizeCounter>().ballcount;
                        winnername = agent.GetComponent<HeliControlMulti>().pname;
                    }
                    else if (agent.GetComponent<PrizeCounter>().ballcount == bestcount && bestcount != 0)
                    {
                        winnername = winnername + " and " + agent.GetComponent<HeliControlMulti>().pname;
                    }
                }
                winner.text = winnername;
                finalScore.text = bestcount.ToString();
                winnerPanel.SetActive(true);

            }
            else if (gmc.ctypeid == "5")
            {

                List<GameObject> sortedlist = GameObject.FindGameObjectsWithTag("drone").ToList<GameObject>();
                sortedlist = sortedlist.OrderBy(o => o.GetComponent<PrizeCounter>().ballcount).ToList<GameObject>();
                foreach (GameObject agent in sortedlist)
                {

                    //data tracker///////////////////////////
                    if (agent.GetComponent<HeliControlMulti>().localplayer)
                    {
                        agent.GetComponent<HeliControlMulti>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<HeliControlMulti>().score = agent.GetComponent<PrizeCounter>().ballcount;
                        agent.GetComponent<HeliControlMulti>().scorerType = "Solo";
                        agent.GetComponent<HeliControlMulti>().scoretype = "Boms Defused";
                    }
                    //data tracker///////////////////////////

                    GameObject newrow = Instantiate(winnerrow);
                    newrow.transform.parent = winnerrow.transform.parent;
                    newrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
                    winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
                    newrow.transform.GetChild(1).GetComponentInChildren<Text>().text = agent.GetComponent<HeliControlMulti>().pname;
                    newrow.transform.GetChild(2).GetComponentInChildren<Text>().text = agent.GetComponent<PrizeCounter>().ballcount.ToString();
                    newrow.SetActive(true);
                }

                winnerPanel5.SetActive(true);
            }
            else if (gmc.ctypeid == "3")
            {
                //data tracker///////////////////////////
                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("drone"))
                {
                    if (agent.GetComponent<HeliControlMulti>().localplayer)
                    {
                        agent.GetComponent<HeliControlMulti>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<HeliControlMulti>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1;
                        agent.GetComponent<HeliControlMulti>().scorerType = "Team";
                        agent.GetComponent<HeliControlMulti>().scoretype = "Bombs Defused";
                        break;
                    }
                }
                //data tracker///////////////////////////

                finalScore.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                winnerPanel.SetActive(true);
            }
            else
            {
                //data tracker///////////////////////////

                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("drone"))
                {
                    if (agent.GetComponent<HeliControlMulti>().localplayer)
                    {
                        agent.GetComponent<HeliControlMulti>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        if (agent.GetComponent<PrizeCounter>().teamno == 1)
                        {
                            agent.GetComponent<HeliControlMulti>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1;
                            agent.GetComponent<HeliControlMulti>().scorerType = "Team1";
                            agent.GetComponent<HeliControlMulti>().scoretype = "Bombs Defused";
                            break;
                        }
                        else
                        {
                            agent.GetComponent<HeliControlMulti>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2;
                            agent.GetComponent<HeliControlMulti>().scorerType = "Team2";
                            agent.GetComponent<HeliControlMulti>().scoretype = "Bombs Defused";
                            break;
                        }

                    }
                }

                //data tracker///////////////////////////

                if (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 > GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2)
                {
                    winner.text = "Team 1";
                    finalScore.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                }
                else if (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 < GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2)
                {
                    winner.text = "Team 2";
                    finalScore.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2.ToString();
                }
                else
                {
                    winner.text = "Team 1 and Team 2";
                    finalScore.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                }
                winnerPanel.SetActive(true);
            }


            foreach (GameObject panelobj in GameObject.FindGameObjectsWithTag("panel"))
            {
                if (panelobj != winnerPanel)
                {
                    panelobj.SetActive(false);
                }
            }

            GameObject.Find("DataTracker").GetComponent<StoreScript>().createXML();
            finish = true;
        }

    }

    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void exit()
    {
        Application.Quit();
    }
}
