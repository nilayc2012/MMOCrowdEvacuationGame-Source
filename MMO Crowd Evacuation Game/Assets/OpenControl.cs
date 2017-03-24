using UnityEngine;
using System.Collections;

public class OpenControl : MonoBehaviour {

    public GameObject panel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void openControl()
    {
        panel.SetActive(true);
    }
    public void closeControl()
    {
        panel.SetActive(false);
    }
}
