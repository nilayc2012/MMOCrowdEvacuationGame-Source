using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameControllerMultiCrowd : NetworkBehaviour
{

    public GameObject winnerPanel;
    public GameObject winnerPanel5;
    public Text winner;
    public Text finalScore;
    public Text scorer;
    public Text score;
    public Text score1;
    public Text min;
    public Text sec;


    public GameObject winnerrow;

    [SyncVar]
    public int time;


    int count;
    GameMetaScript gmc;
    bool end;

    void Awake()
    {
        end = false;
        count = 0;
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        if (gmc.diffid == "1")
        {
            time = 300;
        }
        else if (gmc.diffid == "2")
        {
            time = 500;
        }
        else
        {
            time = 700;
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
            count++;
            if (count == 60)
            {
                count = 0;
                if (time > 0)
                {
                    time--;
                }
            }
            float minval = time / 60;
            float secval = time % 60;

            if (minval < 10)
            {
                min.text = "0" + minval.ToString();
            }
            else
            {
                min.text = minval.ToString();
            }

            if (secval < 10)
            {
                sec.text = "0" + secval.ToString();
            }
            else
            {
                sec.text = secval.ToString();
            }

            if (gmc.ctypeid == "2")
            {
                score.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                score1.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2.ToString();
            }
            else if (gmc.ctypeid == "1")
            {
                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
                {
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        score.text = agent.GetComponent<PrizeCounter>().ballcount.ToString();
                    }
                    else
                    {
                        score1.text = agent.GetComponent<PrizeCounter>().ballcount.ToString();
                    }
                }
            }
            else if (gmc.ctypeid == "3")
            {
                score.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
            }
            else if (gmc.ctypeid == "5")
            {
                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
                {
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        score.text = agent.GetComponent<PrizeCounter>().ballcount.ToString();
                        scorer.text = agent.GetComponent<PlayerController1>().pname;
                    }
                }
            }

            if (time == 0 && !end)
            {

            GameObject.Find("DataTracker").GetComponent<DataTracker>().end = true;
            if (gmc.ctypeid == "1")
                {
                    int bestcount = 0;
                    string winnername = "";
                    foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
                    {

                    //data tracker////////////////////////////
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        agent.GetComponent<PlayerController1>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<PlayerController1>().score = agent.GetComponent<PrizeCounter>().ballcount;
                        agent.GetComponent<PlayerController1>().scorerType = "Solo";
                        agent.GetComponent<PlayerController1>().scoretype = "Number of exists";
                    }
                    //data tracker///////////////////////////

                    if (agent.GetComponent<PrizeCounter>().ballcount > bestcount)
                        {
                            bestcount = agent.GetComponent<PrizeCounter>().ballcount;
                            winnername = agent.GetComponent<PlayerController1>().pname;
                        }
                        else if (agent.GetComponent<PrizeCounter>().ballcount == bestcount && bestcount != 0)
                        {
                            winnername = winnername + " and " + agent.GetComponent<PlayerController1>().pname;
                        }
                    }
                    winner.text = winnername;
                    finalScore.text = bestcount.ToString();
                    winnerPanel.SetActive(true);

                }
                else if (gmc.ctypeid == "5")
                {

                    List<GameObject> sortedlist = GameObject.FindGameObjectsWithTag("multiplayer").ToList<GameObject>();
                    sortedlist = sortedlist.OrderByDescending(o => o.GetComponent<PrizeCounter>().ballcount).ToList<GameObject>();
                    foreach (GameObject agent in sortedlist)
                    {

                    //data tracker///////////////////////////
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        agent.GetComponent<PlayerController1>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<PlayerController1>().score = agent.GetComponent<PrizeCounter>().ballcount;
                        agent.GetComponent<PlayerController1>().scorerType = "Solo";
                        agent.GetComponent<PlayerController1>().scoretype = "Number of exists";
                    }
                    //data tracker///////////////////////////

                    GameObject newrow = Instantiate(winnerrow);
                        newrow.transform.parent = winnerrow.transform.parent;
                        newrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
                        winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text = (Int32.Parse(winnerrow.transform.GetChild(0).GetComponentInChildren<Text>().text) + 1).ToString();
                        newrow.transform.GetChild(1).GetComponentInChildren<Text>().text = agent.GetComponent<PlayerController1>().pname;
                        newrow.transform.GetChild(2).GetComponentInChildren<Text>().text = agent.GetComponent<PrizeCounter>().ballcount.ToString();
                        newrow.SetActive(true);
                    }

                    winnerPanel5.SetActive(true);
                }
                else if (gmc.ctypeid == "3")
                {

                //data tracker///////////////////////////
                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
                {
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        agent.GetComponent<PlayerController1>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        agent.GetComponent<PlayerController1>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1;
                        agent.GetComponent<PlayerController1>().scorerType = "Team";
                        agent.GetComponent<PlayerController1>().scoretype = "Number of exists";
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

                foreach (GameObject agent in GameObject.FindGameObjectsWithTag("multiplayer"))
                {
                    if (agent.GetComponent<PlayerController1>().localplayer)
                    {
                        agent.GetComponent<PlayerController1>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
                        if (agent.GetComponent<PrizeCounter>().teamno == 1)
                        {
                            agent.GetComponent<PlayerController1>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1;
                            agent.GetComponent<PlayerController1>().scorerType = "Team1";
                            agent.GetComponent<PlayerController1>().scoretype = "Number of exists";
                            break;
                        }
                        else
                        {
                            agent.GetComponent<PlayerController1>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2;
                            agent.GetComponent<PlayerController1>().scorerType = "Team2";
                            agent.GetComponent<PlayerController1>().scoretype = "Number of exists";
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

            GameObject.Find("DataTracker").GetComponent<StoreScript>().createXML();
            end = true;
            }
        }
    
}
