using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerSingleTime : MonoBehaviour {

    public GameObject winnerPanel;
    public Text finalScore;
    public Text score;
    public Text min;
    public Text sec;

    public int ballcount;
    int time;
    int count;

    void Awake()
    {
        ballcount = 0;
        count = 0;
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
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


        score.text = ballcount.ToString();

        if (time == 0)
        {
            GameObject agent = GameObject.FindGameObjectWithTag("multiplayer");
            agent.GetComponent<PlayerController1single>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
            agent.GetComponent<PlayerController1single>().score = ballcount;
            agent.GetComponent<PlayerController1single>().scorerType = "Solo";
            agent.GetComponent<PlayerController1single>().scoretype = "Number of exits";

            agent.GetComponent<PlayerController1single>().userend = true;
            finalScore.text = ballcount.ToString();
            winnerPanel.SetActive(true);
            GameObject.Find("DataTracker").GetComponent<StoreSingleScript>().createXML();
        }
    }
}
