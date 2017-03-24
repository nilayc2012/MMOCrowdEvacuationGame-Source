using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameControllerCrowdEvac : NetworkBehaviour
{
    public Text min;
    public Text sec;

    public Text winner;
    public Text scoreMin;
    public Text scoresec;

    public GameObject finishPanel;

    [SyncVar]
    public int time;

    int count = 0,count1=0,count2=0;

    bool end1, end2;

    void Awake()
    {
        time = 0;
        count = 0;
        end1 = false;
        end2 = false;
    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!end1 && !end2)
        {
            count1 = 0;
            count2 = 0;
            foreach (PlayerController1 agent in GameObject.FindObjectsOfType<PlayerController1>())
            {
                if (!agent.userend && agent.gameObject.GetComponent<PrizeCounter>().teamno == 1)
                {
                    count1++;
                }
                else if (!agent.userend && agent.gameObject.GetComponent<PrizeCounter>().teamno == 2)
                {
                    count2++;
                }
            }

            if (count1 == 0 && !end1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 = time;

                end1 = true;
            }

            if (count2 == 0 && !end2)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2 = time;
                end2 = true;
            }


            count++;
            if (count == 60)
            {
                count = 0;
                time++;
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
                        agent.GetComponent<PlayerController1>().scoretype = "Evacuation Time";
                        break;
                    }
                    else
                    {
                        agent.GetComponent<PlayerController1>().score = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2;
                        agent.GetComponent<PlayerController1>().scorerType = "Team2";
                        agent.GetComponent<PlayerController1>().scoretype = "Evacuation Time";
                        break;
                    }

                }
            }

            //data tracker///////////////////////////

            if (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1> GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2)
            {
                winner.text = "Team 1 ";
                scoreMin.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 / 60).ToString();
                scoresec.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 % 60).ToString();
                
            }
            else if (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 < GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2)
            {
                winner.text = "Team 2";
                scoreMin.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2 / 60).ToString();
                scoresec.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2 % 60).ToString();
            }

            else
            {
                winner.text = "Team 1 & Team 2";
                scoreMin.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 / 60).ToString();
                scoresec.text = (GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1 % 60).ToString();
            }

            finishPanel.SetActive(true);
        }
    }

}

