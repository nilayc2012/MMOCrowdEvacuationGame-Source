using UnityEngine;
using System.Collections;

public class UserDataScript : MonoBehaviour {

    public string uid, fullname, gamername;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
