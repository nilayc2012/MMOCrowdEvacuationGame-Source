using UnityEngine;
using System.Collections;

public class SpawnPosGenerator : MonoBehaviour {

    public GameObject startpos;

    void Awake()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("multiplayer");

        for(int i=0;i<agents.Length;i++)
        {
            Instantiate(startpos);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
