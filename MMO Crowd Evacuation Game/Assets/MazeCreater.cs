using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreater : MonoBehaviour {

    public GameObject maze1,maze2,maze3;
   
    void Awake()
    {
        GameMetaScript gmc = GameObject.Find("GameMetaData").GetComponent<GameMetaScript>();
        if(gmc.envid=="1")
        {
            Instantiate(maze1);
        }
        else if(gmc.envid == "2")
        {
            Instantiate(maze2);
        }
        else
        {
            Instantiate(maze3);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
