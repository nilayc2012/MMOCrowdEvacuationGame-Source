using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class BombSpawnerTime : NetworkBehaviour {


    public GameObject bombPrefab;
    GameMetaScript gmc;
    int bombcount;

    // Use this for initialization
    public override void OnStartServer()
    {

        //base.OnStartServer();

        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        /*  if (gmc.ctypeid!="2" && gmc.ctypeid != "3")
          {
              start = true;
          }
          else
          {
              start = false;
          }*/


        GameObject[] agents = GameObject.FindGameObjectsWithTag("drone");

        if (gmc.diffid == "1")
        {
            bombcount = 3 * agents.Length;
        }
        else if (gmc.diffid == "2")
        {
            bombcount = 5 * agents.Length;
        }
        else
        {
            bombcount = 7 * agents.Length;
        }

        GameObject[] regions = GameObject.FindGameObjectsWithTag("region");
        UnityEngine.Random.InitState(10);

        for (int i = 0; i < bombcount; i++)
        {
            GameObject bomb = Instantiate(bombPrefab);

            int regionIndex = UnityEngine.Random.Range(0, regions.Length);


            int index = UnityEngine.Random.Range(0, regions[regionIndex].transform.childCount - 1);
            GameObject bombpos = regions[regionIndex].transform.GetChild(index).gameObject;

            bomb.transform.position = new Vector3(bombpos.transform.position.x, 0.1f, bombpos.transform.position.z);
            
            bomb.SetActive(true);

            bomb.GetComponent<BombDetectorMulti>().regionx = regions[regionIndex].transform.position.x;
            bomb.GetComponent<BombDetectorMulti>().regiony = regions[regionIndex].transform.position.y;
            bomb.GetComponent<BombDetectorMulti>().regionz = regions[regionIndex].transform.position.z;

            NetworkServer.Spawn(bomb);

        }

        GameControllerBSMultiTime gb = GameObject.Find("GameController").GetComponent<GameControllerBSMultiTime>();
        Vector3 startpos = GameObject.Find("helipad").transform.position;

        Vector3 temp = new Vector3(startpos.x, startpos.y, startpos.z);

        float totaldist = 0;

        foreach (GameObject bomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            totaldist = totaldist + Vector3.Distance(temp, bomb.transform.position);
            temp = bomb.transform.position;
        }

            if (gmc.diffid == "1")
            {
                gb.time = (int)(totaldist * 0.5);
            }
            else if (gmc.diffid == "2")
            {
                gb.time = (int)(totaldist * 0.75);
            }
            else
            {
                gb.time = (int)(totaldist * 1);
            }

        //this.gameObject.GetComponent<FinalOutcomeBSMultiTime>().start = true;
        //adjustTime();


    }

    // Update is called once per frame
    void Update()
    {

    }

}
