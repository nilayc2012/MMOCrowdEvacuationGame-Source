using UnityEngine;
using System.Collections;

public class DeactivatePanel : MonoBehaviour {

    public GameObject panel;
	// Use this for initialization
	void Start () {

        panel.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        panel.SetActive(false);

	}
}
