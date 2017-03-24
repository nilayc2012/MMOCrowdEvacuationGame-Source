using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour {

    public GameObject winnerPanel;
    public Text finalScoremin;
    public Text finalScoresec;
    public Text min;
    public Text sec;

    public GameObject spawnGround;
    int time;
    int count;

    public int initialBallcount;

    public GameObject prize;

    void Awake()
    {
        time = 0;
        count = 0;
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        if(gmc.diffid=="1")
        {
            initialBallcount = 5;
        }
        else if (gmc.diffid == "2")
        {
            initialBallcount = 10;
        }
        else
        {
            initialBallcount = 15;
        }

        for(int i=0;i<initialBallcount;i++)
        {
            GameObject prizeobj = Instantiate(prize);
            float x = Random.Range(spawnGround.transform.position.x - spawnGround.transform.localScale.x / 2+20, spawnGround.transform.position.x + spawnGround.transform.localScale.x / 2-20);
            float z = Random.Range(spawnGround.transform.position.z - spawnGround.transform.localScale.z / 2+20, spawnGround.transform.position.z + spawnGround.transform.localScale.z / 2-20);
            prizeobj.transform.position = new Vector3(x, prizeobj.transform.position.y, z);
            prizeobj.SetActive(true);
        }


    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (GameObject.FindGameObjectsWithTag("prize").Length == 0)
        {
            GameObject.Find("DataTracker").GetComponent<DataTrackerSingle>().end = true;
            GameObject agent = GameObject.FindGameObjectWithTag("multiplayer");
            agent.GetComponent<PlayerController1single>().endpos = new Pos(agent.transform.position.x, agent.transform.position.z);
            agent.GetComponent<PlayerController1single>().score = time;
            agent.GetComponent<PlayerController1single>().scorerType = "Solo";
            agent.GetComponent<PlayerController1single>().scoretype = "Prizes Collected";

            float finmin = time / 60;
            float finsec = time % 60;
            agent.GetComponent<PlayerController1single>().userend = true;
            finalScoremin.text = finmin.ToString();
            finalScoresec.text = finsec.ToString();
            winnerPanel.SetActive(true);
            GameObject.Find("DataTracker").GetComponent<StoreSingleScript>().createXML();
            return;
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
            min.text = "0"+minval.ToString();
        }
        else
        {
            min.text = minval.ToString();
        }

        if(secval<10)
        {
            sec.text = "0"+secval.ToString();
        }
        else
        {
            sec.text = secval.ToString();
        }
        


	}
}
