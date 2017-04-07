using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DistanceCheckerMulti : NetworkBehaviour {

    public Text distancetext;
    GameObject[] bombs;
    // Use this for initialization
    void Start()
    {
        distancetext = GameObject.Find("distancetext").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            bombs = GameObject.FindGameObjectsWithTag("bomb");
            int[] distances = new int[bombs.Length];
            Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            int index = 0;
            int count = 0;

            foreach (GameObject bomb in bombs)
            {
                if (!bomb.GetComponent<BombDetectorMulti>().detected && !bomb.GetComponent<BombDetectorMulti>().isDiffused)
                {
                    temp.y = 0.1f;
                    distances[index++] = (int)Vector3.Distance(temp, bomb.transform.GetChild(0).position);
                    count++;
                }
                else
                {
                    distances[index++] = Int32.MaxValue;
                }
            }

            if(count!=0)
            distancetext.text = distances.Min() + " m";
            else
                distancetext.text = "No More Bombs to Defuse";

        }
    }
}
