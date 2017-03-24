using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrackerMultiCrowd : MonoBehaviour {


    GameMetaScript gmc;
    // Use this for initialization

    void Start()
    {
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    // For Mazerunner game to decide when an agent collides with the prize
    void OnTriggerEnter(Collider other)
    {

        if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
        {
            other.gameObject.GetComponent<PrizeCounter>().ballcount++;
            other.gameObject.transform.position = other.gameObject.GetComponent<PrizeCounter>().startpos;
        }
        else if (gmc.ctypeid == "2")
        {
            if (other.gameObject.GetComponent<PrizeCounter>().teamno == 1)
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            }
            else
            {
                GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2++;
            }

            other.gameObject.transform.position = other.gameObject.GetComponent<PrizeCounter>().startpos;

        }
        else if (gmc.ctypeid == "3")
        {
            GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1++;
            other.gameObject.transform.position = other.gameObject.GetComponent<PrizeCounter>().startpos;
        }
    }
}
