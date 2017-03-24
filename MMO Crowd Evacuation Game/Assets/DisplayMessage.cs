using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour {

    int count;
	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        count++;
        if(count==50)
        {
            this.gameObject.SetActive(false);
        }
	}
}
