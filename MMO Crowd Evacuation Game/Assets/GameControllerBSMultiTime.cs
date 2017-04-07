using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RegionPos : IEquatable<RegionPos>
{
    public float x, y, z;

    public RegionPos()
    {

    }

    public RegionPos(float x,float y,float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public bool Equals(RegionPos other)
    {
        return this.x == other.x &&
               this.y == other.y &&
               this.z == other.z;
    }

}

public class GameControllerBSMultiTime : NetworkBehaviour {

    public GameObject bombobj;
    public Text bombcounttext;

    //int bombcount = 3;

    public Text minute, sec;

    [SyncVar]
    public int time;
    int count;

    //public bool start;

    public GameObject soldier;

    GameMetaScript gmc;

    public GameObject localplayerobj = null;

    public List<RegionPos> bomPosis;
    // Use this for initialization
    void Start()
    {

        bomPosis = new List<RegionPos>();
        GameObject[] regions = GameObject.FindGameObjectsWithTag("region");

        foreach (GameObject region in regions)
        {
                bomPosis.Add(new RegionPos(region.transform.position.x,region.transform.position. y,region.transform.position. z));
     
        }

        foreach(GameObject bomb in GameObject.FindGameObjectsWithTag("bomb"))
        {
            bomPosis.Remove(new RegionPos(bomb.GetComponent<BombDetectorMulti>().regionx, bomb.GetComponent<BombDetectorMulti>().regiony, bomb.GetComponent<BombDetectorMulti>().regionz));
        }


        RenderSettings.fog = false;
        time = 200;
        gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

   /*     if (gmc.ctypeid != "2" && gmc.ctypeid != "3")
        {
            start = true;
        }
        else
        {
            start = false;
        }
*/
/*
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
        }*/


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

        if (!this.GetComponent<FinalOutcomeBSMultiTime>().finish )
        {

            GameObject[] bombs = GameObject.FindGameObjectsWithTag("bomb");

            if (localplayerobj != null)
            {
                GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();

                if (gmc.ctypeid == "1" || gmc.ctypeid == "5")
                {
                    bombcounttext.text = localplayerobj.GetComponent<PrizeCounter>().ballcount.ToString();
                }
                else if (gmc.ctypeid == "2")
                {
                    if (localplayerobj.gameObject.GetComponent<PrizeCounter>().teamno == 1)
                    {
                        bombcounttext.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();

                    }
                    else
                    {
                        bombcounttext.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount2.ToString();
                    }

                }
                else if (gmc.ctypeid == "3")
                {
                    bombcounttext.text = GameObject.Find("TeamCounter").GetComponent<TeamCounter>().ballcount1.ToString();
                }


            }
            //bombcounttext.text = bombs.Length.ToString();

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
