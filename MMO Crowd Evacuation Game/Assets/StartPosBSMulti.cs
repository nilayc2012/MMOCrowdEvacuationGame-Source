using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class StartPosBSMulti : NetworkBehaviour {

    public GameObject startpos;

    // Use this for initialization
    void Awake()
    {

        for (int i = 0; i < GameObject.Find("LobbyManager").GetComponent<Prototype.NetworkLobby.LobbyManager>().maxPlayers; i++)
        {

            GameObject posPlane = GameObject.Find("helipad");

            float x = UnityEngine.Random.Range(posPlane.transform.position.x - posPlane.transform.localScale.x * 10 / 2, posPlane.transform.position.x + posPlane.transform.localScale.x * 10 / 2);

            float z = UnityEngine.Random.Range(posPlane.transform.position.z - posPlane.transform.localScale.z * 10 / 2, posPlane.transform.position.z + posPlane.transform.localScale.z * 10 / 2);

            GameObject start = Instantiate(startpos);

            start.transform.position = new Vector3(x, 0.0f, z);
            start.SetActive(true);
            NetworkManager.RegisterStartPosition(start.transform);
            DontDestroyOnLoad(start);
        }
    }


    void Start()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("drone");

        foreach (GameObject agent in agents)
        {
            GameObject strtpos = GameObject.FindGameObjectWithTag("startpos");
            agent.transform.position = strtpos.transform.position;
            agent.GetComponent<PrizeCounter>().startpos = strtpos.transform.position;
            strtpos.SetActive(false);
        }
    }


}
