using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class DistanceChecker : MonoBehaviour {

    public Text distancetext;
    GameObject[] bombs;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        bombs = GameObject.FindGameObjectsWithTag("bomb");
        int[] distances = new int[bombs.Length];
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        int index = 0;
        
        foreach (GameObject bomb in bombs)
        {
            temp.y = 0.1f;
            distances[index++] = (int)Vector3.Distance(temp, bomb.transform.GetChild(0).position);
        }

        distancetext.text = distances.Min() + " m";

    }
}
