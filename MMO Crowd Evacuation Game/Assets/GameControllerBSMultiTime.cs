using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameControllerBSMultiTime : NetworkBehaviour {

    public GameObject bombobj;
    public Text bombcounttext;

    int bombcount = 3;

    public Text minute, sec;

    [SyncVar]
    public int time;
    int count;

    public bool start;

    public GameObject soldier;

    GameMetaScript gmc;

    public GameObject localplayerobj = null;
    // Use this for initialization
    void Start()
    {

        RenderSettings.fog = false;

        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

        if (gmc.ctypeid != "2" && gmc.ctypeid != "3")
        {
            start = true;
        }
        else
        {
            start = false;
        }


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
            GameObject bomb = Instantiate(bombobj);

            int regionIndex = UnityEngine.Random.Range(0, regions.Length);



            int index = UnityEngine.Random.Range(0, regions[regionIndex].transform.childCount - 1);
            GameObject bombpos = regions[regionIndex].transform.GetChild(index).gameObject;

            bomb.transform.position = new Vector3(bombpos.transform.position.x, 0.1f, bombpos.transform.position.z);

            bomb.SetActive(true);
        }

        bombcounttext.text = bombcount.ToString();

        Vector3 startpos = GameObject.Find("helipad").transform.position;


        GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");
        float[] distances = new float[bombs.Length];
        Vector3 temp = new Vector3(startpos.x, startpos.y, startpos.z);

        int index1 = 0;
        List<GameObject> bomblist = new List<GameObject>();

        float totaldist = 0;
        float mindist = Single.MaxValue;
        int minindex = 0;
        foreach (GameObject bomb in bombs)
        {
            bomblist.Add(bomb);
            temp.y = 0.1f;

            if (mindist > Vector3.Distance(temp, bomb.transform.GetChild(0).position))
            {
                mindist = Vector3.Distance(temp, bomb.transform.GetChild(0).position);
                minindex = index1;
            }
            index1++;
        }

        totaldist = totaldist + mindist;
        temp = bombs[minindex].transform.GetChild(0).position;
        bomblist.Remove(bombs[minindex]);

        while (bomblist.Count != 0)
        {
            mindist = Single.MaxValue;
            index1 = 0;
            foreach (GameObject bomb in bomblist)
            {
                if (mindist > Vector3.Distance(temp, bomb.transform.GetChild(0).position))
                {
                    mindist = Vector3.Distance(temp, bomb.transform.GetChild(0).position);
                    minindex = index1;
                }
                index1++;
            }

            totaldist = totaldist + mindist;
            temp = bombs[minindex].transform.GetChild(0).position;
            bomblist.Remove(bombs[minindex]);
        }

        if (gmc.ruleid == "4")
        {
            if (gmc.diffid == "1")
            {
                time = (int)totaldist * 15;
            }
            else if (gmc.diffid == "2")
            {
                time = (int)totaldist * 10;
            }
            else
            {
                time = (int)totaldist * 5;
            }
        }
        else if (gmc.ruleid == "3")
        {
            if (gmc.diffid == "1")
            {
                time = (int)totaldist * 30;
            }
            else if (gmc.diffid == "2")
            {
                time = (int)totaldist * 20;
            }
            else
            {
                time = (int)totaldist * 10;
            }
        }


        count = 0;


        //GameObject[] bombposis = GameObject.FindGameObjectsWithTag("bombpos");


        if (time / 60 < 10)
        {
            minute.text = "0" + (time / 60);
        }
        else
        {
            minute.text = "" + (time / 60);
        }

        if (time % 60 < 10)
        {
            sec.text = "0" + (time % 60);
        }
        else
        {
            sec.text = "" + (time % 60);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!this.GetComponent<FinalOutcomeBSMultiTime>().finish && start)
        {

            GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

            bombcounttext.text = bombs.Length.ToString();

            count++;

            if (count == 60)
            {
                count = 0;
                if (time >= 1)
                {
                    time--;
                }
                if (time / 60 < 10)
                {
                    minute.text = "0" + (time / 60);
                }
                else
                {
                    minute.text = "" + (time / 60);
                }

                if (time % 60 < 10)
                {
                    sec.text = "0" + (time % 60);
                }
                else
                {
                    sec.text = "" + (time % 60);
                }
            }

        }
    }
}
