using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombSpawner : NetworkBehaviour {

    public GameObject bombPrefab;
    GameMetaScript gmc;
    int bombcount;

    // Use this for initialization
    public override void OnStartServer () {

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
        Random.InitState(10);

        for (int i = 0; i < bombcount; i++)
        {
            GameObject bomb = Instantiate(bombPrefab);

            int regionIndex = UnityEngine.Random.Range(0, regions.Length);


            int index = UnityEngine.Random.Range(0, regions[regionIndex].transform.childCount - 1);
            GameObject bombpos = regions[regionIndex].transform.GetChild(index).gameObject;

            bomb.transform.position = new Vector3(bombpos.transform.position.x, 0.1f, bombpos.transform.position.z);

            bomb.SetActive(true);

            NetworkServer.Spawn(bomb);

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
