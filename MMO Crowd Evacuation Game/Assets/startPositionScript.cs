using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class startPositionScript : NetworkBehaviour {

    public GameObject startpos;

    //public GameObject dummyplanes;

    float lowx1=-1190f, lowx2=-1025f, highx1=1030f, highx2=1190f, lowz1=-1200f, lowz2=-1040f, highz1=1030f, highz2=1190f;

    
    // Use this for initialization
    void Awake() {

        UnityEngine.Random.InitState(12);
        Debug.Log("max : " + GameObject.Find("LobbyManager").GetComponent<Prototype.NetworkLobby.LobbyManager>().maxPlayers);
        for (int i = 0; i < GameObject.Find("LobbyManager").GetComponent<Prototype.NetworkLobby.LobbyManager>().maxPlayers; i++)
        {
            //Transform[] planes = dummyplanes.GetComponentsInChildren<Transform>();

            //GameObject posPlane = planes[Convert.ToInt32(UnityEngine.Random.value * (planes.Length - 1))].gameObject;

            //float x = UnityEngine.Random.Range(posPlane.transform.position.x - posPlane.transform.localScale.x * 10 / 2, posPlane.transform.position.x + posPlane.transform.localScale.x * 10 / 2);

            //float z = UnityEngine.Random.Range(posPlane.transform.position.z - posPlane.transform.localScale.z * 10 / 2, posPlane.transform.position.z + posPlane.transform.localScale.z * 10 / 2);

            float x=0, z=0;

            if (GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid == "2")
            {
                x = UnityEngine.Random.Range(-30f, 140f);
                z = UnityEngine.Random.Range(100f, -65f);
            }

            else if (GameObject.Find("GameMetaData").GetComponent<GameMetaScript>().ruleid == "1")
            {


                if (UnityEngine.Random.value <= 0.25)
                {
                    x = UnityEngine.Random.Range(lowx1, lowx2);
                    z = UnityEngine.Random.Range(lowz1, highz2);
                }
                else if (UnityEngine.Random.value > 0.25 && UnityEngine.Random.value <= 0.5)
                {
                    x = UnityEngine.Random.Range(highx1, highx2);
                    z = UnityEngine.Random.Range(lowz1, highz2);
                }
                else if (UnityEngine.Random.value > 0.5 && UnityEngine.Random.value <= 0.75)
                {
                    x = UnityEngine.Random.Range(lowx1, highx2);
                    z = UnityEngine.Random.Range(lowz1, lowz2);
                }
                else
                {
                    x = UnityEngine.Random.Range(lowx1, highx2);
                    z = UnityEngine.Random.Range(highz1, highz2);
                }
            }

            GameObject start = Instantiate(startpos);

            start.transform.position = new Vector3(x, 0.0f, z);
            NetworkManager.RegisterStartPosition(start.transform);
        }


    }

    void Start()
    {

        GameObject[] agents = GameObject.FindGameObjectsWithTag("multiplayer");

        foreach(GameObject agent in agents)
        {
            GameObject strtpos = GameObject.FindGameObjectWithTag("startpos");
            agent.transform.position = strtpos.transform.position;
            agent.GetComponent<PrizeCounter>().startpos = strtpos.transform.position;
            strtpos.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {


    }

}
