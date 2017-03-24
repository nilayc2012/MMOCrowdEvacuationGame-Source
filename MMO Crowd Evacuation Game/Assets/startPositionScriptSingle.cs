using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class startPositionScriptSingle : MonoBehaviour {

    public GameObject agent;
    
    // Use this for initialization
    void Awake () {

            GameObject[] planes = GameObject.FindGameObjectsWithTag("dummyplane");

            GameObject posPlane = planes[Convert.ToInt32(UnityEngine.Random.value * (planes.Length - 1))];

            float x = UnityEngine.Random.Range(posPlane.transform.position.x - posPlane.transform.localScale.x * 10 / 2, posPlane.transform.position.x + posPlane.transform.localScale.x * 10 / 2);

            float z = UnityEngine.Random.Range(posPlane.transform.position.z - posPlane.transform.localScale.z * 10 / 2, posPlane.transform.position.z + posPlane.transform.localScale.z * 10 / 2);

            agent.transform.position = new Vector3(x, 0.0f, z);
        
        }

    
	
	// Update is called once per frame
	void Update () {


    }
}
