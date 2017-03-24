using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataTrackerSingle : MonoBehaviour {

    public Pos borderBottomLeft, borderBottomRight, borderTopLeft, borderTopRight;
    public List<Pos> pathpositions;
    int count;

    public bool end;
    public bool once;

    public GameMetaScript gmc;
    public GameObject localagent;

    public List<Pos> goalPositions;

    public string scoretype;
    public string goaltype;
    // Use this for initialization
    void Start()
    {
        once = false;
        Transform ground = GameObject.Find("Ground").transform;
        borderBottomLeft = new Pos(ground.position.x - ground.localScale.x * 10 / 2, ground.position.z - ground.localScale.z * 10 / 2);
        borderBottomRight = new Pos(ground.position.x + ground.localScale.x * 10 / 2, ground.position.z - ground.localScale.z * 10 / 2);
        borderTopLeft = new Pos(ground.position.x - ground.localScale.x * 10 / 2, ground.position.z + ground.localScale.z * 10 / 2);
        borderTopRight = new Pos(ground.position.x + ground.localScale.x * 10 / 2, ground.position.z + ground.localScale.z * 10 / 2);

        pathpositions = new List<Pos>();
        goalPositions = new List<Pos>();
        count = 0;
        end = false;
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        localagent = GameObject.FindGameObjectWithTag("multiplayer");
        
            if (gmc.ruleid == "1" || gmc.ruleid == "2")
            {

                foreach (GameObject goal in GameObject.FindGameObjectsWithTag("prize"))
                {
                    goalPositions.Add(new Pos(goal.transform.position.x, goal.transform.position.z));
                }
            }
            else if (gmc.ruleid == "3" || gmc.ruleid == "4")
            {

                foreach (GameObject goal in GameObject.FindGameObjectsWithTag("bomb"))
                {
                    goalPositions.Add(new Pos(goal.transform.position.x, goal.transform.position.z));
                }
            }
        


        if (gmc.ruleid == "1")
        {
            goaltype = "Prizes";
        }
        else if (gmc.ruleid == "2")
        {
            goaltype = "Exit Locations";
        }
        else
        {
            goaltype = "Bombs to Defuse";
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(!once)
        {
            if (gmc.ruleid == "1" || gmc.ruleid == "2")
            {

                foreach (GameObject goal in GameObject.FindGameObjectsWithTag("prize"))
                {
                    goalPositions.Add(new Pos(goal.transform.position.x, goal.transform.position.z));
                }
            }
            else if (gmc.ruleid == "3" || gmc.ruleid == "4")
            {

                foreach (GameObject goal in GameObject.FindGameObjectsWithTag("bomb"))
                {
                    goalPositions.Add(new Pos(goal.transform.position.x, goal.transform.position.z));
                }
            }
        }

        if (!end)
        {
            count++;

            if (count == 3)
            {
                count = 0;
                pathpositions.Add(new Pos(localagent.transform.position.x, localagent.transform.position.z));

            }
        }
    }
}
