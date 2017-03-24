using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PrizeSpawner : NetworkBehaviour{

    public GameObject prize;
    public GameObject spawnGround;

    public int initialBallcount;

    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
		
	}

    void Start()
    {
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        if (gmc.diffid == "1")
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

        Random.InitState(10);
        for (int i = 0; i < initialBallcount; i++)
        {
            GameObject prizeobj = Instantiate(prize);
            
            float x = Random.Range(spawnGround.transform.position.x - spawnGround.transform.localScale.x / 2 + 20, spawnGround.transform.position.x + spawnGround.transform.localScale.x / 2 - 20);
            float z = Random.Range(spawnGround.transform.position.z - spawnGround.transform.localScale.z / 2 + 20, spawnGround.transform.position.z + spawnGround.transform.localScale.z / 2 - 20);
            prizeobj.transform.position = new Vector3(x, prizeobj.transform.position.y, z);
            prizeobj.name = "prize";
            prizeobj.SetActive(true);
        }

    }
    }
