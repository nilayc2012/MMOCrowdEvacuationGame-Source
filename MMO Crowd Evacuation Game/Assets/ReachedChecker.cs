using UnityEngine;
using System.Collections;

public class ReachedChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.GetComponent<NavigationControllerBS>().isHere && this.GetComponent<NavigationControllerBS>().enabled)
        {
            this.GetComponent<NavigationControllerBS>().enabled = false;
            this.GetComponent<NavigationControllerBS>().isHere = false;
        }
	}
}
