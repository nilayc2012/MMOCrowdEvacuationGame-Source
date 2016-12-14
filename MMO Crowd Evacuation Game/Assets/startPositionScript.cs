using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class startPositionScript : MonoBehaviour {

    public GameObject startpos;
    
    // Use this for initialization
    void Awake () {

        for (int i = 0; i < GameObject.Find("LobbyManager").GetComponent<Prototype.NetworkLobby.LobbyManager>().maxPlayers; i++)
        {
            GameObject[] planes = GameObject.FindGameObjectsWithTag("dummyplane");
            Debug.Log(planes.Length);

            GameObject posPlane = planes[Convert.ToInt32(UnityEngine.Random.value * (planes.Length - 1))];

            float x = UnityEngine.Random.Range(posPlane.transform.position.x - posPlane.transform.localScale.x * 10 / 2, posPlane.transform.position.x + posPlane.transform.localScale.x * 10 / 2);

            float z = UnityEngine.Random.Range(posPlane.transform.position.z - posPlane.transform.localScale.z * 10 / 2, posPlane.transform.position.z + posPlane.transform.localScale.z * 10 / 2);

            GameObject start = Instantiate(startpos);

            start.transform.position = new Vector3(x, 0.0f, z);
            start.SetActive(true);
            NetworkManager.RegisterStartPosition(start.transform);
            DontDestroyOnLoad(start);
        }
        }

    
	
	// Update is called once per frame
	void Update () {


    }
}
