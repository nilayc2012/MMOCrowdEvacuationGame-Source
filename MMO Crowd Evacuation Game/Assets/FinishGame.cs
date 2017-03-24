using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void exit()
    {
        Application.Quit();
    }
}
